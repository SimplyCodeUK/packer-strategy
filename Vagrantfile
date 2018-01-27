# -*- mode: ruby -*-
# vi: set ft=ruby :
# Copyright (c) 2018 Simply Code Ltd. All rights reserved.
# Licensed under the MIT License.
# See LICENSE file in the project root for full license information.

SERVICES = {
  packit: {
    repo: "https://github.com/SimplyCodeUK/packer-strategy.git",
    project_file: "PackIt.csproj",
    build_dir: "PackIt/src/PackIt",
    binary: "PackIt.dll",
    server_location: "/",
    guest_port: "8000",
    host_port: "8100"
  },
  packitui: {
    repo: "https://github.com/SimplyCodeUK/packer-strategy.git",
    project_file: "PackItUI.csproj",
    build_dir: "PackItUI/src/PackItUI",
    binary: "PackItUI.dll",
    server_location: "/",
    guest_port: "9000",
    host_port: "8080"
  }
}

MACHINES = {
  packit: {
    services: [
      "packit",
      "packitui"
    ]
  }
}

SERVICES_DIR = "/srv"

BASE_INSTALL = <<-SCRIPT
  echo "ubuntu:ubuntu" | chpasswd
  curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
  mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
  sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
  apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893
  apt-get install python-software-properties=0.96.20.7 -y
  curl -sL https://deb.nodesource.com/setup_8.x | sudo -E bash -
  apt-get update
  apt-get install dotnet-sdk-2.1.4=2.1.4-1      -y
  apt-get install nuget=2.8.7+md510+dhx1-1      -y
  apt-get install git=1:2.7.4-0ubuntu1.3        -y
  apt-get install nginx=1.10.3-0ubuntu0.16.04.2 -y
  apt-get install nodejs=8.9.4-1nodesource1     -y
  npm install -g bower
  bower --version
  systemctl stop nginx
  rm /etc/nginx/sites-enabled/default 2> /dev/null
SCRIPT

# All Vagrant configuration is done below. The "2" in Vagrant.configure
# configures the configuration version (we support older styles for
# backwards compatibility). Please don't change it unless you know what
# you're doing.
Vagrant.configure("2") do |config|
  # Every Vagrant development environment requires a box. You can search for
  # boxes at https://vagrantcloud.com/search.
  config.vm.box = "ubuntu/xenial64"

  MACHINES.each do |key, machine|
    buildEnv = BASE_INSTALL

    config.vm.define "#{key}" do |node|
      machine[:services].each do |service|
        buildEnv += <<-SCRIPT1
          systemctl stop #{service}.service

          cd #{SERVICES_DIR}
          if cd #{service}; then git pull; else git clone #{SERVICES[service.to_sym][:repo]} #{service}; fi
          cd #{SERVICES_DIR}/#{service}/#{SERVICES[service.to_sym][:build_dir]}
          if [ -f "./bower.json" ]; then
            bower install --allow-root
          fi
          nuget restore #{SERVICES[service.to_sym][:project_file]}
          dotnet restore #{SERVICES[service.to_sym][:project_file]}
          dotnet publish --configuration Release
          echo "[Unit]"                                                                                                                   > "/etc/systemd/system/#{service}.service"
          echo "Description=Example .NET Web API Application running on Ubuntu"                                                          >> "/etc/systemd/system/#{service}.service"
          echo ""                                                                                                                        >> "/etc/systemd/system/#{service}.service"
          echo "[Service]"                                                                                                               >> "/etc/systemd/system/#{service}.service"
          echo "WorkingDirectory=#{SERVICES_DIR}/#{service}/#{SERVICES[service.to_sym][:build_dir]}/bin/Release/netcoreapp2.0/publish"   >> "/etc/systemd/system/#{service}.service"
          echo "ExecStart=/usr/bin/dotnet #{SERVICES[service.to_sym][:binary]} --urls http://*:#{SERVICES[service.to_sym][:guest_port]}" >> "/etc/systemd/system/#{service}.service"
          echo "Restart=always"                                                                                                          >> "/etc/systemd/system/#{service}.service"
          echo "RestartSec=60s  # Restart service after 60 seconds if dotnet service crashes"                                            >> "/etc/systemd/system/#{service}.service"
          echo "SyslogIdentifier=#{service}"                                                                                             >> "/etc/systemd/system/#{service}.service"
          echo "User=www-data"                                                                                                           >> "/etc/systemd/system/#{service}.service"
          echo "Environment=ASPNETCORE_ENVIRONMENT=Development"                                                                          >> "/etc/systemd/system/#{service}.service"
          echo "Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false"                                                                        >> "/etc/systemd/system/#{service}.service"
          echo "Environment=DOTNET_CLI_TELEMETRY_OPTOUT=1"                                                                               >> "/etc/systemd/system/#{service}.service"
          echo ""                                                                                                                        >> "/etc/systemd/system/#{service}.service"
          echo "[Install]"                                                                                                               >> "/etc/systemd/system/#{service}.service"
          echo "WantedBy=multi-user.target"                                                                                              >> "/etc/systemd/system/#{service}.service"

          echo "server {"                                                                   > "/etc/nginx/sites-available/#{service}"
        SCRIPT1

        if SERVICES[service.to_sym].key?(:host_port)
          buildEnv += <<-SCRIPT2
            echo "  listen #{SERVICES[service.to_sym][:host_port]};"                       >> "/etc/nginx/sites-available/#{service}"
            echo "  listen [::]:#{SERVICES[service.to_sym][:host_port]} default_server;"   >> "/etc/nginx/sites-available/#{service}"
          SCRIPT2
        end

        buildEnv += <<-SCRIPT3
          echo "  location #{SERVICES[service.to_sym][:server_location]} {"                >> "/etc/nginx/sites-available/#{service}"
          echo "    proxy_pass http://localhost:#{SERVICES[service.to_sym][:guest_port]};" >> "/etc/nginx/sites-available/#{service}"
          echo "  }"                                                                       >> "/etc/nginx/sites-available/#{service}"
          echo "}"                                                                         >> "/etc/nginx/sites-available/#{service}"

          systemctl enable #{service}.service
          systemctl start #{service}.service

          ln -sf /etc/nginx/sites-available/#{service} /etc/nginx/sites-enabled/#{service}
        SCRIPT3

        if SERVICES[service.to_sym].key?(:host_port) && SERVICES[service.to_sym].key?(:guest_port)
          node.vm.network "forwarded_port", guest: SERVICES[service.to_sym][:guest_port], host: SERVICES[service.to_sym][:host_port], id: "#{service}"
        end
      end

      # start nginx
      buildEnv += <<-SCRIPT
        systemctl start nginx
      SCRIPT

      node.vm.provision "shell", inline: buildEnv
    end
  end
end

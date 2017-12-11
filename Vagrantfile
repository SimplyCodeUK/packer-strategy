# -*- mode: ruby -*-
# vi: set ft=ruby :

SERVICES = {
  packit: {
    repo: "https://github.com/SimplyCodeUK/packer-strategy.git",
    project_file: "PackIt.csproj",
    build_dir: "PackIt/src/PackIt",
    binary: "PackIt.dll",
    server_location: "/api",
    proxy_pass: "http://localhost:8000"
  },
  packitui: {
    repo: "https://github.com/SimplyCodeUK/packer-strategy.git",
    project_file: "PackItUI.csproj",
    build_dir: "PackItUI/src/PackItUI",
    binary: "PackItUI.dll",
    server_location: "/ui",
    proxy_pass: "http://localhost:9000"
  }
}

MACHINES = {
  packit: {
    services: [
      "packit"
    ],
    guest_port: "80",
    host_port: "8080"
  },
  packitui: {
    services: [
      "packitui"
    ],
    guest_port: "80",
    host_port: "8081"
  }
}

SERVICES_DIR = "/srv"

BASE_INSTALL = <<-SCRIPT
  echo "ubuntu:ubuntu" | chpasswd
  curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
  sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
  sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
  apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893
  apt-get update
  apt-get install dotnet-sdk-2.0.3=2.0.3-1      -y
  apt-get install nuget=2.8.7+md510+dhx1-1      -y
  apt-get install git=1:2.7.4-0ubuntu1.3        -y
  apt-get install nginx=1.10.3-0ubuntu0.16.04.2 -y
  systemctl stop nginx
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
        buildEnv += <<-SCRIPT
          systemctl stop #{service}.service

          cd #{SERVICES_DIR}
          if cd #{service}; then git pull; else git clone #{SERVICES[service.to_sym][:repo]} #{service}; fi
          cd #{SERVICES_DIR}/#{service}/#{SERVICES[service.to_sym][:build_dir]}
          nuget restore #{SERVICES[service.to_sym][:project_file]}
          dotnet restore #{SERVICES[service.to_sym][:project_file]}
          dotnet publish --configuration Release
          echo "[Unit]"                                                                                                                 > "/etc/systemd/system/#{service}.service"
          echo "Description=Example .NET Web API Application running on Ubuntu"                                                        >> "/etc/systemd/system/#{service}.service"
          echo ""                                                                                                                      >> "/etc/systemd/system/#{service}.service"
          echo "[Service]"                                                                                                             >> "/etc/systemd/system/#{service}.service"
          echo "WorkingDirectory=#{SERVICES_DIR}/#{service}/#{SERVICES[service.to_sym][:build_dir]}/bin/Release/netcoreapp2.0/publish" >> "/etc/systemd/system/#{service}.service"
          echo "ExecStart=/usr/bin/dotnet #{SERVICES[service.to_sym][:binary]}"                                                        >> "/etc/systemd/system/#{service}.service"
          echo "Restart=always"                                                                                                        >> "/etc/systemd/system/#{service}.service"
          echo "RestartSec=60s  # Restart service after 60 seconds if dotnet service crashes"                                          >> "/etc/systemd/system/#{service}.service"
          echo "SyslogIdentifier=#{service}"                                                                                           >> "/etc/systemd/system/#{service}.service"
          echo "User=www-data"                                                                                                         >> "/etc/systemd/system/#{service}.service"
          echo "Environment=ASPNETCORE_ENVIRONMENT=Development"                                                                        >> "/etc/systemd/system/#{service}.service"
          echo "Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false"                                                                      >> "/etc/systemd/system/#{service}.service"
          echo "Environment=DOTNET_CLI_TELEMETRY_OPTOUT=1"                                                                             >> "/etc/systemd/system/#{service}.service"
          echo ""                                                                                                                      >> "/etc/systemd/system/#{service}.service"
          echo "[Install]"                                                                                                             >> "/etc/systemd/system/#{service}.service"
          echo "WantedBy=multi-user.target"                                                                                            >> "/etc/systemd/system/#{service}.service"

          echo "server {"                                                                                                               > "/etc/nginx/sites-available/#{service}"
          echo "  listen 80;"                                                                                                          >> "/etc/nginx/sites-available/#{service}"
          echo "  listen [::]:80 default_server;                                                                                       >> "/etc/nginx/sites-available/#{service}"
          echo "  location #{SERVICES[service.to_sym][:server_location]} {"                                                            >> "/etc/nginx/sites-available/#{service}"
          echo "    proxy_pass #{SERVICES[service.to_sym][:proxy_pass]};"                                                              >> "/etc/nginx/sites-available/#{service}"
          echo "  }"                                                                                                                   >> "/etc/nginx/sites-available/#{service}"
          echo "}"                                                                                                                     >> "/etc/nginx/sites-available/#{service}"

          systemctl enable #{service}.service
          systemctl start #{service}.service

          cd /etc/nginx/sites-enabled
          rm /etc/nginx/sites-enabled/default 2> /dev/null
          ln -sf "/etc/nginx/sites-available/#{service}" "/etc/nginx/sites-enabled/#{service}"
        SCRIPT
      end

      # start nginx
      buildEnv += <<-SCRIPT
        systemctl start nginx
      SCRIPT

      node.vm.network "forwarded_port", guest: machine[:guest_port], host: machine[:host_port], id: "nginx"
      node.vm.provision "shell", inline: buildEnv
    end
  end
end
# -*- mode: ruby -*-
# vi: set ft=ruby :
# Copyright (c) 2018 Simply Code Ltd. All rights reserved.
# Licensed under the MIT License.
# See LICENSE file in the project root for full license information.

DATABASES = {
  postgresql: {
    server_location: "/",
    guest_port: "5432",
    host_port: "5000"
  }
}

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
    dbs: [
      "postgresql"
    ],
    services: [
      "packit",
      "packitui"
    ]
  }
}

SERVICES_DIR = "/srv"

COMMON_INSTALL = <<-COMMON_INSTALL_SCRIPT
echo "ubuntu:ubuntu" | chpasswd
COMMON_INSTALL_SCRIPT

DATABASE_PRE_INSTALL = <<-DATABASE_PRE_INSTALL_SCRIPT
DATABASE_PRE_INSTALL_SCRIPT

SERVICE_PRE_INSTALL = <<-SERVICE_PREP_SCRIPT
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893
SERVICE_PREP_SCRIPT

BASE_PRE_INSTALL = <<-BASE_PRE_INSTALL_SCRIPT
apt-get update
BASE_PRE_INSTALL_SCRIPT

DATABASE_INSTALL = <<-DATABASE_INSTALL_SCRIPT
echo 'deb http://apt.postgresql.org/pub/repos/apt/ xenial-pgdg main' | tee /etc/apt/sources.list.d/pgdg.list
wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | apt-key add -
apt-get update
apt-get install postgresql-10         -y
apt-get install postgresql-contrib-10 -y
service postgresql stop
echo "-------------------- fixing listen_addresses on /etc/postgresql/10/main/postgresql.conf"
sed -i "s/#listen_address.*/listen_addresses '*'/" /etc/postgresql/10/main/postgresql.conf
echo "-------------------- fixing postgres /etc/postgresql/10/main/pg_hba.conf"
cat >> /etc/postgresql/10/main/pg_hba.conf <<EOF
# Accept all IPv4 connections - FOR DEVELOPMENT ONLY!!!
host    all         all         0.0.0.0/0             md5
EOF
echo "-------------------- set default client_encoding /var/lib/postgresql/10/main"
echo "client_encoding = utf8" >> /var/lib/postgresql/10/main
service postgresql restart
sudo -u postgres psql --command "ALTER USER postgres WITH PASSWORD 'postgres';"
DATABASE_INSTALL_SCRIPT

SERVICE_INSTALL = <<-SERVICE_INSTALL_SCRIPT
apt-get install python3-software-properties=0.96.20.7 -y
curl -sL https://deb.nodesource.com/setup_8.x | sudo -E bash -
apt-get install dotnet-sdk-2.1.4=2.1.4-1      -y
apt-get install nuget=2.8.7+md510+dhx1-1      -y
apt-get install git=1:2.7.4-0ubuntu1.3        -y
apt-get install nginx=1.10.3-0ubuntu0.16.04.2 -y
apt-get install nodejs=8.9.4-1nodesource1     -y
npm install -g bower
bower --version
systemctl stop nginx
rm /etc/nginx/sites-enabled/default 2> /dev/null
SERVICE_INSTALL_SCRIPT

MACHINES.each do |_key, machine|
  buildScript = COMMON_INSTALL

  if machine[:dbs]
    buildScript += DATABASE_PRE_INSTALL
  end
  if machine[:services]
    buildScript += SERVICE_PRE_INSTALL
  end
  buildScript += BASE_PRE_INSTALL

  if machine[:dbs]
    buildScript += DATABASE_INSTALL
  end
  if machine[:services]
    buildScript += SERVICE_INSTALL
  end

  machine[:dbs].each do |db|
    buildScript += <<-DB_SCRIPT1
      echo "server {"                                                               > "/etc/nginx/sites-available/#{db}"
    DB_SCRIPT1
    if DATABASES[db.to_sym].key?(:host_port)
      buildScript += <<-DB_SCRIPT2
        echo "  listen #{DATABASES[db.to_sym][:host_port]};"                       >> "/etc/nginx/sites-available/#{db}"
        echo "  listen [::]:#{DATABASES[db.to_sym][:host_port]} default_server;"   >> "/etc/nginx/sites-available/#{db}"
      DB_SCRIPT2
    end
    buildScript += <<-DB_SCRIPT3
      echo "  location #{DATABASES[db.to_sym][:server_location]} {"                >> "/etc/nginx/sites-available/#{db}"
      echo "    proxy_pass http://localhost:#{DATABASES[db.to_sym][:guest_port]};" >> "/etc/nginx/sites-available/#{db}"
      echo "  }"                                                                   >> "/etc/nginx/sites-available/#{db}"
      echo "}"                                                                     >> "/etc/nginx/sites-available/#{db}"

      ln -sf /etc/nginx/sites-available/#{db} /etc/nginx/sites-enabled/#{db}
    DB_SCRIPT3
  end

  machine[:services].each do |service|
    buildScript += <<-SRV_SCRIPT1
      systemctl stop #{service}.service

      cd #{SERVICES_DIR}
      if [[ -d "${service}" && ! -L "${service}" ]] ; then git pull; else git clone #{SERVICES[service.to_sym][:repo]} #{service}; fi
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
    SRV_SCRIPT1

    if SERVICES[service.to_sym].key?(:host_port)
      buildScript += <<-SRV_SCRIPT2
        echo "  listen #{SERVICES[service.to_sym][:host_port]};"                       >> "/etc/nginx/sites-available/#{service}"
        echo "  listen [::]:#{SERVICES[service.to_sym][:host_port]} default_server;"   >> "/etc/nginx/sites-available/#{service}"
      SRV_SCRIPT2
    end
    buildScript += <<-SRV_SCRIPT3
      echo "  location #{SERVICES[service.to_sym][:server_location]} {"                >> "/etc/nginx/sites-available/#{service}"
      echo "    proxy_pass http://localhost:#{SERVICES[service.to_sym][:guest_port]};" >> "/etc/nginx/sites-available/#{service}"
      echo "  }"                                                                       >> "/etc/nginx/sites-available/#{service}"
      echo "}"                                                                         >> "/etc/nginx/sites-available/#{service}"

      systemctl enable #{service}.service
      systemctl start #{service}.service

      ln -sf /etc/nginx/sites-available/#{service} /etc/nginx/sites-enabled/#{service}
    SRV_SCRIPT3
  end

  buildScript += <<-SCRIPT4
    systemctl start nginx
  SCRIPT4

  machine[:build_script] = buildScript
end

# All Vagrant configuration is done below. The "2" in Vagrant.configure
# configures the configuration version (we support older styles for
# backwards compatibility). Please don't change it unless you know what
# you're doing.
Vagrant.configure("2") do |config|
  # Every Vagrant development environment requires a box. You can search for
  # boxes at https://vagrantcloud.com/search.
  config.vm.box = "ubuntu/xenial64"

  MACHINES.each do |key, machine|
    config.vm.define "#{key}" do |node|
      machine[:dbs].each do |db|
        if DATABASES[db.to_sym].key?(:host_port) && DATABASES[db.to_sym].key?(:guest_port)
          node.vm.network "forwarded_port", guest: DATABASES[db.to_sym][:guest_port], host: DATABASES[db.to_sym][:host_port], id: "#{db}"
        end
      end
      machine[:services].each do |service|
        if SERVICES[service.to_sym].key?(:host_port) && SERVICES[service.to_sym].key?(:guest_port)
          node.vm.network "forwarded_port", guest: SERVICES[service.to_sym][:guest_port], host: SERVICES[service.to_sym][:host_port], id: "#{service}"
        end
      end

      node.vm.provision "shell", inline: machine[:build_script]
    end
  end
end

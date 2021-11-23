# -*- mode: ruby -*-
# vi: set ft=ruby :
# Copyright (c) 2018-2021 Simply Code Ltd. All rights reserved.
# Licensed under the MIT License.
# See LICENSE file in the project root for full license information.

DATABASES = {
  postgresql: {
    server_location: "/",
    guest_port: "5432",
    host_port: "5000",
    connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=postgres;"
  }
}

SERVICES = {
  packit: {
    repo: "https://github.com/SimplyCodeUK/packer-strategy.git",
    branch: "master",
    project_file: "PackIt.csproj",
    build_dir: "PackIt/src",
    binary: "PackIt.dll",
    server_location: "/",
    guest_port: "8000",
    host_port: "8100",
    database: "postgresql"
  },
  packit_draw: {
    repo: "https://github.com/SimplyCodeUK/packer-strategy.git",
    branch: "master",
    project_file: "PackItDraw.csproj",
    build_dir: "PackItDraw/src",
    binary: "PackItDraw.dll",
    server_location: "/",
    guest_port: "8010",
    host_port: "8200",
    database: "postgresql"
  },
  packitui: {
    repo: "https://github.com/SimplyCodeUK/packer-strategy.git",
    branch: "master",
    project_file: "PackItUI.csproj",
    build_dir: "PackItUI/src",
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
      "packit_draw",
      "packitui"
    ]
  }
}

SERVICES_DIR = "/srv"

COMMON_INSTALL = <<-SCRIPT
echo "ubuntu:ubuntu" | chpasswd
SCRIPT

DATABASE_PRE_INSTALL = <<-SCRIPT
SCRIPT

SERVICE_PRE_INSTALL = <<-SCRIPT
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-focal-prod focal main" > /etc/apt/sources.list.d/dotnetdev.list'
apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893
wget -q https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb
SCRIPT

BASE_PRE_INSTALL = <<-SCRIPT
apt-get install apt-transport-https
apt-get update
SCRIPT

DATABASE_INSTALL = <<-SCRIPT
echo 'deb http://apt.postgresql.org/pub/repos/apt/ focal-pgdg main' | tee /etc/apt/sources.list.d/pgdg.list
wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | apt-key add -
apt-get update
apt-get install postgresql-10=10.* -y
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
SCRIPT

SERVICE_INSTALL = <<-SCRIPT
curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | sudo apt-key add -
echo "deb https://dl.yarnpkg.com/debian/ stable main" | sudo tee /etc/apt/sources.list.d/yarn.list
apt-get install python3-software-properties=0.96.* -y
curl -sL https://deb.nodesource.com/setup_10.x | sudo -E bash -
apt-get install nodejs         -y
apt-get install yarn           -y
apt-get install nuget          -y
apt-get install git            -y
apt-get install dotnet-sdk-6.0 -y
apt-get install nginx          -y
service nginx stop
rm /etc/nginx/sites-enabled/default 2> /dev/null
SCRIPT

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
    buildScript += <<-DB_SCRIPT
      echo "server {"                                                               > "/etc/nginx/sites-available/#{db}"
    DB_SCRIPT
    if DATABASES[db.to_sym].key?(:host_port)
      buildScript += <<-DB_SCRIPT
        echo "  listen #{DATABASES[db.to_sym][:host_port]};"                       >> "/etc/nginx/sites-available/#{db}"
        echo "  listen [::]:#{DATABASES[db.to_sym][:host_port]} default_server;"   >> "/etc/nginx/sites-available/#{db}"
      DB_SCRIPT
    end
    buildScript += <<-DB_SCRIPT
      echo "  location #{DATABASES[db.to_sym][:server_location]} {"                >> "/etc/nginx/sites-available/#{db}"
      echo "    proxy_pass http://localhost:#{DATABASES[db.to_sym][:guest_port]};" >> "/etc/nginx/sites-available/#{db}"
      echo "  }"                                                                   >> "/etc/nginx/sites-available/#{db}"
      echo "}"                                                                     >> "/etc/nginx/sites-available/#{db}"

      ln -sf /etc/nginx/sites-available/#{db} /etc/nginx/sites-enabled/#{db}
    DB_SCRIPT
  end

  machine[:services].each do |service|
    workingDir = "#{SERVICES_DIR}/#{service}/#{SERVICES[service.to_sym][:build_dir]}/bin/Release/net6.0/publish"
    buildScript += <<-SRV_SCRIPT
      systemctl stop #{service}.service

      cd #{SERVICES_DIR}
      if [[ -d "${service}" && ! -L "${service}" ]]
      then
        git pull
      else
        git clone --branch #{SERVICES[service.to_sym][:branch]} #{SERVICES[service.to_sym][:repo]} #{service}
      fi
      cd #{SERVICES_DIR}/#{service}/#{SERVICES[service.to_sym][:build_dir]}
      nuget restore #{SERVICES[service.to_sym][:project_file]}
      dotnet restore #{SERVICES[service.to_sym][:project_file]} --disable-parallel
      dotnet publish -c Release
      if [ -d "./node_modules" ]
      then
        mkdir ./wwwroot/lib
        cp -R ./node_modules/** ./wwwroot/lib/
      fi
    SRV_SCRIPT
    if SERVICES[service.to_sym].key?(:database)
      db = SERVICES[service.to_sym][:database]
      connectionString = "#{DATABASES[db.to_sym][:connectionString]}"
      buildScript += <<-SRV_SCRIPT
        echo '{'                                                                  > "#{workingDir}/appsettings.local.json"
        echo '  "Connections": {'                                                >> "#{workingDir}/appsettings.local.json"
        echo '    "MaterialContext": {'                                          >> "#{workingDir}/appsettings.local.json"
        echo '      "Type": "postgres",'                                         >> "#{workingDir}/appsettings.local.json"
        echo '      "ConnectionString": "#{connectionString}Database=material;"' >> "#{workingDir}/appsettings.local.json"
        echo '    },'                                                            >> "#{workingDir}/appsettings.local.json"
        echo '    "PackContext": {'                                              >> "#{workingDir}/appsettings.local.json"
        echo '      "Type": "postgres",'                                         >> "#{workingDir}/appsettings.local.json"
        echo '      "ConnectionString": "#{connectionString}Database=pack;"'     >> "#{workingDir}/appsettings.local.json"
        echo '    },'                                                            >> "#{workingDir}/appsettings.local.json"
        echo '    "PlanContext": {'                                              >> "#{workingDir}/appsettings.local.json"
        echo '      "Type": "postgres",'                                         >> "#{workingDir}/appsettings.local.json"
        echo '      "ConnectionString": "#{connectionString}Database=plan;"'     >> "#{workingDir}/appsettings.local.json"
        echo '    },'                                                            >> "#{workingDir}/appsettings.local.json"
        echo '    "DrawingContext": {'                                           >> "#{workingDir}/appsettings.local.json"
        echo '      "Type": "postgres",'                                         >> "#{workingDir}/appsettings.local.json"
        echo '      "ConnectionString": "#{connectionString}Database=drawing;"'  >> "#{workingDir}/appsettings.local.json"
        echo '    }'                                                             >> "#{workingDir}/appsettings.local.json"
        echo '  }'                                                               >> "#{workingDir}/appsettings.local.json"
        echo '}'                                                                 >> "#{workingDir}/appsettings.local.json"
      SRV_SCRIPT
    end
    buildScript += <<-SRV_SCRIPT
      echo "[Unit]"                                                                                                                   > "/etc/systemd/system/#{service}.service"
      echo "Description=Example .NET Web API Application running on Ubuntu"                                                          >> "/etc/systemd/system/#{service}.service"
      echo ""                                                                                                                        >> "/etc/systemd/system/#{service}.service"
      echo "[Service]"                                                                                                               >> "/etc/systemd/system/#{service}.service"
      echo "WorkingDirectory=#{workingDir}"                                                                                          >> "/etc/systemd/system/#{service}.service"
      echo "ExecStart=/usr/bin/dotnet #{SERVICES[service.to_sym][:binary]} --urls http://*:#{SERVICES[service.to_sym][:guest_port]}" >> "/etc/systemd/system/#{service}.service"
      echo "Restart=always"                                                                                                          >> "/etc/systemd/system/#{service}.service"
      echo "RestartSec=60s"                                                                                                          >> "/etc/systemd/system/#{service}.service"
      echo "SyslogIdentifier=#{service}"                                                                                             >> "/etc/systemd/system/#{service}.service"
      echo "User=www-data"                                                                                                           >> "/etc/systemd/system/#{service}.service"
      echo "Environment=ASPNETCORE_ENVIRONMENT=Development"                                                                          >> "/etc/systemd/system/#{service}.service"
      echo "Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false"                                                                        >> "/etc/systemd/system/#{service}.service"
      echo "Environment=DOTNET_CLI_TELEMETRY_OPTOUT=1"                                                                               >> "/etc/systemd/system/#{service}.service"
      echo ""                                                                                                                        >> "/etc/systemd/system/#{service}.service"
      echo "[Install]"                                                                                                               >> "/etc/systemd/system/#{service}.service"
      echo "WantedBy=multi-user.target"                                                                                              >> "/etc/systemd/system/#{service}.service"
      echo "server {"                                                                   > "/etc/nginx/sites-available/#{service}"
    SRV_SCRIPT

    if SERVICES[service.to_sym].key?(:host_port)
      buildScript += <<-SRV_SCRIPT
        echo "  listen #{SERVICES[service.to_sym][:host_port]};"                       >> "/etc/nginx/sites-available/#{service}"
        echo "  listen [::]:#{SERVICES[service.to_sym][:host_port]} default_server;"   >> "/etc/nginx/sites-available/#{service}"
      SRV_SCRIPT
    end
    buildScript += <<-SRV_SCRIPT
      echo "  location #{SERVICES[service.to_sym][:server_location]} {"                >> "/etc/nginx/sites-available/#{service}"
      echo "    proxy_pass http://localhost:#{SERVICES[service.to_sym][:guest_port]};" >> "/etc/nginx/sites-available/#{service}"
      echo "  }"                                                                       >> "/etc/nginx/sites-available/#{service}"
      echo "}"                                                                         >> "/etc/nginx/sites-available/#{service}"

      systemctl enable #{service}.service
      systemctl start #{service}.service

      ln -sf /etc/nginx/sites-available/#{service} /etc/nginx/sites-enabled/#{service}
    SRV_SCRIPT
  end

  buildScript += <<-SCRIPT
    service nginx start
  SCRIPT

  machine[:build_script] = buildScript
end

# All Vagrant configuration is done below. The "2" in Vagrant.configure
# configures the configuration version (we support older styles for
# backwards compatibility). Please don't change it unless you know what
# you're doing.
Vagrant.configure("2") do |config|
  # Every Vagrant development environment requires a box. You can search for
  # boxes at https://vagrantcloud.com/search.
  config.vm.box = "generic/ubuntu2004"
  config.vm.provider "virtualbox" do |vb|
    vb.cpus = 4
    vb.memory = 2048
    vb.customize ['modifyvm', :id, '--graphicscontroller', 'vmsvga']
  end
  config.vm.network "forwarded_port", guest: 80, host: 80

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

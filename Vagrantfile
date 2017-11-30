# -*- mode: ruby -*-
# vi: set ft=ruby :

SERVICES = {
  packit: {
    repo: "https://github.com/SimplyCodeUK/packer-strategy.git",
    build_dir: "PackIt/src/PackIt",
  },
  packitui: {
    repo: "https://github.com/SimplyCodeUK/packer-strategy.git",
    build_dir: "PackItUI/src/PackItUI"
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
  apt-get install git=1:2.7.4-0ubuntu1.3        -y
  apt-get install nginx=1.10.3-0ubuntu0.16.04.2 -y
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
          cd #{SERVICES_DIR}
          if cd #{service}; then git pull; else git clone #{SERVICES[service.to_sym][:repo]} #{service}; fi
          cd #{SERVICES_DIR}/#{service}/#{SERVICES[service.to_sym][:build_dir]}
          dotnet restore
          dotnet publish --configuration Release
        SCRIPT
      end

      # start nginx
      buildEnv += <<-SCRIPT
        service nginx restart
      SCRIPT

      node.vm.network "forwarded_port", guest: machine[:guest_port], host: machine[:host_port], id: "nginx"
      node.vm.provision "shell", inline: buildEnv
    end
  end
end

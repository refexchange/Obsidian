echo "=============== Obsidian Configuration Script for CentOS ============="
sudo yum install -y curl wget git libunwind libicu
echo "=============== Cloning git repository =================="
git clone https://github.com/ZA-PT/Obsidian.git

echo "=============== Configuring environment =================="
sudo cp ./Obsidian/configure_env/centos/mongodb-org-3.4.repo /etc/yum.repos.d/mongodb-org-3.4.repo

sudo wget https://dl.yarnpkg.com/rpm/yarn.repo -O /etc/yum.repos.d/yarn.repo
curl --silent --location https://rpm.nodesource.com/setup_8.x | bash -
sudo yum install -y nodejs npm yarn mongodb-org

curl -sSL -o dotnet.tar.gz https://go.microsoft.com/fwlink/?linkid=848821
sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/local/bin
rm dotnet.tar.gz

echo "=============== Starting database service ===================="
sudo service mongod start

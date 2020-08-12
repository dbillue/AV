# Author: Duane Billue
# Date: 2020-08-11
# Description: Docker Image / Container Management script

Clear

# Download MS docker image
docker pull mcr.microsoft.com/dotnet/core/sdk:3.1
docker pull mcr.microsoft.com/dotnet/core/aspnet:3.1

# List local image
Clear
Docker images

# Build docker image
docker build -t familyapp .

# Start container
# docker run --rm -p 3000:3000 familyapp
# docker run --rm -p 8000:80 --name blazordock_container blazordock

# Stop image
Docker stop ddac733ff690

# Remove containers
docker rm c84db1136924

# Docker remove images
docker rmi mcr.microsoft.com/dotnet/nightly/sdk:5.0 # Repository:Tag

docker rmi e0b780badaf5 # Image Id
Clear
Docker images

# Tag image
docker tag 11146ae7177c duanebillue2020/nodejs:v1

# Push image to registry
docker push duanebillue2020/nodejs:v1

# IpConfig of container
docker exec blazordock ipconfig
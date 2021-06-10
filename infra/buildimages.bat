cd ..
docker build . -f .\Dockerfile.be -t patuz.azurecr.io/be:%1
docker push patuz.azurecr.io/be:%1
docker build . -f .\Dockerfile.bffe -t patuz.azurecr.io/bffe:%1
docker push patuz.azurecr.io/bffe:%1

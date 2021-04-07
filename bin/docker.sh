#!/usr/bin/env bash
set -euxo pipefail 
account=$1
region=$2
version=$3
docker build -t \
  "$account".dkr.ecr."$region".amazonaws.com/mona-app: \
  "$version" . \

docker push \
  "$account".dkr.ecr."$region".amazonaws.com/mona-app: \
  "$version"
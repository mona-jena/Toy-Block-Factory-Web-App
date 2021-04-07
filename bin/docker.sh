#!/usr/bin/env bash
set -euxo pipefail 
docker build -t \
  138666658526.dkr.ecr.ap-southeast-2.amazonaws.com/mona-app: \
  "${BUILDKITE_BUILD_NUMBER}" . \

docker push \
  138666658526.dkr.ecr.ap-southeast-2.amazonaws.com/mona-app: \
  "${BUILDKITE_BUILD_NUMBER}"
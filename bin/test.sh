#!/usr/bin/env bash
set -euxo pipefail
docker run --rm -it \
  -v "$(pwd):/source" -w /source \
  mcr.microsoft.com/dotnet/sdk:5.0 dotnet test
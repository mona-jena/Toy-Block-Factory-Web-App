#!/usr/bin/env bash
set -euxo pipefail
version=$1
ktmpl deployment.yml -p imagetag "$version" \
  | kubectl apply -f - -n fma

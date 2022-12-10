#!/usr/bin/env bash

configuration=$1

dotnet restore
dotnet build

mkdir -p ./release-dir/

function publish() {
  local configuration=$1
  local rid=$2
  local publishArgs=" -c $configuration -o ./clasharp-$rid/ -r $rid -p:PublishSingleFile=true -p:PublishTrimmed=true -p:PublishReadyToRun=true"
  if [ "$configuration" == "Debug" ]; then
      publishArgs=" -c $configuration -o ./clasharp-$rid/ -r $rid"
  fi
  # shellcheck disable=SC2086
  dotnet publish Clasharp/Clasharp.csproj $publishArgs
  # shellcheck disable=SC2086
  dotnet publish Clasharp.Service/Clasharp.Service.csproj $publishArgs
  
  zip -r ./release-dir/clasharp-"$rid"-"$configuration".zip clasharp-"$rid"
}

publish "$configuration" win-x64
publish "$configuration" linux-x64

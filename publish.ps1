param([string]$configuration)

$ErrorActionPreference = "Stop"

dotnet restore
dotnet build

mkdir -Force ./release-dir/

function publish([string]$configuration, [string]$rid)
{
    if ($configuration -eq "Debug")
    {
        dotnet publish Clasharp/Clasharp.csproj -c $configuration -o ./clasharp-$rid/ -r $rid
        dotnet publish Clasharp.Service/Clasharp.Service.csproj -c $configuration -o ./clasharp-$rid/ -r $rid
    }
    else
    {
        dotnet publish Clasharp/Clasharp.csproj -c $configuration -o ./clasharp-$rid/ -r $rid -p:PublishSingleFile=true -p:PublishTrimmed=true -p:PublishReadyToRun=true
        dotnet publish Clasharp.Service/Clasharp.Service.csproj -c $configuration -o ./clasharp-$rid/ -r $rid -p:PublishSingleFile=true -p:PublishTrimmed=true -p:PublishReadyToRun=true
    }
    
    Compress-Archive -Force -Path clasharp-"$rid" -DestinationPath ./release-dir/clasharp-"$rid"-"$configuration".zip
}

publish -configuration $configuration -rid "win-x64"
publish -configuration $configuration -rid "linux-x64"


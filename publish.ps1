param([string]$configuration)

$ErrorActionPreference = "Stop"


Remove-Item -Recurse -Force ./release-dir/ -ErrorAction Ignore
Remove-Item -Recurse -Force ./clasharp-win-x64/ -ErrorAction Ignore
Remove-Item -Recurse -Force ./clasharp-linux-x64/ -ErrorAction Ignore
New-Item -Force -Path ./release-dir/ -Type Directory

dotnet restore
dotnet build

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
    & 7z a -tzip  "release-dir/clasharp-${rid}-${configuration}.zip" "clasharp-${rid}" -aoa
#    Compress-Archive -Force -Path clasharp-"$rid" -DestinationPath ./release-dir/clasharp-"$rid"-"$configuration".zip
}

publish -configuration $configuration -rid "win-x64"
publish -configuration $configuration -rid "linux-x64"


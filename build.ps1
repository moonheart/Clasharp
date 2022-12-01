param([string]$configuration='Release')
        
dotnet restore
dotnet build

dotnet publish Clasharp/Clasharp.csproj -c $configuration -o ./publish-win/ -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true
dotnet publish Clasharp.Service/Clasharp.Service.csproj -c $configuration -o ./publish-win/ -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true

dotnet publish Clasharp/Clasharp.csproj -c $configuration -o ./publish-linux/ -r linux-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true
dotnet publish Clasharp.Service/Clasharp.Service.csproj -c $configuration -o ./publish-linux/ -r linux-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true


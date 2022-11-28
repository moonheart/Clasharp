param([string]$configuration='Release')

dotnet build

dotnet publish ClashGui/ClashGui.csproj -c $configuration -o ./publish-win/ -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true
dotnet publish ClashGui.WindowsService/ClashGui.WindowsService.csproj -c $configuration -o ./publish-win/ -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true

dotnet publish ClashGui/ClashGui.csproj -c $configuration -o ./publish-linux/ -r linux-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true
dotnet publish ClashGui.WindowsService/ClashGui.WindowsService.csproj -c $configuration -o ./publish-linux/ -r linux-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true


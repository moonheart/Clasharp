param([string]$configuration='Release')

dotnet build

dotnet publish ClashGui/ClashGui.csproj -c $configuration -o ./publish/ -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true

dotnet publish ClashGui.WindowsService/ClashGui.WindowsService.csproj -c $configuration -o ./publish/ -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true


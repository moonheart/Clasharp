param([string]$configuration='Release', [string]$rid='win-x64')
        
dotnet restore
dotnet build

dotnet publish Clasharp/Clasharp.csproj -c $configuration -o ./publish/ -r $rid -p:PublishSingleFile=true -p:PublishTrimmed=true
dotnet publish Clasharp.Service/Clasharp.Service.csproj -c $configuration -o ./publish/ -r $rid -p:PublishSingleFile=true -p:PublishTrimmed=true



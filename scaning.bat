dotnet sonarscanner begin /k:"dotnet_manager" /d:sonar.host.url="http://localhost:49153"  /d:sonar.login="621701f6ecb8b1c1fb758c6548422b9eea135cdb"
dotnet build
dotnet tool update --global dotnet-reportgenerator-globaltool
dotnet test Services.UnitTests/Services.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover -l trx
reportgenerator "-reports:*\TestResults\*\coverage.opencover.xml" "-targetdir:sonarqubecoverage" "-reporttypes:SonarQube"
dotnet sonarscanner end /d:sonar.login="621701f6ecb8b1c1fb758c6548422b9eea135cdb"
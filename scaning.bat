dotnet sonarscanner begin /k:"manager" /d:sonar.host.url="http://localhost:49153"  /d:sonar.login="8c3129914185f7bdb3c1628eb6bc77878a0fa56c"
dotnet build
dotnet test Services.UnitTests/Services.UnitTests.csproj --no-build --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover
reportgenerator "-reports:*\TestResults\*\coverage.opencover.xml" "-targetdir:sonarqubecoverage" "-reporttypes:SonarQube"
dotnet sonarscanner end /d:sonar.login="8c3129914185f7bdb3c1628eb6bc77878a0fa56c"
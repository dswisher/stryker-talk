
.PHONY: test watch report mutants clean

test:
	dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

watch:
	dotnet watch test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

report:
	dotnet reportgenerator -reports:coverage.opencover.xml -targetdir:coveragereport

mutants:
	dotnet stryker --log-file

clean:
	dotnet clean
	rm -rf StrykerOutput coveragereport coverage.opencover.xml


# vh-test-web
Test Web is a test data and user management application to help manual testing and development.  

## Home
<img width="1145" alt="Home hearing list" src="https://user-images.githubusercontent.com/10450246/114573088-5b7f2580-9c78-11eb-8264-d17efb74fbbb.png">

## Create Data
<img width="1043" alt="Create Hearing" src="https://user-images.githubusercontent.com/10450246/114573131-65088d80-9c78-11eb-954b-2323874c2e27.png">

## Delete Data
<img width="1027" alt="Delete Hearing" src="https://user-images.githubusercontent.com/10450246/114573167-6df95f00-9c78-11eb-930b-936c28fceb8c.png">

## Events
<img width="1113" alt="Events (with Transfers)" src="https://user-images.githubusercontent.com/10450246/114573329-941eff00-9c78-11eb-8d30-13e6be34be48.png">

## Allocating a Test User
<img width="1048" alt="Allocate users" src="https://user-images.githubusercontent.com/10450246/114573383-a0a35780-9c78-11eb-805b-122123e28c63.png">

## Updating client code via NSwag

The project is utilising NSwag to auto-generate client code for the front-end.

### Updating the front end Angular client code

The configuration for the front end TypeScript can be found in 'TestWeb/ClientApp/api-ts.nswag'

- Install the NSwag CLI (at least version 12)
- Install Dotnet SDK 2.2
- Ensure the MVC application is running. This can be managed by either:
  - `dotnet run TestWeb/TestWeb.csproj`
  - or via an IDE
- open a terminal at the folder containing the nswag file 'TestWeb/ClientApp'
- execute `nswag run`

The latest version of the client code can be found in 'src/app/services/clients/api-client.ts'

## Running code coverage

First ensure you are running a terminal in the TestWeb directory of this repository and then run the following commands.

```bash
dotnet test --no-build TestWeb.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat="\"opencover,cobertura,json,lcov\"" /p:CoverletOutput=../Artifacts/Coverage/ /p:MergeWith='../Artifacts/Coverage/coverage.json' /p:Exclude="\"[TestWeb.ConfigureServicesExtensions,[TestWeb]TestWeb.Program,[TestWeb]TestWeb.Startup,[*]TestWeb.Common.*,[*]TestWeb.Extensions.*,[*]TestWeb.Pages.*,[*]TestWeb.Swagger.*,[*]TestWeb.Views.*,[*]TestWeb.UnitTests.*,[*]TestWeb.Services.*,[*]Testing.Common.*\""

```
Using Poweshell

```
dotnet test --% --no-build TestWeb.UnitTests/TestWeb.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=\"opencover,cobertura,json,lcov\" /p:CoverletOutput=../Artifacts/Coverage/ /p:MergeWith='../Artifacts/Coverage/coverage.json' /p:Exclude=\"[TestWeb.ConfigureServicesExtensions,[TestWeb]TestWeb.Program,[TestWeb]TestWeb.Startup,[*]TestWeb.Common.*,[*]TestWeb.Extensions.*,[*]TestWeb.Pages.*,[*]TestWeb.Swagger.*,[*]TestWeb.Views.*,[*]TestWeb.UnitTests.*,[*]TestWeb.Services.*,[*]Testing.Common.*\"
```

## Generate HTML Report

Under the unit test project directory

```bash
dotnet reportgenerator "-reports:../Artifacts/Coverage/coverage.opencover.xml" "-targetDir:../Artifacts/Coverage/Report" -reporttypes:HtmlInline_AzurePipelines
```

## Linting

Verify the source code passes linting. To quickly fix linting issues, execute the following command from the 'ClientApp' directory in a terminal

```bash
ng lint TestWeb --fix
```

## Styling Client App

Verify the source code passes linting. To quickly identify linting issues, execute the following command from the 'ClientApp' directory in a terminal

```bash
npm install
npm run prettify
```

### Applying Client App Styles

Use the command below to apply prettier after installation packages

```bash
npm run prettify-fix
```

## Running Tests with Code Coverage with VS Code

### Install Coverage Gutters

Install the extension : Coverage Gutters
Id: ryanluker.vscode-coverage-gutters

This extension will load covage files and display in real-time which lines are covered

### Run the Test task

Ensure you have a terminal with the current directory set to the same level as angular workspace.

```bash
npm run test
```

This will execute the angular tests files with the --code-coverage parameter. Once the coverage files have been produced, enable the watch command for Coverage Gutters.

### Branch name git hook will run on pre commit and control the standard for new branch name.

The branch name should start with: feature/VIH-XXXX-branchName (X - is digit).
If git version is less than 2.9 the pre-commit file from the .githooks folder need copy to local .git/hooks folder.
To change git hooks directory to directory under source control run (works only for git version 2.9 or greater) :
\$ git config core.hooksPath .githooks

## Commit message

The commit message will be validated by prepare-commit-msg hook.
The commit message format should start with : 'feature/VIH-XXXX : ' folowing by 8 or more characters description of commit, otherwise the warning message will be presented.

## Run Zap scan locally

To run Zap scan locally update the following settings and run acceptance tests

User Secrets:

- "VhServices:TestWebUrl": "https://testweb_ac/"

Update following configuration under TestWeb/appsettings.json

- "AzureAd:RedirectUri": "https://testweb_ac/home"
- "AzureAd:PostLogoutRedirectUri": "https://testweb_ac/logout"
- "ZapScan": true

Update following configuration under TestWeb.AcceptanceTests/appsettings.json

- "AzureAd:RedirectUri": "https://testweb_ac/home"
- "AzureAd:PostLogoutRedirectUri": "https://testweb_ac/logout"
- "ZapConfiguration:ZapScan": true

## Run Stryker

To run stryker mutation test, go to UnitTest folder under command prompt and run the following command

```bash
dotnet stryker
```

From the results look for line(s) of code highlighted with Survived\No Coverage and fix them.


If in case you have not installed stryker previously, please use one of the following commands

### Global
```bash
dotnet tool install -g dotnet-stryker
```
### Local
```bash
dotnet tool install dotnet-stryker
```

To update latest version of stryker please use the following command

```bash
dotnet tool update --global dotnet-stryker
```

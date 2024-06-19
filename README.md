## Application URLs - DEVELOPMENT:

- Identity API: http://localhost:5001

## Docker Command Examples

- Create a ".env" file at the same location with docker-compose.yml file:
  COMPOSE_PROJECT_NAME=e_microservices_idp
- run command: docker-compose -f docker-compose.yml up -d --remove-orphans --build
- Run script: idp_stores.sql in DatabaseScripts/Store Procedures



`add-migration InitialPersistedGrantMigration -c PersistedGrantDbContext -o Migration/s/IdentityServer/PersistedGrantDb`
` add-migration InitialConfigurationMigration -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb`

## Application URLs - PRODUCTION:

## Packages References

- https://github.com/serilog/serilog/wiki/Getting-Started
- https://github.com/IdentityServer/IdentityServer4.Quickstart.UI

## Install Environment

## Migrations commands (cd into microservice.IDP project):

- migration PersistedGrantDbContext: `dotnet ef migrations add "Initial_PersistedGrantDb" -c PersistedGrantDbContext -s Microservices.IDP.csproj -o Persistence/Migrations/PersistedGrantDb`
- migration ConfigurationDbContext: `dotnet ef migrations add "Initial_ConfigurationDb" -c ConfigurationDbContext -s Microservices.IDP.csproj -o Persistence/Migrations/ConfigurationDb`
- update db PersistedGrantDbContext: `dotnet ef database update -c PersistedGrantDbContext`
- update db ConfigurationDbContext: `dotnet ef database update -c ConfigurationDbContext`
- dotnet ef migrations add "Init_Identity" -c IdentityContext -s Microservices.IDP.csproj -p ../Microservices.IDP.Infrastructure/Microservices.IDP.Infrastructure.csproj -o Persistence/Migrations
- dotnet ef database update -c IdentityContext -s Microservices.IDP.csproj -p ../Microservices.IDP.Infrastructure/Microservices.IDP.Infrastructure.csproj

## Useful commands:

- Docker build (root folder): docker build -t e_microservice_idp:latest -f src/Microservices.IDP/Dockerfile src/.
- Update migration (root folder):
  - `dotnet ef database update -c PersistedGrantDbContext -s src/Microservices.IDP/Microservices.IDP.csproj --connection "${connection_string}"`
  - `dotnet ef database update -c ConfigurationDbContext -s src/Microservices.IDP/Microservices.IDP.csproj --connection "${connection_string}"`
  - `dotnet ef database update -c IdentityContext -p src/Microservices.IDP.Infrastructure/Microservices.IDP.Infrastructure.csproj -s src/Microservices.IDP/Microservices.IDP.csproj --connection "${connection_string}"`

## https certificate with Docker Compose:

- Create a .pfx file:
  - WindowsOS: dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\e-idp.pfx -p password!
- Trust the file: dotnet dev-certs https --trust

## Docker hub:
- docker login: docker login
- Go to docker hub, create a new repository: e-microservice-idp
- At root folder:
  - `docker build -t {your_docker_account}/e-microservice-idp:latest -f src/Microservices.IDP/Dockerfile src/.`
  - `docker push {your_docker_account}/e-microservice-idp:latest`
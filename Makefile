# Set default shell
SHELL := /bin/bash

rename-project:
	@echo "Making the Script Executable"
	chmod +x scripts/rename_dotnet_template.sh
	@echo "Executing rename script with name: $(name)"
	scripts/./rename_dotnet_template.sh $(name)

install-dotnet-ef:
	dotnet tool install --local dotnet-ef

# Run the API project
run:
	dotnet run --project src/Courta.WebAPI

# Run the API project
watch:
	dotnet watch --project src/Courta.WebAPI

# Build the solution
build:
	dotnet build

# Restore NuGet packages
restore:
	dotnet restore

# Clean the project
clean:
	dotnet clean

# Create and apply database migrations (for EF Core)
migrate:
	dotnet ef migrations add $(name) --project src/Courta.Infrastructure --startup-project src/Courta.WebAPI

database-update:
	dotnet ef database update --project src/Courta.Infrastructure --startup-project src/Courta.WebAPI

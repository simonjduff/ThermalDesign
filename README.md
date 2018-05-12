# Thermal design
## Setup
Install [dotnet core](https://www.microsoft.com/net/learn/get-started/windows)

Run the following commands:
1. dotnet restore
2. dotnet build
3. dotnet run --project src\TermalDesign.App\TermalDesign.App.csproj

## Input needed
### Fitness Function
This must return a value representing how good the solution is.
This will have to account for all scenarios for a given set of insulation values.
This will have to account for all constraints.
This function will replace the placeholder in ThermalFitness.cs.

### Insulation ranges
Each of the seven segments must have a min and max insulation integer defined.
These are defined in ThermalGenome.cs currently.
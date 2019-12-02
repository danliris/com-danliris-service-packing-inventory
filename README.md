# com-danliris-service-packing-inventory

## command to add migration and update database

go to root directory of the project before run these command
dotnet ef migrations add <CHANGE_ME_WITH_THE_NAME_OF_YOUR_MIGRATION> -o ../Com.Danliris.Service.Packing.Inventory.WebApi/Migrations -p src/Com.Danliris.Service.Packing.Inventory.Infrastructure/ -c PackingInventoryDbContext -s src/Com.Danliris.Service.Packing.Inventory.WebApi/
dotnet ef database update -p src/Com.Danliris.Service.Packing.Inventory.Infrastructure/ -c PackingInventoryDbContext -s src/Com.Danliris.Service.Packing.Inventory.WebApi/
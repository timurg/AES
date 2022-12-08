# Install Tools

dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design

# Go to project

cd AES.Infrastructure.EntityFrameworkCore.PostgreSql

# Update

dotnet ef migrations add name<BR>
dotnet ef database update
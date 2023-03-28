# Install Tools

    dotnet tool install --global dotnet-ef
    dotnet add package Microsoft.EntityFrameworkCore.Design

# Go to project

    cd AES.Infrastructure.EntityFrameworkCore.PostgreSql
    cd AES.Infrastructure.EntityFrameworkCore.Sqlite

# Update

    dotnet ef migrations add name
    dotnet ef database update
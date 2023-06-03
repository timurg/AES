FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY ["./bin/Release/net6.0/publish/", "."]
RUN ["dotnet", "--version"]
CMD ["dotnet", "/app/MyStoryBot.dll"]
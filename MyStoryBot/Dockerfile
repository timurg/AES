FROM mcr.microsoft.com/dotnet/runtime:8.0

WORKDIR /app

# Копируем собранное приложение
COPY /bin/Release/net8.0/publish/ .

# Создаем директории
RUN mkdir -p /app/config /app/data

# Монтируем конфиг
VOLUME ["/app/config", "/app/data"]

# Задаем точку входа с указанием конфига
ENTRYPOINT ["dotnet", "MyStoryBot.dll", "--config", "/app/config/appsettings.json"]
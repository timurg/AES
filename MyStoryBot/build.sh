# Скрипт сборки и запуска
#!/bin/bash
# filepath: build.sh

# Публикация приложения локально
dotnet publish -c Release

# Сборка Docker образа
docker build -t mystorybot .

# Запуск контейнера
docker run -v $(pwd)/../Config:/app/config -v $(pwd)/../Data:/app/data mystorybot
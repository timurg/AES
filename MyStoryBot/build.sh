#!/bin/bash

# Публикация приложения локально
dotnet publish -c Release

# Остановка и удаление старого контейнера если существует
docker rm -f mystorybot_instance || true

# Удаление старого образа
docker rmi mystorybot || true

# Сборка Docker образа
docker build -t mystorybot .

# Очистка неиспользуемых образов
docker image prune -f

# Запуск нового контейнера с именем
docker run \
  --name mystorybot_instance \
  -v $(pwd)/../Config:/home/app/config:ro \
  -v $(pwd)/../Data:/home/app/data:rw \
  --restart unless-stopped \
  -d \
  mystorybot
FROM mcr.microsoft.com/dotnet/runtime:8.0

# Создаем директории
WORKDIR /home/app

# Настраиваем директории и права
RUN mkdir -p /home/app/config /home/app/data && \
    chown -R app:app /home/app && \
    chmod 755 /home/app && \
    chmod 775 /home/app/data

# Копируем приложение
COPY --chown=app:app /bin/Release/net8.0/publish/ .

# Настраиваем тома
VOLUME ["/home/app/config", "/home/app/data"]

# Переключаемся на пользователя app
USER app

ENTRYPOINT ["dotnet", "MyStoryBot.dll"]
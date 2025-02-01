FROM mcr.microsoft.com/dotnet/sdk:8.0

# Configuración de variables de entorno
ENV PATH="$PATH:/root/.dotnet/tools"
ENV VERSION="0.0.1"

# Definir el directorio de trabajo dentro del contenedor
WORKDIR /src

# Copiar todo el código fuente al contenedor
COPY . .

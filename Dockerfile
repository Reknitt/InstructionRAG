FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj .
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0.2-bookworm-slim-amd64 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
COPY ./model.gguf .

RUN apt update && apt install -y libgomp1 

ENTRYPOINT ["dotnet", "InstructionRAG.dll"]

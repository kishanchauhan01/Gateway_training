FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /out

# Multistage build
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS final

# same working DIR for both builds
WORKDIR /app

COPY --from=build /out .

ENTRYPOINT [ "dotnet", "LinqPractice.dll" ]

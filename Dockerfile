FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MentorStudent.Api/MentorStudent.Api.csproj", "MentorStudent.Api/"]
COPY ["MentorStudent.Application/MentorStudent.Application.csproj", "MentorStudent.Application/"]
COPY ["MentorStudent.Domain/MentorStudent.Domain.csproj", "MentorStudent.Domain/"]
COPY ["MentorStudent.Infrastructure/MentorStudent.Infrastructure.csproj", "MentorStudent.Infrastructure/"]

RUN dotnet restore "MentorStudent.Api/MentorStudent.Api.csproj"

COPY . .


WORKDIR "/src/MentorStudent.Api"
RUN dotnet build "MentorStudent.Api.csproj" -c Release -o /app/build

RUN dotnet publish "MentorStudent.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MentorStudent.Api.dll"]
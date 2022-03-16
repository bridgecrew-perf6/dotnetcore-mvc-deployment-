FROM utarn/aspnetcore5-centos8:latest AS builder 
WORKDIR /src
COPY dacon_exam.csproj.csproj . 
RUN dotnet restore dacon_exam.csproj.csproj
COPY . .
WORKDIR /src
RUN dotnet publish --output /app --configuration Release

FROM utarn/aspnetcore5-centos8-runtime:latest
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "dacon_exam.dll"]
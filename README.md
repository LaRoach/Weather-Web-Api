# WeatherWebApi
ASP .NET Core web api to register , login , view profile and call external weather data

### build local
dotnet restore
dotnet build
dotnet run


## Environment variables
| Name                           | Required (yes/no) | Default value                                                            | Description        |
| ------------------------------ | ----------------- | -------------------------------------------------------------------------| ------------------ |
| DbConnection                   | no                | server=127.0.0.1;port=3306;uid=root;pwd=password;database=mydb(local dev)| DbConnectionString |

## ⚙️ Deployment
Build Docker Image
```lang-bash
docker build -t weatherapi .
```
Run Docker Image (with env variables)
```lang-bash
docker run -p 5005:5005 --env DbConnection="server=host.docker.internal;port=3306;uid=root;pwd=password;database=testdb" --name weatherwebapi weatherwebapi
```

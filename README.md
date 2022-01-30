## Docker Sql Server
```sh
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=!Manager010203@#" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU15-ubuntu-20.04

```

```sh
CREATE TABLE master.dbo.Usuario (
	Id bigint IDENTITY(0,1) NOT NULL,
	Nome varchar(60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email varchar(180) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DataCriacao datetime2(0) NOT NULL,
	DataAtualizacao datetime2(0) NULL
);
```

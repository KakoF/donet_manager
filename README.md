# Exemplo para estudo
## Docker (Sql Server e Redis)

Arquivo composer irá inicializar as dependências de infra, rodar o comando na raíz onde o arquivo se encontra:
```sh
docker-compose up -d

```

## Script
script para criar a tabela de Usuário
```sh
CREATE TABLE master.dbo.Usuario (
	Id bigint IDENTITY(1,1) NOT NULL,
	Nome varchar(60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email varchar(180) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DataCriacao datetime2(0) NOT NULL,
	DataAtualizacao datetime2(0) NULL
);

```
## Script Integração
script para criar a tabela de Usuário
```sh
CREATE DATABASE master_integration_test

CREATE TABLE master_integration_test.dbo.Usuario (
	Id bigint IDENTITY(1,1) NOT NULL,
	Nome varchar(60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email varchar(180) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DataCriacao datetime2(0) NOT NULL,
	DataAtualizacao datetime2(0) NULL
);

```

## Dependências e execução do projeto
Aqruivo appsettings já aponta para a infra que docker irá subir
```sh
- Executar o restore ou build da aplicação para instalação das dependências

- Rodar o projeto
```


## Coverage
```
dotnet tool install -g dotnet-reportgenerator-globaltool  

reportgenerator -reports:".\Services.UnitTests\TestResults\f41cb602-de29-4be6-a250-76acd941ed85\coverage.cobertura.xml" -targetdir:"coverageresults" -reporttypes:Html
```
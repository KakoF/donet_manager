# Exemplo para estudo
## Docker (Sql Server e Redis)

Arquivo composer ir� inicializar as depend�ncias de infra, rodar o comando na ra�z onde o arquivo se encontra:
```sh
docker-compose up -d

```

## Script
script para criar a tabela de Usu�rio
```sh
CREATE TABLE master.dbo.Usuario (
	Id bigint IDENTITY(0,1) NOT NULL,
	Nome varchar(60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email varchar(180) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DataCriacao datetime2(0) NOT NULL,
	DataAtualizacao datetime2(0) NULL
);
```

## Depend�ncias e execu��o do projeto
Aqruivo appsettings j� aponta para a infra que docker ir� subir
```sh
- Executar o restore ou build da aplica��o para instala��o das depend�ncias

- Rodar o projeto
```

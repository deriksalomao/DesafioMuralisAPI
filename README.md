# Desafio Técnico C# - API de Gerenciamento de Clientes

Este projeto é uma solução para o desafio técnico backend proposto pela Muralis, com o objetivo de construir uma API RESTful para o gerenciamento de clientes.

## Descrição

A API permite realizar o ciclo completo de operações CRUD (Criar, Ler, Atualizar e Deletar) para clientes, seus endereços e contatos. Um dos principais requisitos do desafio é a integração com o serviço [ViaCep](https://viacep.com.br/) para buscar e validar os dados de endereço a partir do CEP fornecido, garantindo a consistência dos dados.

O projeto foi desenvolvido seguindo as melhores práticas de arquitetura de software, como a separação de responsabilidades em camadas (Controllers, Services, Data) e o uso de DTOs (Data Transfer Objects) para o contrato da API.

## Tecnologias Utilizadas

* **.NET 8:** Versão mais recente com Suporte de Longo Prazo (LTS) da plataforma .NET.
* **ASP.NET Core:** Framework para a construção da API RESTful.
* **Entity Framework Core:** ORM para o mapeamento objeto-relacional e comunicação com o banco de dados.
* **PostgreSQL:** Banco de dados relacional robusto e open-source.
* **AutoMapper:** Biblioteca para automatizar a conversão entre DTOs e modelos de domínio.
* **Swagger (Swashbuckle):** Para documentação interativa da API.

## Funcionalidades

* Cadastro, consulta, atualização, listagem e exclusão de Clientes.
* Pesquisa de clientes por nome.
* Validação e preenchimento automático de endereço (Logradouro e Cidade) através da consulta ao CEP na API do ViaCep.
* Tratamento de erros centralizado para respostas padronizadas da API.

## Como Executar o Projeto

Siga os passos abaixo para configurar e executar o projeto em um ambiente local.

### 1. Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* Uma instância do [PostgreSQL](https://www.postgresql.org/download/) em execução.

### 2. Clone o Repositório

```bash
git clone <URL_DO_SEU_REPOSITORIO_NO_GITHUB>
cd <NOME_DA_PASTA_DO_PROJETO>
```

### 3. Configure o `appsettings.json`

Para que a API se conecte ao seu banco de dados PostgreSQL, é preciso configurar a string de conexão no arquivo `Muralis.Desafio.Api/appsettings.json`. O arquivo deve ter a seguinte estrutura, com credenciais de teste para um ambiente de desenvolvimento local:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=desafio_muralis_db;Username=postgres;Password=sua_senha_aqui"
  }
}
```

**Instruções para o avaliador:**
* **Host**: Mantenha `localhost` se o PostgreSQL estiver rodando na mesma máquina.
* **Database**: `desafio_muralis_db` é o nome sugerido para o banco de dados. O Entity Framework irá criá-lo se não existir.
* **Username**: O usuário do PostgreSQL (o padrão geralmente é `postgres`).
* **Password**: **Substitua `"sua_senha_aqui"`** pela senha que você configurou para o seu usuário do PostgreSQL.

### 4. Aplique as Migrations

Com a conexão configurada, o próximo passo é criar a estrutura de tabelas no banco de dados. Isso é feito com o comando de migrations do Entity Framework, que lê os arquivos da pasta `Migrations`.

Abra um terminal na pasta do projeto (`Muralis.Desafio.Api`) e execute:

```bash
dotnet ef database update
```
Este comando irá ler o histórico de alterações e aplicar o schema mais recente ao banco de dados.

### 5. Execute a API

Com o banco de dados pronto, inicie a aplicação com o seguinte comando:

```bash
dotnet run
```

A API estará em execução. A documentação interativa do Swagger estará disponível no endereço indicado no terminal (ex: `https://localhost:7039/swagger`), conforme configurado no `launchSettings.json`.

## Estrutura da API (Endpoints)

| Verbo HTTP | Rota | Descrição |
| :--- | :--- | :--- |
| `POST` | `/api/Clientes` | Cria um novo cliente. |
| `GET` | `/api/Clientes` | Lista todos os clientes cadastrados. |
| `GET` | `/api/Clientes/{id}` | Obtém os detalhes de um cliente por seu ID. |
| `PUT` | `/api/Clientes/{id}` | Atualiza os dados de um cliente existente. |
| `DELETE` | `/api/Clientes/{id}` | Remove um cliente do sistema. |
| `GET` | `/api/Clientes/pesquisar`| Pesquisa e retorna clientes por parte do nome (`?nome=...`). |
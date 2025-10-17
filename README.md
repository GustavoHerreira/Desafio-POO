## Pré-requisitos
SDK do .NET 9

## Instruções de Execução
Para configurar e executar o projeto localmente, siga os passos abaixo no seu terminal, a partir da pasta raiz do projeto.

### 1. Restaurar as Dependências
Execute o comando abaixo para baixar e instalar todos os pacotes NuGet necessários para o projeto.

```
dotnet tool install --global dotnet-ef
dotnet restore
```

### 2. Aplicar as Migrations ao Banco de Dados
Este comando irá criar o banco de dados (SQLite, por padrão) e aplicar o schema definido, incluindo a tabela Imoveis.

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Executar a Aplicação
Após a criação bem-sucedida do banco de dados, inicie o servidor da API.

```
dotnet run
```

A API estará em execução e pronta para receber requisições nos endereços indicados no terminal.

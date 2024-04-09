# Aquivo base para seguir um fluxo de desenvolvimento do CRUD de clientes

Informações essenciais:

- Connection String: `Server=tcp:xva-server.database.windows.net,1433;Initial Catalog=xva-database;Persist Security Info=False;User ID=xva-db-integrator;Password={senha};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;`
- Tabela cliente: id, name, phone, email
- versão copilot: v1.178.0
- Docker Engine tem que estar ligada!!

## Milestones

1. Criação do projeto via command line
2. Implementação da model Cliente
3. Criação do contrato do repositório
4. Implementação do contrato do repositório
5. Implementação da interface `IDisposable` no repositório
6. Implementação da controller de cliente
7. Configuração das dependências na classe `Program`
8. Configuração do `Swagger`

Adicionais

1. Geração de uma `Dockerfile`
2. build, tag e push no Registry
3. criação do arquivo `Deployment.yaml`
4. criação do arquivo `Service.yaml`, o tipo de serviço é um `ExternalIp`
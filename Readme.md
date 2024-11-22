# EnergyConsumptionAPI: A Solução Inteligente para Otimização do Consumo de Energia

Bem-vindo ao repositório da **EnergyConsumptionAPI**, uma aplicação robusta desenvolvida para monitorar, analisar e otimizar o consumo de energia elétrica em residências. Esta API visa oferecer uma solução escalável e inteligente, utilizando os Princípios de Arquitetura de Software, como modularidade, escalabilidade e separação de responsabilidades. A EnergyConsumptionAPI integra tecnologias modernas de design de software e inteligência artificial para proporcionar insights às pessoas sobre como reduzir o consumo de energia de maneira eficiente e sustentável.

## Características do Projeto

1. **Princípios de Arquitetura de Software**

   - **Modularidade**: Cada módulo foi cuidadosamente separado para garantir simplicidade e reutilização de código. As responsabilidades foram divididas em controllers, serviços, modelos e camadas de inteligência artificial para maximizar a manutenção e extensibilidade.
   - **Escalabilidade**: A API foi projetada para ser escalável, utilizando padrões de projeto como Factory e Repository, facilitando a expansão do sistema para novos recursos e integrações.
   - **Separação de Responsabilidades**: Adotamos uma arquitetura limpa (clean architecture), que separa logicamente os componentes em camadas, promovendo um código organizado e de fácil manutenção.

2. **Design Patterns Implementados**

   - **Repository Pattern**: O uso do Repository Pattern permitiu a abstração das operações de banco de dados, facilitando o desacoplamento da lógica de negócios da camada de persistência.
   - **Factory Pattern**: Aplicado para a criação dinâmica de objetos complexos, garantindo que cada componente seja instanciado com suas dependências necessárias.
   - **Dependency Injection**: Usado para promover a inversão de controle, facilitando a manutenção do código e a criação de testes automatizados.

3. **Documentação e Testes Automatizados**

   - **Swagger**: Toda a API foi documentada utilizando Swagger, permitindo uma exploração intuitiva dos endpoints, facilitando a compreensão e integração por terceiros.
   - **Testes Automatizados com xUnit e Moq**: A aplicação é coberta por testes unitários utilizando xUnit e Moq. Isso garante a qualidade e robustez dos endpoints, validando a funcionalidade e prevenindo regressão de código.

4. **Qualidade de Código e Análise Estática**

   - **Boas Práticas de Desenvolvimento**: O código segue os padrões e boas práticas de desenvolvimento, com nomenclatura consistente, métodos bem documentados e padrões de projeto adequados.
   - **Ferramentas de Análise Estática**: Ferramentas como **SonarQube** foram utilizadas para garantir a qualidade do código, verificando a presença de vulnerabilidades e códigos de baixa manutenção.

5. **Banco de Dados: MongoDB**

   - O projeto utiliza **MongoDB** como banco de dados, escolhido pela sua flexibilidade e facilidade de escalabilidade horizontal. Todas as operações de persistência foram desacopladas utilizando o Repository Pattern, permitindo a troca por outro banco de dados no futuro, como **Oracle**, de forma fácil e sem grande impacto.

6. **Inteligência Artificial Generativa com .NET**

   - Utilizando recursos de **ML.NET**, implementamos uma IA generativa para analisar o consumo de energia das residências. A IA é responsável por identificar padrões de consumo e sugerir formas de reduzi-lo, fornecendo recomendações personalizadas, como "Desligue aparelhos em standby" ou "Substitua lâmpadas convencionais por LEDs".
   - O modelo é treinado automaticamente utilizando dados dinâmicos do MongoDB, tornando a solução mais inteligente conforme novos dados são coletados.

## Estrutura do Projeto

```
EnergyConsumptionAPI/
├── Controllers/
│   ├── EnergyConsumptionController.cs
│   └── EnergyConsumptionAnalysisController.cs
├── Models/
│   ├── BaseEntity.cs
│   ├── EnergyConsumption.cs
│   └── EnergyConsumptionAnalysis.cs
├── DTOs/
│   └── EnergyConsumptionDTO.cs
├── Services/
│   ├── EnergyConsumptionService.cs
│   └── ConsumptionAnalysisService.cs
├── AI/
│   └── ConsumptionSuggestionGenerator.cs
├── Data/
│   └── MongoDbService.cs
├── Tests/
│   └── EnergyConsumptionAPI.Tests/
│       ├── EnergyConsumptionControllerTests.cs
│       └── EnergyConsumptionAnalysisControllerTests.cs
├── Program.cs
└── README.md
```

## Tecnologias Utilizadas

- **.NET 8**: Para desenvolvimento da API e integração com a inteligência artificial.
- **MongoDB**: Banco de dados NoSQL escalável para armazenar os registros de consumo de energia.
- **ML.NET**: Biblioteca de aprendizado de máquina da Microsoft para criar e treinar modelos de IA diretamente na aplicação.
- **Swagger**: Documentação e exploração dos endpoints da API.
- **xUnit e Moq**: Frameworks para testes automatizados, garantindo a qualidade e a corretude dos endpoints.
- **SonarQube**: Ferramenta de análise de qualidade de código para identificar problemas e vulnerabilidades.

## Endpoints da API

Abaixo estão descritos os principais endpoints fornecidos pela EnergyConsumptionAPI:

### 1. **EnergyConsumptionController**

- **GET /api/EnergyConsumption**: Retorna uma lista de todos os registros de consumo de energia.
- **GET /api/EnergyConsumption/{id}**: Retorna um registro específico de consumo de energia pelo seu ID.
- **POST /api/EnergyConsumption**: Cria um novo registro de consumo de energia.
- **PUT /api/EnergyConsumption/{id}**: Atualiza um registro existente de consumo de energia pelo seu ID.
- **DELETE /api/EnergyConsumption/{id}**: Deleta um registro específico de consumo de energia pelo seu ID.

### 2. **EnergyConsumptionAnalysisController**

- **GET /api/EnergyConsumptionAnalysis/{id}**: Obtém uma análise do consumo de energia específico pelo seu ID, incluindo se o consumo está alto e sugestões para otimização.

Estes endpoints permitem a gestão completa dos registros de consumo de energia e fornecem análises detalhadas para ajudar a otimizar o consumo energético de residências.

## Como Executar o Projeto


1. **Configurar o Banco de Dados:**

Certifique-se de que o MongoDB está instalado e em execução.

Atualize a string de conexão no arquivo appsettings.json com suas credenciais do MongoDB.

String de Conexão

No arquivo appsettings.json, configure a string de conexão para o MongoDB como mostrado abaixo:

```sh
"ConnectionStrings": {
"DbConnection": "mongodb+srv://<username>:<password>@<clusterurl>/EnergyConsumptionAPI"
}
```

Esta string de conexão é utilizada para conectar o aplicativo à instância do MongoDB hospedada na nuvem. Certifique-se de alterar as credenciais de acordo com suas configurações de ambiente e segurança.

*lembrando que você precisa executar alguns métodos POST para inserir no banco, para treinar a IA*

2. **Executar a Aplicação**:
   ```sh
   dotnet run
   ```
   A aplicação estará disponível em: `https://localhost:5001`

3. **Acessar a Documentação da API**:
   - Abra o navegador e acesse `https://localhost:5001/swagger` para explorar os endpoints disponíveis.



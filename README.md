<h1 align="center">
  <br>
  <a href="https://github.com/user-attachments/assets/8a2f48c8-d3eb-4a59-a487-105dd0635144"><img src="https://github.com/user-attachments/assets/8a2f48c8-d3eb-4a59-a487-105dd0635144" alt="ApiKMLManipulation" width="200"></a>
  <br>
  ApiKMLManipulation
  <br>
</h1>

<h4 align="center">Uma Web API para manipulação de arquivos KML, com suporte a exportação, filtragem e listagem de dados.</h4>

<p align="center">
  <a href="https://dotnet.microsoft.com/">
    <img src="https://img.shields.io/badge/.NET-8.0-blue.svg">
  </a>
  <a href="https://swagger.io/">
    <img src="https://img.shields.io/badge/Swagger-OpenAPI-yellow.svg">
  </a>
  <a href="https://github.com/coverlet-coverage/coverlet">
    <img src="https://img.shields.io/badge/Coverlet-Code--Coverage-brightgreen.svg">
  </a>
</p>

<p align="center">
  <a href="#key-features">Funcionalidades</a> •
  <a href="#how-to-use">Como Usar</a> •
  <a href="#project-structure">Estrutura do Projeto</a> •
  <a href="#support">Suporte</a>
</p>

## Funcionalidades

* **Exportar KML filtrado:** Criação de novos arquivos KML baseados em filtros personalizados.
* **Listagem de dados em JSON:** Converta os dados do KML em um formato JSON legível.
* **Valores únicos para filtros:** Obtenha os valores disponíveis para campos de filtro como CLIENTE, SITUAÇÃO e BAIRRO.

---

## Como Usar

### Requisitos

- [Git](https://git-scm.com)  
- [.NET SDK 8.0](https://dotnet.microsoft.com/download)  
- Um editor de código, como o [Visual Studio Code](https://code.visualstudio.com/).

### Passos

```bash
# Clone este repositório
$ git clone https://github.com/taisprestes01/ApiKmlManipulation.git

# Acesse a pasta do projeto
$ cd ApiKMLManipulation

# Restaure as dependências do projeto
$ dotnet restore

# Execute o projeto
$ dotnet run

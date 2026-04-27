# 🍔 Good Hamburger API

API REST desenvolvida em **.NET / ASP.NET Core** para gerenciamento de pedidos de uma lanchonete fictícia, com aplicação de regras de negócio e descontos conforme o cardápio.

---

## 📌 Funcionalidades

* CRUD completo de pedidos
* Cálculo automático de subtotal, desconto e total
* Validação de regras de negócio
* Endpoint para consulta do cardápio
* Tratamento global de exceções

---

## 🧾 Regras de negócio

* Cada pedido pode conter:

  * 1 sanduíche
  * 1 batata
  * 1 bebida

* Descontos aplicados:

  * 🍔 + 🍟 + 🥤 → **20%**
  * 🍔 + 🥤 → **15%**
  * 🍔 + 🍟 → **10%**
  * Apenas 🍔 → **sem desconto**

* Não é permitido adicionar itens duplicados

* O pedido deve conter ao menos um sanduíche

---

## 🏗️ Arquitetura

O projeto foi estruturado seguindo princípios de **Clean Architecture**, com separação clara de responsabilidades:

* **Domain**

  * Entidades e regras de negócio
  * Exceções de domínio

* **Application**

  * Serviços de aplicação
  * DTOs
  * Exceções de fluxo

* **Infrastructure**

  * Persistência com Entity Framework Core
  * Repositórios

* **API**

  * Controllers
  * Middleware de tratamento de exceções

---

## ⚙️ Decisões técnicas

* Utilização de **Domain Driven Design (DDD)** para centralizar regras no domínio
* Implementação de **Owned Entity** para `OrderItem`, garantindo consistência do agregado
* Uso de **Exception Middleware** para tratamento global de erros
* Separação de DTOs para evitar acoplamento com entidades de domínio
* Uso de **Entity Framework Core + SQLite** para persistência simples e leve

---

## ▶️ Como executar o projeto

### Pré-requisitos

* .NET 10 SDK
* Visual Studio

---

### 1. Clonar o repositório

```bash
https://github.com/kcsdesenvolvedor/GoodHamburger-BackEnd.git
```

---

### 2. Acessar a pasta da API

```bash
cd GoodHamburger.API
```

---

### 3. Executar o projeto

```bash
dotnet run
```

---

### 4. Acessar a API

Swagger disponível em:

```text
https://localhost:7023/swagger/index.html
```

---

## 📡 Endpoints principais

| Método | Rota         | Descrição        |
| ------ | ------------ | ---------------- |
| GET    | /menu        | Lista o cardápio |
| POST   | /orders      | Cria um pedido   |
| GET    | /orders      | Lista pedidos    |
| GET    | /orders/{id} | Busca por ID     |
| PUT    | /orders      | Atualiza pedido  |
| DELETE | /orders/{id} | Remove pedido    |

---

## ⚠️ Tratamento de erros

A API utiliza um middleware global que retorna respostas padronizadas:

```json
{
  "message": "Descrição do erro",
  "status": 400,
  "timestamp": "2026-01-01T12:00:00Z",
  "path": "/orders"
  "method": "Post"
}
```

---

## 📌 O que foi deixado de fora

* Autenticação (fora do escopo do desafio)
* Persistência em banco robusto (uso de SQLite para simplicidade)
* Frontend avançado (implementado separadamente como diferencial)

---

## 👨‍💻 Autor

Desenvolvido por [Kellton Castro Silva]

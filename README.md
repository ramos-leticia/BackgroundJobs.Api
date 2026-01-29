# BackgroundJobs.Api

Projeto de estudo desenvolvido em **ASP.NET Core (.NET 9)** com foco no uso do **Hangfire** para processamento de tarefas em background e jobs recorrentes.

O objetivo não é simular um sistema de produção completo, mas uma demonstração da utilização do Hangfire.

---

## 🎯 Objetivos do projeto

* Entender o funcionamento de **background jobs**
* Trabalhar com **execução assíncrona fora do ciclo HTTP**
* Explorar **retry automático e falhas controladas**
* Criar e visualizar **jobs recorrentes**
* Utilizar o **Hangfire Dashboard** para observabilidade

---

## 🛠️ Tecnologias utilizadas

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* Hangfire
* Scalar

---

## 🔌 Endpoints disponíveis

### ➕ Criar usuário (fluxo com sucesso)

```
POST /api/users
```

Cria um usuário e dispara um job em background para simular o envio de e-mail de boas-vindas com sucesso.

Exemplo de payload:

```json
{
  "name": "User Example",
  "email": "user@example.com"
}
```

---

### ❌ Criar usuário com falha simulada no job

```
POST /api/users/with-error
```

Cria um usuário e dispara um job em background que **lança uma exceção propositalmente**, permitindo observar:

* Retry automático
* Estado *Failed*
* Histórico de falhas
* Reprocessamento manual via dashboard

Esse endpoint existe **exclusivamente para fins de estudo**.

---

## ⚙️ Jobs em background (Hangfire)

O projeto utiliza o Hangfire para executar tarefas em background, desacoplando o processamento pesado do ciclo da requisição HTTP.

Características exploradas:

* Disparo de jobs via `IBackgroundJobClient`
* Retry automático (`AutomaticRetry`)
* Persistência de jobs em banco de dados
* Execução em threads do Thread Pool
* Visualização via Hangfire Dashboard

Dashboard disponível em:

```
/hangfire
```

---

### 🔁 Job recorrente

Foi implementado um **job recorrente de manutenção** utilizando `RecurringJob.AddOrUpdate`.

Função do job:

* Executar uma regra simples de manutenção sobre os usuários
* Rodar automaticamente conforme uma expressão Cron
* Demonstrar execução periódica sem interação do usuário

Para fins de estudo, o job roda em um intervalo curto para facilitar a visualização no dashboard.

---

## ▶️ Como executar o projeto

1. Clonar o repositório
2. Restaurar os pacotes
3. Aplicar as migrations do banco de dados

```bash
dotnet ef database update
```

4. Executar a aplicação

```bash
dotnet run
```

A aplicação estará disponível com:

* Scalar
* Hangfire Dashboard

---

### 📌 Observações

* Conceitos como idempotência, mensageria e tolerância avançada a falhas não foram abordados nessa demo

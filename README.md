# 🏠 Real Estate Chat Search — Semantic Kernel + Azure OpenAI

This project is my **first experiment** using [Microsoft Semantic Kernel](https://github.com/microsoft/semantic-kernel) with [Azure OpenAI Service](https://learn.microsoft.com/en-us/azure/ai-services/openai/) in a practical scenario.

The use case simulates a **real estate listings application**, where users can **interact with an AI agent** to search and filter property listings in the Netherlands using natural language.

---

## ✨ Project Goals

- Learn the basics of integrating **Semantic Kernel** with **Azure OpenAI**.
- Explore **AI-powered chat interfaces** for filtering structured data.
- Build a small demo using **mocked real estate listings**.
- Apply best practices around configuration and secrets using `.NET user-secrets`.

---

## 📦 Tech Stack

- [.NET 8 Console Application](https://learn.microsoft.com/en-us/dotnet/core/)
- [Semantic Kernel](https://github.com/microsoft/semantic-kernel) (C# SDK)
- [Azure OpenAI via Azure AI Studio](https://learn.microsoft.com/en-us/azure/ai-services/openai/overview)
- [Azure AI Foundry](https://aka.ms/azureaifoundry)
- `System.Text.Json` for JSON deserialization
- `Microsoft.Extensions.Configuration` for managing secrets and settings

---

## 🧠 AI Capabilities

The project uses **Semantic Kernel Plugins** to enable chat-based querying of property listings.

You can ask things like:

> Find houses for sale in Amsterdam with at least 3 bedrooms and energy label A. List apartments available for rent under €1500 in Utrecht.

The plugin parses the intent and filters data accordingly from a local mock database.

---

## 🏗️ Project Structure

/data/
└── real_estate_data.json # Mocked property data in Dutch cities

/Plugins/
└── ListingPlugin.cs # Semantic Kernel plugin for property queries

/Repositories/
└── RealEstateRepository.cs # Repository for managing listings (CRUD)

/Models/
└── RealEstateListing.cs # Core entity representing a real estate object

Program.cs # Initializes config, SK, and runs the demo

---

## 🔐 Configuration & Secrets

All sensitive data like Azure endpoints and keys are stored using **.NET user secrets**:

```bash
dotnet user-secrets set "SemanticKernel:Endpoint" "https://your-endpoint"
dotnet user-secrets set "SemanticKernel:ApiKey" "your-api-key"
dotnet user-secrets set "SemanticKernel:DeploymentName" "gpt-35-turbo"
```

These values are loaded into the app using IConfiguration.

## 🗃️ Real Estate Data

All listing data is mocked

Data includes properties like city, province, zip code, energy label, bedrooms, pricing, and more

Properties are localized to the Netherlands

## 📚 References

📘 Microsoft Semantic Kernel

🧠 Azure OpenAI Service

⚡ Azure AI Foundry

💬 Getting started with Semantic Kernel in C#

## 🚀 Next Steps

Add natural language processing for multilingual input

Integrate with real data APIs or CRM systems

Deploy as a web API or chat UI interface

Support richer queries with embeddings or vector search

## 👋 Author

Created by [Your Name] as an educational project and hands-on experiment with AI + .NET.

Feel free to fork, star, or contribute!

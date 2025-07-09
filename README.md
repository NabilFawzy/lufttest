# 🚀 LuftTest Project

A full-stack demo project showcasing modern authentication, API development, and frontend UI. The system is composed of:

- ✅ **IdentityServer** for secure authentication (OAuth2/OpenID Connect)
- ✅ **ASP.NET Core Web API** with SQLite for product management
- ✅ **Angular SPA** for frontend user interaction

---

## 📁 Project Structure

Lufttest/
│
├── IdentityServer/ # IdentityServer4 for authentication
├── MyApi/ # ASP.NET Core API with SQLite CRUD
└── client/
└── ecommerce/ # Angular frontend app


---

## 🧰 Technologies Used

- **ASP.NET Core 3.1**
- **Entity Framework Core (SQLite)**
- **IdentityServer4**
- **Angular 17**
- **JWT Authentication**
- **CORS Configuration**

---

## 🧪 Features

- 🔐 JWT-based authentication via IdentityServer
- 📦 SQLite-based persistent product store
- 🛒 Angular UI with login/logout, add/delete product
- 🔄 Full integration between API and frontend via CORS

---

## ⚙️ Getting Started

### 1. Clone the Repo

```bash
git clone https://github.com/NabilFawzy/lufttest.git
cd lufttest


2. Run IdentityServer

cd IdentityServer
dotnet run
# Runs on http://localhost:5003


3. Run API

cd ../MyApi
dotnet ef database update
dotnet run
# Runs on http://localhost:5000


4. Run Angular Frontend

cd ../client/ecommerce
npm install
ng serve
# Runs on http://localhost:4200


Example User
Username: nabil
Password: pass123





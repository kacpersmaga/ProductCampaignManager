# Product Campaign Manager

This is a CRUD application for managing product advertising campaigns. Sellers can create, edit, delete, and list campaigns for their products, with the following details:

- **Campaign Name**: Mandatory field for the campaign’s name.
- **Keywords**: Mandatory, pre-populated with typeahead suggestions.
- **Bid Amount**: Mandatory, with a minimum amount.
- **Campaign Fund**: Mandatory, deducted from the seller’s Emerald account, with the updated balance displayed.
- **Status**: Mandatory, either "On" or "Off".
- **Town**: Selected from a pre-populated dropdown list.
- **Radius**: Mandatory, in kilometers.

The project consists of a **.NET REST API** backend with SQLite for data storage and a **Next.js** frontend built with React and TypeScript, styled with Tailwind CSS.

## Prerequisites

- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) for the backend.
- [Node.js](https://nodejs.org/) (version 18 or later) for the frontend.
- [Git](https://git-scm.com/) to clone the repository.

## Project Structure

- **backend/**: .NET REST API with SQLite database.
  - `ProductCampaignManager.API/`: Main API project.
  - `ProductCampaignManager.Application/`: Business logic and command/query handlers.
  - `ProductCampaignManager.Domain/`: Entity definitions.
  - `ProductCampaignManager.Infrastructure/`: Database context and repositories.
- **frontend/**: Next.js frontend with React and TypeScript.

## Running the Project Locally

### 1. Clone the Repository
```bash
git clone https://github.com/kacpersmaga/ProductCampaignManager.git
cd ProductCampaignManager
```

### 2. Run the Backend
1. Navigate to the backend directory:
   ```bash
   cd backend/ProductCampaignManager.API
   ```
2. Restore dependencies and run the .NET API:
   ```bash
   dotnet restore
   dotnet run
   ```
   The backend will start on https://localhost:7263 (preferred) or http://localhost:5290.

### 3. Run the Frontend
1. Open a new terminal and navigate to the frontend directory:
   ```bash
   cd frontend
   ```
2. Install dependencies and start the Next.js app:
   ```bash
   npm install
   npm run dev
   ```
   The frontend will start on `http://localhost:3000`.

### 4. Access the Application
- Open your browser and navigate to `http://localhost:3000` to use the frontend.

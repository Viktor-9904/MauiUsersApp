# MauiUsersApp - .NET MAUI App

## Overview
This is a **.NET MAUI (.NET 8)** mobile application for Android that manages users.  
It allows login, viewing a list of users, and adding/editing user details.  
The app uses **MVVM architecture**, **Shell navigation**, and **Dapper** for data operations.  

### Features
- **Login Page**: Authenticate with email and password
- **Users Page**:
  - Display a list of users (Name, Username, Phone, Email, IsActive)
  - Navigate to Edit User page by selecting a user
  - Add new user via "Add User" button
- **Edit User Page**:
  - Add new or edit existing user
  - Validate required fields
  - Save data to SQLite database via Dapper
- Success/error feedback messages

---

## Requirements
- .NET 8 SDK
- Visual Studio 2022
- Android Emulator (tested on **Google Pixel 6 Pro**)
- Optional: SQLite database (included with the project)

---

## Setup Instructions

1. **Clone the repository**
```bash
git clone [https://github.com/Viktor-9904/UserManagementMauiApp.git](https://github.com/Viktor-9904/MauiUsersApp.git)
cd MauiUsersApp
```

## Notes
- The app uses **MVVM** with separate ViewModels for each page.
- **Dapper** is used for CRUD operations on SQLite.
- Navigation is implemented via **Shell**.
- Validates required fields before saving.
- Tested on **Google Pixel 6 Pro emulator** running Android 13.

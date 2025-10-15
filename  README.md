# Floors Manufacturing Management System

[![](https://img.shields.io/badge/AvaloniaUI-blue?style=flat&logo=avaloniaui&labelColor=333333&color=1e90ff)](https://avaloniaui.net/)
[![](https://img.shields.io/badge/.NET-9.0-purple?style=flat&logo=dotnet&labelColor=333333&color=8A2BE2)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![](https://img.shields.io/badge/C%23-11-green?style=flat&logo=csharp&labelColor=333333&color=008000)](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11)
[![](https://img.shields.io/badge/PostgreSQL-424242?style=flat&logo=postgresql&labelColor=333333&color=30689b)](https://www.postgresql.org/)

---

[🇷🇺 Русский](#ru) | [🇺🇸 English](#en)

<a id="ru"></a>

## 🇷🇺 Floors Manufacturing Management System: Управление производством

### **Содержание**

1.  [О проекте](#о-проекте)
2.  [Ключевые возможности](#ключевые-возможности)
3.  [Технологии](#технологии)
4.  [Установка и запуск](#установка-и-запуск)
5.  [Галерея скриншотов](#галерея-скриншотов)

### **О проекте**

**Floors Manufacturing Management System** — это кроссплатформенное desktop-приложение, разработанное для автоматизации и оптимизации бизнес-процессов в производственной компании "Мастер пол". Приложение охватывает полный цикл управления, от работы с клиентами до складского учета, и решает ключевую задачу — автоматизацию рутинных процессов производителя напольных покрытий.

### **Ключевые возможности**

- ⚙️ **CRM-система:** Управление данными партнеров, включая добавление, редактирование и удаление. Система проводит детальную валидацию бизнес-данных, таких как ИНН и email, для обеспечения их корректности.
- 📦 **Каталог продукции:** Централизованный каталог напольных покрытий с ценами, описаниями и функцией поиска.
- 💰 **Система заказов:** Создание и управление заявками. Приложение автоматически рассчитывает прогрессивные скидки (5% / 10% / 15%) в зависимости от суммы заказа.
- 📈 **Складской учет:** Отслеживание остатков продукции на складе в реальном времени, что обеспечивает прозрачность и контроль.
- 📋 **Гибкий workflow:** Заказы проходят через многоступенчатую систему статусов: `Черновик` → `В обработке` → `Выполнена`, что позволяет контролировать каждый этап выполнения.

### **Технологии**

- **Frontend:** Avalonia UI (кроссплатформенный desktop фреймворк), XAML, MVVM (CommunityToolkit.MVVM).
- **Backend:** .NET 9.0, C# 11, Entity Framework Core (ORM).
- **База данных:** PostgreSQL, Npgsql (провайдер).
- **Архитектура:** **MVVM архитектура** с четким разделением на слои (Views/ViewModels/Services/Models/Data), **Внедрение зависимостей** для управления зависимостями, **Событийно-ориентированная коммуникация** для межмодульного взаимодействия.

### **Установка и запуск**

#### **Предварительные требования**

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL 16+](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/)

#### **Пошаговая инструкция**

1.  **Клонирование репозитория:**
    ```bash
    git clone https://github.com/RamenOfficialGovPatsy/Floors-Manufacturing-System.git
    cd Floors-Manufacturing-System
    ```
2.  **Настройка базы данных:**

    - Создайте новую базу данных в PostgreSQL и настройте пользователя:

      ```sql
      -- Создание базы данных
      CREATE DATABASE master_floor_db;

      -- Создание пользователя (опционально)
      CREATE USER master_floor_user WITH PASSWORD 'secure_password_123';

      -- Назначение прав
      GRANT ALL PRIVILEGES ON DATABASE master_floor_db TO master_floor_user;

      -- Подключение к БД
      \c master_floor_db;
      ```

    - Обновите строку подключения в файле `AppDbContext.cs`.

3.  **Применение миграций:**

    ```bash
    # Создание новой миграции (если нужно)
    dotnet ef migrations add InitialCreate

    # Применение миграций к базе данных
    dotnet ef database update
    ```

4.  **Сборка и запуск:**
    ```bash
    dotnet restore
    dotnet run
    ```
    Или запустите проект через Visual Studio (F5).

### **Галерея скриншотов**

![Главное меню приложения](screenshots/1_main_window.png)
_1. Главное меню приложения_

![Окно управления партнерами](screenshots/2_partners_window.png)
_2. Окно управления партнерами_

![Форма добавления партнера](screenshots/3_add_partner_window.png)
_3. Форма добавления партнера_

![Каталог продукции](screenshots/4_productions_window.png)
_4. Каталог продукции_

![Складские остатки](screenshots/5_warehouse_window.png)
_5. Складские остатки_

![Список заявок](screenshots/6_applications_window.png)
_6. Список заявок_

![Создание заявки](screenshots/7_create_application_window.png)
_7. Создание заявки_

![Выбор продукции для заявки](screenshots/8_choose_product_window.png)
_8. Выбор продукции для заявки_

![Редактирование заявки](screenshots/9_edit_application_window.png)
_9. Редактирование заявки_

---

<br>

<a id="en"></a>

## 🇺🇸 Floors Manufacturing Management System: Production Management

### **Table of Contents**

1.  [About the Project](#about-the-project)
2.  [Key Features](#key-features)
3.  [Technologies](#technologies)
4.  [Installation and Run](#installation-and-run)
5.  [Screenshots Gallery](#screenshots-gallery)

### **About the Project**

**Floors Manufacturing Management System** is a cross-platform desktop application designed to automate and optimize business processes for the "Master Floor" manufacturing company. The application covers the full management cycle, from client relations to warehouse inventory, solving the key challenge of automating routine tasks for a flooring manufacturer.

### **Key Features**

- ⚙️ **CRM System:** Manage partner data, including adding, editing, and deleting records. The system performs detailed validation of business data, such as TIN (Taxpayer Identification Number) and email, to ensure accuracy.
- 📦 **Product Catalog:** A centralized catalog of flooring products with prices, descriptions, and a search function.
- 💰 **Order System:** Create and manage orders. The application automatically calculates progressive discounts (5% / 10% / 15%) based on the order total.
- 📈 **Warehouse Inventory:** Real-time tracking of product stock, providing transparency and control.
- 📋 **Flexible Workflow:** Orders move through a multi-stage status system: `Draft` → `In Processing` → `Completed`, allowing for control over each stage of fulfillment.

### **Technologies**

- **Frontend:** Avalonia UI (cross-platform desktop framework), XAML, MVVM (CommunityToolkit.MVVM).
- **Backend:** .NET 9.0, C# 11, Entity Framework Core (ORM).
- **Database:** PostgreSQL, Npgsql (provider).
- **Architecture:** **MVVM architecture** with clear layer separation (Views/ViewModels/Services/Models/Data), **Dependency Injection** for managing dependencies, **Event-driven communication** for inter-module interaction.

### **Installation and Run**

#### **Prerequisites**

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL 16+](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/)

#### **Step-by-step Guide**

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/RamenOfficialGovPatsy/Floors-Manufacturing-System.git
    cd Floors-Manufacturing-System
    ```
2.  **Database Setup:**

    - Create a new PostgreSQL database and set up a user:

      ```sql
      -- Create a new database
      CREATE DATABASE master_floor_db;

      -- Create a user (optional)
      CREATE USER master_floor_user WITH PASSWORD 'secure_password_123';

      -- Grant privileges
      GRANT ALL PRIVILEGES ON DATABASE master_floor_db TO master_floor_user;

      -- Connect to the database
      \c master_floor_db;
      ```

    - Update the connection string in the `AppDbContext.cs` file.

3.  **Apply Migrations:**

    ```bash
    # Create a new migration (if needed)
    dotnet ef migrations add InitialCreate

    # Apply migrations to the database
    dotnet ef database update
    ```

4.  **Build and Run:**
    ```bash
    dotnet restore
    dotnet run
    ```
    Alternatively, run the project from Visual Studio (F5).

### **Screenshots Gallery**

![Main Application Window](screenshots/1_main_window.png)
_1. Main Application Window_

![Partner Management Window](screenshots/2_partners_window.png)
_2. Partner Management Window_

![Add Partner Form](screenshots/3_add_partner_window.png)
_3. Add Partner Form_

![Product Catalog](screenshots/4_productions_window.png)
_4. Product Catalog_

![Warehouse Stock](screenshots/5_warehouse_window.png)
_5. Warehouse Stock_

![List of Applications](screenshots/6_applications_window.png)
_6. List of Applications_

![Create Application](screenshots/7_create_application_window.png)
_7. Create Application_

![Choose Product for Application](screenshots/8_choose_product_window.png)
_8. Choose Product for Application_

![Edit Application](screenshots/9_edit_application_window.png)
_9. Edit Application_

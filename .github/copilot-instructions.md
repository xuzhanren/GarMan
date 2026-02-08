# GarMan - Garbage/Appliance Management System

## Agent Instructions

This document provides guidelines for AI assistants working with the GarMan codebase.

---

## Project Overview

**GarMan** is an ASP.NET Core MVC web application for managing appliance recycling and collection services. The application enables users to submit appliances for recycling, track collection status, manage recycling centers, and handle user roles and permissions.

**Primary Domain**: Waste management, appliance recycling, and environmental services.

---

## Technology Stack

- **Framework**: ASP.NET Core 10.0 (net10.0)
- **Language**: C# with nullable reference types enabled and implicit usings
- **Database**: SQL Server (connection to `W11P2212\\SQL22DEV`)
- **ORM**: Entity Framework Core 10.0.2
- **Authentication**: ASP.NET Core Identity with roles
- **UI**: Razor Views (MVC pattern) with Bootstrap
- **Client Libraries**: jQuery, jQuery Validation

---

## Architecture & Patterns

### MVC Pattern

- **Models**: Domain entities in `Models/` folder
- **Views**: Razor views in `Views/` folder organized by controller
- **Controllers**: Action handlers in `Controllers/` folder

### Key Design Principles

1. **Repository Pattern**: Access data through `ApplicationDbContext`
2. **View Models**: Separate ViewModels in `ViewModels/` for complex UI scenarios
3. **Data Annotations**: Use extensively for validation and display metadata
4. **Enums**: Define status and condition types within model files

---

## Project Structure

```
GarMan/
├── Controllers/          # MVC Controllers
│   ├── AppliancesController.cs
│   ├── RecyclingCentersController.cs
│   ├── RolesController.cs
│   ├── UsersController.cs
│   └── HomeController.cs
├── Models/              # Domain entities
│   ├── Appliance.cs
│   ├── RecyclingCenter.cs
│   └── ErrorViewModel.cs
├── ViewModels/          # UI-specific view models
│   ├── RoleViewModel.cs
│   └── UserViewModel.cs
├── Views/               # Razor views organized by controller
├── Data/                # Database context
│   └── ApplicationDbContext.cs
├── Migrations/          # EF Core migrations
├── Areas/Identity/      # ASP.NET Core Identity scaffolded pages
├── wwwroot/            # Static files (CSS, JS, libraries)
└── Properties/         # Launch settings
```

---

## Database Schema

### Tables

1. **Appliances** - Core entity for appliance tracking
2. **RecyclingCenters** - Recycling facility information
3. **AspNetUsers** - Identity users
4. **AspNetRoles** - Identity roles
5. **Other Identity tables** - Standard ASP.NET Core Identity schema

### Key Properties & Relationships

#### Appliance Entity

**Properties**:

- `Id` (PK)
- `ApplianceType` (Required, max 100 chars)
- `Brand`, `ModelNumber`, `YearOfManufacture`
- `Condition` (enum: Working, PartiallyWorking, NotWorking, ForParts)
- `Description` (max 500 chars)
- `Weight` (decimal, precision 18,2)
- `CollectionDate`, `CollectionAddress`, `CollectionFee` (default: 50)
- `Status` (enum: Pending, Scheduled, InTransit, Collected, Cancelled)
- `RecyclingStatus` (enum: NotProcessed, InProcessing, Recycled, Disposed, Resold)
- `EstimatedValue`, `RecycledValue` (decimal, precision 18,2)
- `RecyclingComment` (max 500 chars)
- `DateSubmitted` (default: DateTime.Now), `LastUpdated`
- `UserId`, `CustomerName`, `CustomerPhone`, `CustomerEmail`

**Decimal Precision**: All decimal fields (Weight, EstimatedValue, RecycledValue) use precision (18,2)

#### RecyclingCenter Entity

**Properties**:

- `Id` (PK)
- `CenterName` (Required, max 100 chars)
- `Address` (Required, max 200 chars)
- `City`, `State`, `ZipCode`
- `PhoneNumber` (Phone format), `Email` (Email format)
- `OperatingHours`, `AcceptedAppliances`
- `IsActive` (default: true)

**Seed Data**: Two initial recycling centers are seeded (Green Tech Recycling, Eco Appliance Solutions)

---

## Entity Framework Core Migrations

Keep migrations organized and descriptive. Each migration should have a clear name indicating the change (e.g., `AddEstimatedValueToAppliance`).

Migrations should keep current data intact and only add new columns or tables. Avoid modifying existing columns in a way that could cause data loss without proper handling.

### Migration History

1. `20260206013253_InitialCreate` - Initial schema
2. `20260206020439_AddEstimatedAndRecycledValueToAppliance` - Added value tracking
3. `20260206022335_AddRecyclingCommentToAppliance` - Added comments
4. `20260206023642_AddCollectionFeeToAppliance` - Added collection fee

### Creating Migrations

```powershell
dotnet ef migrations add <MigrationName> --project GarMan
dotnet ef database update --project GarMan
```

---

## Conventions & Guidelines

### C# Code Style

- **Nullable Reference Types**: Enabled - use `?` for nullable properties
- **String Initialization**: Use `= string.Empty` for required strings
- **Primary Constructors**: Used in `ApplicationDbContext`
- **Implicit Usings**: Enabled - common namespaces auto-imported

### Data Annotations

Always include appropriate attributes:

- `[Required]` for mandatory fields
- `[StringLength(n)]` for string size limits
- `[Display(Name = "...")]` for user-friendly labels
- `[DataType]` for specialized types (Currency, Date, Phone, Email)
- `[EmailAddress]`, `[Phone]` for validation

### Enums

- Define within the same file as the entity
- Use `[Display(Name = "...")]` for multi-word enum values
- Example: `[Display(Name = "Partially Working")]` for `PartiallyWorking`

### ViewModels

- Create separate ViewModels in `ViewModels/` folder for complex UIs
- Use when combining multiple entities or when view requirements differ from domain models
- Current ViewModels: `RoleViewModel`, `UserViewModel`

---

## Development Workflow

### Adding New Features

1. **Create/Modify Model**
   - Add entity to `Models/` folder
   - Apply data annotations
   - Define enums inline if specific to the entity

2. **Update DbContext**
   - Add `DbSet<TEntity>` to `ApplicationDbContext`
   - Configure in `OnModelCreating()` if needed (precision, relationships, seed data)

3. **Create Migration**

   ```powershell
   dotnet ef migrations add <DescriptiveName>
   dotnet ef database update
   ```

4. **Generate/Create Controller & Views**
   - Use scaffolding or create manually
   - Place controller in `Controllers/` folder
   - Place views in `Views/<ControllerName>/` folder

5. **Update Navigation** (if needed)
   - Update `_Layout.cshtml` navigation menu

### Building & Running

```powershell
# Build
dotnet build

# Run
dotnet run --project GarMan

# Stop any running instance before rebuilding
Stop-Process -Name "GarMan" -Force -ErrorAction SilentlyContinue
```

---

## Important Rules for AI Agents

### DO

✅ **Always check existing migrations** before creating new ones  
✅ **Use data annotations** for validation and metadata  
✅ **Follow existing naming conventions** (PascalCase for properties, etc.)  
✅ **Preserve decimal precision** configurations (18,2) when modifying models  
✅ **Update both model AND views** when adding/modifying properties  
✅ **Respect nullable reference types** - use `?` appropriately  
✅ **Apply [Display] attributes** for user-friendly field names  
✅ **Check for existing ViewModels** before creating new ones  
✅ **Test migrations** in development before suggesting for production  
✅ **Maintain consistency** with existing enum patterns and naming

### DON'T

❌ **Don't modify migrations** after they've been applied  
❌ **Don't remove [Required] or [StringLength]** without confirming data won't break  
❌ **Don't change decimal precision** without creating a migration  
❌ **Don't create duplicate enums** - check if one already exists  
❌ **Don't scaffold over existing views** without backing up custom changes  
❌ **Don't hardcode connection strings** - use configuration  
❌ **Don't ignore seed data** when modifying entities with HasData  
❌ **Don't bypass validation** - maintain data integrity rules  
❌ **Don't create models without updating ApplicationDbContext**  
❌ **Don't forget to update corresponding views** when modifying models

### When Modifying Entities

1. Check if a migration already exists for the change
2. Update the model class with proper annotations
3. Update OnModelCreating if precision/relationships are affected
4. Create a new migration with a descriptive name
5. Verify the generated migration looks correct
6. Update related views and controllers
7. Test the changes

### When Working with Identity

- The project uses ASP.NET Core Identity with roles
- Identity pages are scaffolded in `Areas/Identity/Pages/`
- User management is handled through `UsersController` and `RolesController`
- Always preserve Identity configuration in `Program.cs`

---

## Configuration

### Connection String

Located in `appsettings.json`:

```json
"DefaultConnection": "Server=W11P2212\\SQL22DEV;Database=GarbageManagementDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

### User Secrets

- UserSecretsId: `aspnet-GarMan-cf742c62-1b74-4c8c-bf1b-f099e37fc5a1`
- For sensitive configuration, use: `dotnet user-secrets set "key" "value"`

---

## Common Tasks

### Adding a Property to Appliance

1. Add property to `Appliance.cs` with annotations
2. Update `ApplicationDbContext.OnModelCreating()` if it's decimal
3. Create migration: `dotnet ef migrations add Add<PropertyName>ToAppliance`
4. Update database: `dotnet ef database update`
5. Update views: `Create.cshtml`, `Edit.cshtml`, `Details.cshtml`, `Index.cshtml`

### Creating a New Entity

1. Create model class in `Models/` folder
2. Add `DbSet<TEntity>` to `ApplicationDbContext`
3. Configure in `OnModelCreating()` if needed
4. Create initial migration
5. Scaffold controller and views OR create manually
6. Add navigation link to `_Layout.cshtml`

### Managing Roles & Users

- Use `RolesController` for role CRUD operations
- Use `UsersController` for user management
- ViewModels exist for these operations

---

## Testing & Debugging

- Launch profiles defined in `Properties/launchSettings.json`
- Development environment uses `appsettings.Development.json`
- Error handling goes through `HomeController.Error()` action
- Use `_ValidationScriptsPartial.cshtml` for client-side validation

---

## Security Considerations

- SignIn requires confirmed account: `options.SignIn.RequireConfirmedAccount = true`
- HTTPS redirection is enabled
- HSTS is configured for production
- Role-based authorization is available through Identity

---

## Dependencies & Packages

Key NuGet packages:

- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (10.0.2)
- `Microsoft.AspNetCore.Identity.UI` (10.0.2)
- `Microsoft.EntityFrameworkCore.SqlServer` (10.0.2)
- `Microsoft.EntityFrameworkCore.Tools` (10.0.2)
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` (10.0.2)

---

## Additional Notes

- **Target Framework**: .NET 10.0 (ensure compatibility)
- **Database**: SQL Server 2022 Developer Edition (based on server name)
- **Static Assets**: Optimized with `MapStaticAssets()` and `WithStaticAssets()`
- **Bootstrap**: Used for styling (located in `wwwroot/lib/bootstrap/`)
- **jQuery**: Used for client-side interactions and validation

---

## Questions to Ask Before Making Changes

1. Does this change require a database migration?
2. Are there existing patterns or conventions I should follow?
3. Will this affect existing data or break current functionality?
4. Should I create a ViewModel or modify the entity directly?
5. Do I need to update navigation or add new links?
6. Are there related views that need to be updated?
7. Does this require new validation rules or data annotations?
8. Should this be role-protected or publicly accessible?

---

**Last Updated**: February 2026  
**Project Version**: .NET 10.0  
**Database**: GarbageManagementDb on SQL Server 2022

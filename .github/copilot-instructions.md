# GitHub Copilot Instructions for MyContoso

## Project Overview

MyContoso is an architectural demonstration application inspired by real enterprise systems. It showcases how .NET MAUI client applications evolve under delivery pressure and how architectural decisions impact correctness, ownership, and maintainability over time.

**Key Purpose**: This is NOT a production application. It's a contrived internal company app designed to demonstrate:
- Shared state management
- Isolated state handling
- Navigation patterns
- Domain ownership
- Cross-cutting concerns
- Common architectural pitfalls and solutions

**Important**: The API does not need full feature implementation—simple stubs are sufficient to support the mobile app. However, the MAUI app should demonstrate proper architectural practices without shortcuts, as the goal is to showcase best practices, not bad ones.

## Technology Stack

- **.NET 10**: Latest .NET version with C# 13
- **.NET MAUI**: Cross-platform mobile/desktop framework (Android, iOS, macOS, Windows)
- **ASP.NET Core**: Minimal API for backend services
- **.NET Aspire**: Local development orchestration and service discovery
- **CommunityToolkit.Maui**: MAUI community extensions
- **CommunityToolkit.Mvvm**: MVVM helpers and source generators

## Repository Structure

```
MyContoso/
├── .github/                    # GitHub configuration and CI/CD
├── src/
│   ├── API/
│   │   └── MyContoso.ApiService/     # ASP.NET Core Minimal API (stub endpoints)
│   ├── Aspire/
│   │   ├── MyContoso.AppHost/        # Aspire orchestration host
│   │   ├── MyContoso.ServiceDefaults/    # Aspire service defaults for backend
│   │   └── MyContoso.MauiServiceDefaults/  # Aspire service defaults for MAUI
│   ├── Common/
│   │   └── Shared/                   # Shared DTOs and contracts
│   └── UI/
│       ├── MyContoso.App/            # .NET MAUI mobile/desktop app
│       └── MyContoso.Web/            # Blazor web frontend (optional)
├── tests/
│   └── MyContoso.Tests/              # Test project
├── README.md                          # Project documentation
└── MyContoso.slnx                     # Solution file (XML format)
```

## Development Guidelines

### Always Search Microsoft Documentation First

**CRITICAL**: Always search Microsoft documentation (MS Learn) when working with .NET, Windows, Microsoft features, or APIs. Use available documentation search tools to find the most current information about capabilities, best practices, and implementation patterns before making changes.

Example areas requiring documentation search:
- .NET MAUI controls, handlers, and platform-specific code
- ASP.NET Core minimal APIs and middleware
- .NET Aspire service configuration
- Cross-platform development patterns
- Performance optimization techniques

### Domain Model

The application demonstrates these core domain concepts:

1. **Employee**: Represents company employees
   - EmployeeId, Name, Role/Title, Department, ProfileSummary, AccreditationStatus

2. **CompanyUpdate**: Company-wide announcements
   - UpdateId, Title, Body, PublishedDate, Author

3. **Policy**: Internal company policies/handbook entries
   - PolicyId, Title, Category, LastUpdated, Content

4. **Accreditation**: Professional accreditations/compliance requirements
   - AccreditationId, Name, Description, Status, ExpiryDate

### App Features

The app includes these demonstration sections:

1. **Company Feed**: Shared, long-lived state (singleton service pattern)
2. **Employee Directory**: Isolated state, transient navigation (proper scoping demonstration)
3. **Employee Profile**: Individual employee details (canonical example for avoiding state leakage)
4. **Policies & Handbook**: Static/semi-static content (showing when NOT to over-architect)
5. **Accreditations**: Derived state and cross-cutting visibility

### Navigation Model

Conceptual routes:
- `/feed` - Company updates feed
- `/employees` - Employee directory listing
- `/employees/{id}` - Individual employee profile
- `/policies` - Policy list
- `/policies/{id}` - Individual policy details

The app may intentionally start with less robust patterns (magic strings, weak typing) to demonstrate evolution toward better practices (centralized routes, strong typing).

## .NET MAUI Development Standards

### Critical Rules (NEVER Violate)

- **NEVER use ListView** - Obsolete, will be deleted. Use `CollectionView`
- **NEVER use TableView** - Obsolete. Use Grid/VerticalStackLayout
- **NEVER use AndExpand layout options** - Obsolete
- **NEVER use BackgroundColor** - Always use `Background` property
- **NEVER place ScrollView/CollectionView inside StackLayout** - Breaks scrolling/virtualization
- **NEVER reference images as SVG** - Always use PNG (SVG only for asset generation)
- **NEVER mix Shell with NavigationPage/TabbedPage/FlyoutPage**
- **NEVER use renderers** - Use handlers instead

### Recommended Controls

**Important**: This project may use FlagstoneUI (a custom UI framework for .NET MAUI). When FlagstoneUI controls are available, prefer them over standard MAUI controls for consistency and theming. See `src/UI/MyContoso.App/AGENTS.MD` for FlagstoneUI details.

**FlagstoneUI Controls** (when available/in use):
- `FsButton` - Fully styleable button (prefer over `Button`)
- `FsCard` - Container with elevation and borders (prefer over `Border` or `Frame` for card-style containers)
- `FsEntry` - Single-line text input with full visual control (prefer over `Entry`)
- `FsEditor` - Multi-line text input (prefer over `Editor`)

**Standard MAUI Controls** (use when FlagstoneUI equivalent not available):

**Layouts**:
- `Grid` - Complex layouts (preferred over StackLayout)
- `Border` - Container with border (preferred over Frame)
- `VerticalStackLayout` / `HorizontalStackLayout` - Simple stacking
- `ScrollView` - Scrollable content (single child only)

**Lists & Collections**:
- `CollectionView` - Lists with >20 items (virtualized)
- `BindableLayout` - Small lists ≤20 items (no virtualization)
- `CarouselView` + `IndicatorView` - Galleries and sliders

**Other Input Controls**:
- `CheckBox` / `Switch` - Boolean selection
- `Picker` - Dropdown selection
- `SearchBar` - Search input
- `ImageButton` - Image-based actions

### Performance Best Practices

1. **Always use compiled bindings** for 8-20x performance improvement:
```xml
<ContentPage x:DataType="vm:MainViewModel">
    <Label Text="{Binding Name}" />
</ContentPage>
```

2. **Use expression-based bindings in C#**:
```csharp
// DO: Type-safe, compiled
label.SetBinding(Label.TextProperty, static (PersonViewModel vm) => vm.FullName?.FirstName);

// DON'T: Runtime errors, no IntelliSense
label.SetBinding(Label.TextProperty, "FullName.FirstName");
```

3. **Choose appropriate binding modes**:
   - `OneTime` - Data won't change
   - `OneWay` - Default, read-only
   - `TwoWay` - Only when needed (editable fields)

4. **Prefer modern controls**: Grid > StackLayout, CollectionView > ListView, Border > Frame

### Platform-Specific Code

Use conditional compilation for platform-specific implementations:

```csharp
#if ANDROID
    // Android-specific code
#elif IOS
    // iOS-specific code
#elif WINDOWS
    // Windows-specific code
#elif MACCATALYST
    // Mac Catalyst-specific code
#endif
```

**Important**: Prefer `BindableObject.Dispatcher` or inject `IDispatcher` via DI for UI thread updates. Use `MainThread.BeginInvokeOnMainThread()` as a fallback.

### Handler Customization

Customize platform controls using handlers in `MauiProgram.cs`:

```csharp
Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("Custom", (handler, view) =>
{
#if ANDROID
    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.HotPink);
#elif IOS
    handler.PlatformView.BackgroundColor = UIKit.UIColor.SystemPink;
#endif
});
```

### Shell Navigation

Use Shell for navigation (recommended pattern):

```csharp
// Register routes
Routing.RegisterRoute("details", typeof(DetailPage));

// Navigate
await Shell.Current.GoToAsync("details?id=123");
```

**Best practices**:
- Set `MainPage` once at startup
- Don't change `MainPage` frequently
- Don't nest tabs
- Don't mix Shell with other navigation patterns

## API Development Guidelines

### Stub Implementation Philosophy

The API exists to support the mobile app's demonstration purposes only. Full implementation is NOT required.

**Key Principles**:
1. **Stubs are sufficient**: Return minimal JSON responses that allow the mobile app to function
2. **No authentication required**: This is a demo, not production
3. **In-memory data is fine**: No need for databases
4. **Typical architectural concerns can be relaxed**: Focus on supporting mobile app scenarios
5. **Security vulnerabilities in stubs are acceptable**: This code won't be deployed

### Example Endpoints

```csharp
// Minimal stub approach
app.MapGet("/company-updates", () => new[]
{
    new { UpdateId = 1, Title = "Welcome", Body = "...", PublishedDate = DateTime.Now },
    new { UpdateId = 2, Title = "Q4 Results", Body = "...", PublishedDate = DateTime.Now }
});

app.MapGet("/employees", () => new[]
{
    new { EmployeeId = 1, Name = "John Doe", Role = "Developer" },
    new { EmployeeId = 2, Name = "Jane Smith", Role = "Designer" }
});

app.MapGet("/employees/{id}", (int id) => 
    new { EmployeeId = id, Name = "John Doe", Role = "Developer", Department = "Engineering" });
```

## .NET Aspire

This project uses .NET Aspire for local development orchestration. Key features:

- **Service Discovery**: Automatic service endpoint resolution
- **Health Checks**: Monitoring service health
- **Dev Tunnels**: Public HTTPS endpoints for iOS/Android testing
- **OpenTelemetry**: Distributed tracing and metrics

**Configuration**: See `src/Aspire/MyContoso.AppHost/AppHost.cs` for orchestration setup.

**Not for Production**: Aspire is used for development convenience only. This application will not be deployed or published.

## Building and Testing

### Prerequisites

- .NET 10 SDK
- MAUI workload: `dotnet workload install maui`
- Visual Studio 2022 17.12+ or VS Code with C# Dev Kit

### Build Commands

```bash
# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run Aspire AppHost (starts all services)
cd src/Aspire/MyContoso.AppHost
dotnet run

# Run API directly
cd src/API/MyContoso.ApiService
dotnet run

# Build MAUI app for specific platform
cd src/UI/MyContoso.App
dotnet build -f net10.0-android
dotnet build -f net10.0-ios
dotnet build -f net10.0-windows10.0.19041.0
```

### Testing

```bash
# Run all tests
dotnet test

# Run specific test project
cd tests/MyContoso.Tests
dotnet test
```

## Code Conventions

### General C# Standards

- Use nullable reference types (`<Nullable>enable</Nullable>`)
- Use implicit usings (`<ImplicitUsings>enable</ImplicitUsings>`)
- Follow standard .NET naming conventions
- Use meaningful variable and method names
- Keep methods focused and small

### MAUI-Specific Conventions

- Use MVVM pattern with CommunityToolkit.Mvvm
- Prefer XAML for UI definition
- Use compiled bindings (`x:DataType`)
- Organize code: Views, ViewModels, Services, Models
- Platform-specific code goes in `Platforms/` folder

### File Organization

```
MyContoso.App/
├── Views/              # XAML pages
├── ViewModels/         # View models
├── Services/           # Business logic services
├── Models/             # Data models
├── Platforms/          # Platform-specific code
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   └── Windows/
└── Resources/          # Images, fonts, styles
```

## Security Considerations

**For MAUI App**:
- Use `SecureStorage` for sensitive data (tokens, credentials)
- Validate all user inputs
- Use HTTPS for all API communication
- Never commit secrets to source control
- Demonstrate proper security patterns (this IS a teaching tool)

**For API**:
- Authentication/authorization NOT required (demo only)
- CORS can be wide-open (demo only)
- Validation is optional for stub endpoints
- Security is NOT a concern for this stub API

## Common Pitfalls to Avoid

### MAUI Development

1. ❌ Changing `MainPage` frequently
2. ❌ Using gesture recognizers on parent and child (use `InputTransparent = true`)
3. ❌ Memory leaks from unsubscribed events
4. ❌ Deeply nested layouts (flatten hierarchy)
5. ❌ Testing only on emulators (test on real devices when possible)
6. ❌ Using obsolete Xamarin.Forms patterns

### State Management

1. ❌ Singleton pages for transient content (demonstrates state leakage)
2. ❌ Mixing scoped and singleton service lifetimes incorrectly
3. ❌ Shared state without proper synchronization
4. ❌ Not properly disposing of resources

## Resources and References

### Official Documentation

- [.NET MAUI Documentation](https://learn.microsoft.com/dotnet/maui)
- [.NET MAUI Controls](https://learn.microsoft.com/dotnet/maui/user-interface/controls/)
- [.NET MAUI Shell](https://learn.microsoft.com/dotnet/maui/fundamentals/shell/)
- [.NET MAUI Handlers](https://learn.microsoft.com/dotnet/maui/user-interface/handlers/)
- [.NET Aspire Documentation](https://learn.microsoft.com/dotnet/aspire)
- [ASP.NET Core Minimal APIs](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis)

### Community Resources

- [.NET MAUI Community Toolkit](https://learn.microsoft.com/dotnet/communitytoolkit/maui/)
- [.NET MAUI GitHub Repository](https://github.com/dotnet/maui)
- [Enterprise Application Patterns using .NET MAUI](https://learn.microsoft.com/dotnet/architecture/maui/)

## Project-Specific Notes

### AGENTS.MD File

The `src/UI/MyContoso.App/AGENTS.MD` file contains specialized guidance for AI coding assistants working with:
- **FlagstoneUI**: A custom UI framework/design system for .NET MAUI
- **.NET MAUI**: Core MAUI development patterns

**Note**: While FlagstoneUI guidance exists in AGENTS.MD, this project may or may not currently use FlagstoneUI. The AGENTS.MD file serves as reference documentation for potential future use or as an example of UI framework integration.

### Aspire Integration

The project includes custom MAUI-specific Aspire service defaults (`MyContoso.MauiServiceDefaults`) that enable:
- Service discovery from MAUI apps
- OpenTelemetry support for mobile platforms
- Dev tunnel support for iOS/Android development

## Updating These Instructions

**IMPORTANT**: As this project evolves, please update these copilot instructions to reflect:

- New features or architectural patterns
- Additional domain concepts or endpoints
- Changes to build or deployment processes
- New dependencies or frameworks
- Lessons learned from architectural decisions
- Updated best practices or conventions

Keep these instructions synchronized with the actual codebase to ensure GitHub Copilot provides the most relevant and accurate assistance.

---

*Last Updated: January 2026*
*Version: 1.0*

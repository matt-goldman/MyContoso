# FlagstoneUI Agent Guide

> **For AI coding assistants and developers**: This guide helps you generate quality FlagstoneUI code by explaining the architecture, patterns, and best practices.

## Overview

**FlagstoneUI** is a cross-platform UI kit and framework for .NET MAUI that provides themeable, platform-neutral controls through a token-based design system. It solves the problem of inconsistent visual styling across platforms by offering controls with full visual control from shared code—no platform handlers needed for basic styling.

### Philosophy

- **Token-First Design**: All visual properties reference semantic design tokens (e.g., `Color.Primary`, `Space.16`) rather than hardcoded values
- **Platform Neutrality**: Controls are designed to look and behave consistently across iOS, Android, Windows, and macOS
- **Theme Flexibility**: Swap entire visual systems by changing one resource dictionary
- **Developer Experience**: Simple XAML API that feels natural to .NET MAUI developers

### When to Use FlagstoneUI

✅ **DO use FlagstoneUI when:**
- Building cross-platform apps that need consistent, custom visual styling
- Implementing a design system or brand identity across .NET MAUI
- You need controls with full visual customization without platform-specific code
- You want token-based theming similar to CSS custom properties

❌ **DON'T use FlagstoneUI when:**
- You specifically want platform-native look and feel
- Your app has minimal UI customization needs
- You're targeting a single platform only

## Current Status

**Proof of Concept (POC)** - ~60% complete, evolving toward MVP.

**Available Controls:**
- `FsButton` - Fully styleable button with corner radius, borders, colors
- `FsCard` - Container with elevation (shadows), borders, corner radius
- `FsEntry` - Single-line text input with full visual control
- `FsEditor` - Multi-line text input with full visual control

**Available Themes:**
- Material Design 3 (`FlagstoneUI.Themes.Material`)
- Example themes in sample app

**Optional Integrations:**
- MAUI Community Toolkit integration (`FlagstoneUI.Integrations.MCT`)

## Architecture

### Package Structure

```
FlagstoneUI.Core
├── Controls/          # FsButton, FsCard, FsEntry, FsEditor
├── Styles/            # Tokens.xaml (design tokens)
└── Builders/          # FlagstoneUIBuilder (minimal, may be removed)

FlagstoneUI.Themes.Material
└── Theme.xaml         # Material Design 3 theme implementation

FlagstoneUI.Integrations.MCT (optional)
├── Behaviors/         # ValidationBehaviorAdapter for FsEntry
└── Animations/        # FsEditorBorderAnimation for animated borders
```

### Token System

FlagstoneUI uses a comprehensive token system with design tokens organized into semantic categories:

**Token Categories:**
- **Colors**: `Color.Primary`, `Color.OnPrimary`, `Color.Surface`, `Color.Error`, etc.
- **Spacing**: `Space.0`, `Space.8`, `Space.16`, `Space.24`, etc.
- **Border Radius**: `Radius.Small`, `Radius.Medium`, `Radius.Large` (Double), `Radius.Button.Small`, `Radius.Button.Medium` (Int32 for Button.CornerRadius)
- **Typography**: `FontSize.BodyLarge`, `FontSize.TitleMedium`, `FontSize.HeadlineLarge`, etc.
- **Border Width**: `BorderWidth.Thin`, `BorderWidth.Thick`, etc.
- **Elevation**: `Elevation.Level0`, `Elevation.Level1`, `Elevation.Level2`, etc.
- **Padding**: `Padding.Small`, `Padding.Medium`, `Padding.Large`, etc.
- **Opacity**: `Opacity.Disabled`, `Opacity.Medium`, `Opacity.High`, etc.

Tokens follow Material Design 3 naming conventions for broad familiarity.

### Theme Configuration

**Important:** FlagstoneUI uses ResourceDictionary merging in `App.xaml` for theme configuration. There is **no builder pattern** (YAGNI principle).

**Correct theme setup in App.xaml:**

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:material="clr-namespace:FlagstoneUI.Themes.Material;assembly=FlagstoneUI.Themes.Material"
             x:Class="YourApp.App">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!-- Your existing styles -->
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
                
                <!-- Add FlagstoneUI theme -->
                <material:Theme />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

**Key Points:**
- Theme is added via `<material:Theme />` in MergedDictionaries
- Requires XAML namespace declaration: `xmlns:material="clr-namespace:FlagstoneUI.Themes.Material;assembly=FlagstoneUI.Themes.Material"`
- Requires project reference to the theme package
- Themes automatically merge core tokens

**Alternative: Define Theme Directly in Your App:**

You can also define a theme ResourceDictionary directly in your app alongside existing resource dictionaries:

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
            <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
            <ResourceDictionary Source="Resources/Styles/YourTheme.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

This is a common pattern for custom themes that are specific to your application.

## Code Generation Patterns

### ✅ DO: Use Tokens for All Visual Properties

**Always reference tokens using DynamicResource:**

```xml
<!-- ✅ BEST: Use theme styles (no explicit styling needed) -->
<fs:FsButton Text="Submit" />

<!-- ✅ ALSO CORRECT: Using tokens when customizing beyond theme defaults -->
<fs:FsButton 
    Text="Submit"
    BackgroundColor="{DynamicResource Color.Primary}"
    TextColor="{DynamicResource Color.OnPrimary}"
    CornerRadius="{DynamicResource Radius.Button.Medium}"
    Padding="{DynamicResource Space.16}" />

<fs:FsCard 
    BackgroundColor="{DynamicResource Color.Surface}"
    CornerRadius="{DynamicResource Radius.Medium}"
    Padding="{DynamicResource Space.16}">
    <Label Text="Card Content" />
</fs:FsCard>
```

### ❌ DON'T: Hardcode Visual Values

```xml
<!-- ❌ WRONG: Hardcoded values -->
<fs:FsButton 
    Text="Submit"
    BackgroundColor="#6750A4"
    TextColor="#FFFFFF"
    CornerRadius="12"
    Padding="16" />
```

**Why?** Hardcoded values break theming, prevent dark mode support, and create inconsistency.

### ✅ DO: Use Semantic Color Tokens

Choose tokens based on semantic meaning, not appearance:

```xml
<!-- ✅ CORRECT: Semantic color usage -->
<fs:FsButton Text="Delete" 
             BackgroundColor="{DynamicResource Color.Error}"
             TextColor="{DynamicResource Color.OnError}" />

<Label Text="Success!" 
       TextColor="{DynamicResource Color.Primary}" />

<fs:FsCard BackgroundColor="{DynamicResource Color.Surface}">
    <Label Text="Card content" 
           TextColor="{DynamicResource Color.OnSurface}" />
</fs:FsCard>
```

**Color Role Guidelines:**
- **Primary**: Main brand actions, key UI elements
- **Secondary**: Supporting actions, less prominent elements
- **Tertiary**: Accent color for visual hierarchy
- **Error**: Error states, destructive actions
- **Surface**: Card backgrounds, elevated surfaces
- **Background**: Main page/screen background
- **On[Color]**: Text/icons displayed on the named color (e.g., `OnPrimary` on `Primary`)

### ✅ DO: Use Theme Style Variants

Themes provide named style variants. Use them instead of custom inline styling:

```xml
<!-- ✅ CORRECT: Using theme style variants -->
<fs:FsButton Text="Primary Action" />  <!-- Default filled button -->
<fs:FsButton Text="Secondary Action" Style="{StaticResource OutlinedButton}" />
<fs:FsButton Text="Tertiary Action" Style="{StaticResource TextButton}" />

<fs:FsCard>  <!-- Default filled card -->
    <Label Text="Default card" />
</fs:FsCard>
<fs:FsCard Style="{StaticResource OutlinedCard}">
    <Label Text="Outlined card" />
</fs:FsCard>
<fs:FsCard Style="{StaticResource ElevatedCard}">
    <Label Text="Elevated card with shadow" />
</fs:FsCard>

<fs:FsEntry Placeholder="Email" />  <!-- Default filled entry -->
<fs:FsEntry Placeholder="Name" Style="{StaticResource OutlinedEntry}" />
```

**Material Theme Style Variants:**
- **Buttons**: Default (filled), `OutlinedButton`, `TextButton`, `TonalButton`, `SecondaryButton`, `TertiaryButton`
- **Cards**: Default (filled), `OutlinedCard`, `ElevatedCard`
- **Entry/Editor**: Default (filled), `OutlinedEntry`, `OutlinedEditor`

### ✅ DO: Use Namespace Prefix for Controls

Always use the `fs:` prefix for FlagstoneUI controls:

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:fs="clr-namespace:FlagstoneUI.Core.Controls;assembly=FlagstoneUI.Core"
             x:Class="YourApp.MainPage">
    
    <fs:FsButton Text="Click Me" />
    <fs:FsCard>
        <fs:FsEntry Placeholder="Enter text" />
    </fs:FsCard>
</ContentPage>
```

### ❌ DON'T: Mix Standard MAUI Controls with FlagstoneUI Controls

For consistency, prefer FlagstoneUI controls throughout your UI:

```xml
<!-- ❌ AVOID: Mixing standard and Fs controls -->
<VerticalStackLayout>
    <Button Text="Standard Button" />
    <fs:FsButton Text="Fs Button" />
    <Entry Placeholder="Standard Entry" />
    <fs:FsEntry Placeholder="Fs Entry" />
</VerticalStackLayout>

<!-- ✅ BETTER: Consistent control usage -->
<VerticalStackLayout>
    <fs:FsButton Text="Primary Action" />
    <fs:FsButton Text="Secondary Action" Style="{StaticResource OutlinedButton}" />
    <fs:FsEntry Placeholder="Email" />
    <fs:FsEntry Placeholder="Password" IsPassword="True" />
</VerticalStackLayout>
```

### ✅ DO: Use Per-Edge Borders When Needed

FlagstoneUI controls support per-edge borders for effects like underlines or 3D styling:

```xml
<!-- Bottom border only (underline effect) -->
<fs:FsEntry 
    Placeholder="Email"
    BorderBottomThickness="2"
    BorderBottomBrush="{DynamicResource Color.Primary}" />

<!-- 3D raised effect with different edge colors -->
<fs:FsCard 
    Padding="16"
    CornerRadius="8"
    BorderTopThickness="2"
    BorderLeftThickness="2"
    BorderRightThickness="2"
    BorderBottomThickness="2"
    BorderTopBrush="White"
    BorderLeftBrush="White"
    BorderRightBrush="Gray"
    BorderBottomBrush="Gray">
    <Label Text="3D effect card" />
</fs:FsCard>

<!-- Border shorthand syntax (uniform or TRBL) -->
<fs:FsCard Border="2 Blue" Padding="16">
    <Label Text="Uniform border using shorthand" />
</fs:FsCard>

<fs:FsCard Border="2 White, 2 Gray, 2 Gray, 2 White" Padding="16">
    <Label Text="TRBL borders using shorthand" />
</fs:FsCard>
```

**Border Properties:**
- **Uniform**: `BorderColor`/`BorderBrush` + `BorderWidth` (sets all four edges)
- **Per-Edge**: `BorderTopThickness`, `BorderRightThickness`, `BorderBottomThickness`, `BorderLeftThickness` + corresponding `Brush` properties
- **Shorthand**: `Border` property accepts "thickness color" syntax with 1, 2, or 4 values
- **Note**: FsCard uses `BorderColor`, FsEntry/FsEditor use `BorderBrush`

### ✅ DO: Apply Visual States Properly

Use Visual State Manager for interactive states:

```xml
<fs:FsButton Text="Interactive Button">
    <VisualStateManager.VisualStateGroups>
        <VisualStateGroup x:Name="CommonStates">
            <VisualState x:Name="Normal" />
            <VisualState x:Name="Disabled">
                <VisualState.Setters>
                    <Setter Property="Opacity" Value="{DynamicResource Opacity.Disabled}" />
                </VisualState.Setters>
            </VisualState>
        </VisualStateGroup>
    </VisualStateManager.VisualStateGroups>
</fs:FsButton>
```

## Form Validation Patterns

### Using MAUI Community Toolkit Validation

FlagstoneUI provides optional integration with MCT validators via the `FlagstoneUI.Integrations.MCT` package:

```xml
<ContentPage xmlns:fs="clr-namespace:FlagstoneUI.Core.Controls;assembly=FlagstoneUI.Core"
             xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit">
    
    <ContentPage.Resources>
        <Style x:Key="ValidStyle" TargetType="fs:FsEntry">
            <Setter Property="BorderBrush" Value="{DynamicResource Color.Primary}" />
            <Setter Property="BorderWidth" Value="2" />
        </Style>
        
        <Style x:Key="InvalidStyle" TargetType="fs:FsEntry">
            <Setter Property="BorderBrush" Value="{DynamicResource Color.Error}" />
            <Setter Property="BorderWidth" Value="2" />
        </Style>
    </ContentPage.Resources>
    
    <!-- Email validation -->
    <fs:FsEntry Placeholder="Enter your email" Keyboard="Email">
        <fs:FsEntry.Behaviors>
            <toolkit:EmailValidationBehavior
                InvalidStyle="{StaticResource InvalidStyle}"
                ValidStyle="{StaticResource ValidStyle}"
                Flags="ValidateOnValueChanged" />
        </fs:FsEntry.Behaviors>
    </fs:FsEntry>
    
    <!-- Password validation (minimum length) -->
    <fs:FsEntry Placeholder="Password (min 8 chars)" IsPassword="True">
        <fs:FsEntry.Behaviors>
            <toolkit:CharactersValidationBehavior
                CharacterType="Any"
                MinimumCharacterCount="8"
                InvalidStyle="{StaticResource InvalidStyle}"
                ValidStyle="{StaticResource ValidStyle}"
                Flags="ValidateOnValueChanged" />
        </fs:FsEntry.Behaviors>
    </fs:FsEntry>
</ContentPage>
```

**Note:** MCT integration requires the `FlagstoneUI.Integrations.MCT` NuGet package. It's not a core dependency of FlagstoneUI.

**Available MCT Validators:**
- `EmailValidationBehavior` - Email validation
- `NumericValidationBehavior` - Numeric range validation
- `RequiredStringValidationBehavior` - Non-empty validation
- `TextValidationBehavior` - Regex-based validation
- `UriValidationBehavior` - URI/URL validation
- `CharactersValidationBehavior` - Character count validation
- `MultiValidationBehavior` - Combine multiple validators

## Common Patterns

### Sign-In Form

```xml
<ScrollView>
    <VerticalStackLayout Padding="24" Spacing="16">
        
        <Label Text="Welcome Back"
               FontSize="{DynamicResource FontSize.HeadlineLarge}"
               TextColor="{DynamicResource Color.OnBackground}"
               HorizontalOptions="Center" />
        
        <fs:FsCard Style="{StaticResource ElevatedCard}">
            <VerticalStackLayout Spacing="16">
                
                <fs:FsEntry Placeholder="Email"
                            Keyboard="Email" />
                
                <fs:FsEntry Placeholder="Password"
                            IsPassword="True" />
                
                <fs:FsButton Text="Sign In"
                             Clicked="OnSignInClicked" />
                
                <fs:FsButton Text="Create Account"
                             Style="{StaticResource OutlinedButton}"
                             Clicked="OnCreateAccountClicked" />
                
            </VerticalStackLayout>
        </fs:FsCard>
        
    </VerticalStackLayout>
</ScrollView>
```

### Card List

```xml
<CollectionView ItemsSource="{Binding Items}">
    <CollectionView.ItemTemplate>
        <DataTemplate>
            <fs:FsCard Margin="16,8">
                <Grid ColumnDefinitions="*,Auto" ColumnSpacing="12">
                    <VerticalStackLayout Grid.Column="0" Spacing="4">
                        <Label Text="{Binding Title}"
                               FontSize="{DynamicResource FontSize.TitleMedium}"
                               FontAttributes="Bold" />
                        <Label Text="{Binding Description}"
                               FontSize="{DynamicResource FontSize.BodyMedium}"
                               TextColor="{DynamicResource Color.OnSurfaceVariant}" />
                    </VerticalStackLayout>
                    
                    <fs:FsButton Grid.Column="1"
                                 Text="View"
                                 Style="{StaticResource OutlinedButton}"
                                 Command="{Binding ViewCommand}" />
                </Grid>
            </fs:FsCard>
        </DataTemplate>
    </CollectionView.ItemTemplate>
</CollectionView>
```

### Settings Screen

```xml
<ScrollView>
    <VerticalStackLayout Padding="16" Spacing="12">
        
        <Label Text="Settings"
               FontSize="{DynamicResource FontSize.HeadlineMedium}"
               Margin="0,0,0,8" />
        
        <!-- Account section -->
        <Label Text="Account"
               FontSize="{DynamicResource FontSize.TitleMedium}"
               TextColor="{DynamicResource Color.Primary}" />
        
        <fs:FsCard>
            <VerticalStackLayout Spacing="12">
                <fs:FsEntry Placeholder="Display Name"
                            Text="{Binding DisplayName}" />
                <fs:FsEntry Placeholder="Email"
                            Text="{Binding Email}"
                            Keyboard="Email" />
            </VerticalStackLayout>
        </fs:FsCard>
        
        <!-- Preferences section -->
        <Label Text="Preferences"
               FontSize="{DynamicResource FontSize.TitleMedium}"
               TextColor="{DynamicResource Color.Primary}"
               Margin="0,16,0,0" />
        
        <fs:FsCard>
            <VerticalStackLayout Spacing="12">
                <Grid ColumnDefinitions="*,Auto">
                    <Label Text="Notifications" VerticalOptions="Center" />
                    <Switch Grid.Column="1" IsToggled="{Binding NotificationsEnabled}" />
                </Grid>
                <Grid ColumnDefinitions="*,Auto">
                    <Label Text="Dark Mode" VerticalOptions="Center" />
                    <Switch Grid.Column="1" IsToggled="{Binding DarkModeEnabled}" />
                </Grid>
            </VerticalStackLayout>
        </fs:FsCard>
        
        <fs:FsButton Text="Save Changes"
                     Margin="0,16,0,0"
                     Command="{Binding SaveCommand}" />
        
    </VerticalStackLayout>
</ScrollView>
```

## Anti-Patterns (DO NOT)

### ❌ DON'T: Hardcode Colors or Spacing

```xml
<!-- ❌ WRONG -->
<fs:FsButton BackgroundColor="#6750A4" Padding="24,10" />

<!-- ✅ CORRECT -->
<fs:FsButton 
    BackgroundColor="{DynamicResource Color.Primary}"
    Padding="{DynamicResource Space.24}" />
```

### ❌ DON'T: Override Token Values in Component Styles

```xml
<!-- ❌ WRONG: Hardcoded values in application styles break theming -->
<Style TargetType="fs:FsButton">
    <Setter Property="BackgroundColor" Value="#FF0000" />
</Style>

<!-- ✅ CORRECT: Use theme style variants or semantic tokens -->
<Style x:Key="DangerButton" TargetType="fs:FsButton">
    <Setter Property="BackgroundColor" Value="{DynamicResource Color.Error}" />
    <Setter Property="TextColor" Value="{DynamicResource Color.OnError}" />
</Style>
```

**Note**: Base themes (like Material theme) set token values and define base styles. This anti-pattern applies to application-level styles that consumers create.

### ❌ DON'T: Use Magic Numbers for Sizing

```xml
<!-- ❌ WRONG -->
<fs:FsCard Padding="14" CornerRadius="7" />

<!-- ✅ CORRECT -->
<fs:FsCard 
    Padding="{DynamicResource Space.16}"
    CornerRadius="{DynamicResource Radius.Small}" />
```

### ❌ DON'T: Use Platform-Specific Code for Styling

```xml
<!-- ❌ WRONG: Platform-specific styling breaks FlagstoneUI's purpose -->
<fs:FsEntry>
    <OnPlatform x:TypeArguments="Color" x:Key="EntryBackground">
        <On Platform="iOS" Value="#F0F0F0" />
        <On Platform="Android" Value="#E0E0E0" />
    </OnPlatform>
</fs:FsEntry>

<!-- ✅ CORRECT: Use tokens for consistent cross-platform styling -->
<fs:FsEntry Background="{DynamicResource Color.SurfaceVariant}" />
```

### ❌ DON'T: Mix BorderColor/BorderWidth with Per-Edge Properties

```xml
<!-- ❌ WRONG: Conflicts between uniform and per-edge properties -->
<fs:FsCard 
    BorderColor="Blue"
    BorderWidth="2"
    BorderBottomThickness="4"
    BorderBottomBrush="Red">
    <!-- Behavior is undefined -->
</fs:FsCard>

<!-- ✅ CORRECT: Choose one approach -->
<!-- Option 1: Uniform border -->
<fs:FsCard BorderColor="Blue" BorderWidth="2">
    <Label Text="Uniform border" />
</fs:FsCard>

<!-- Option 2: Per-edge borders -->
<fs:FsCard 
    BorderTopThickness="2"
    BorderBottomThickness="4"
    BorderTopBrush="Blue"
    BorderBottomBrush="Red">
    <Label Text="Different edges" />
</fs:FsCard>
```

### ❌ DON'T: Use Hardcoded Values in Custom Containers

```xml
<!-- ❌ WRONG: Hardcoded styling can't be themed -->
<ContentView BackgroundColor="#FFFFFF" Padding="16">
    <Label Text="Content" TextColor="#000000" />
</ContentView>

<!-- ✅ CORRECT: Use tokens for any custom containers -->
<ContentView 
    BackgroundColor="{DynamicResource Color.Surface}"
    Padding="{DynamicResource Space.16}">
    <Label Text="Content" 
           TextColor="{DynamicResource Color.OnSurface}" />
</ContentView>

<!-- ✅ ALSO CORRECT: Use FsCard for card-style containers -->
<fs:FsCard>
    <Label Text="Content" 
           TextColor="{DynamicResource Color.OnSurface}" />
</fs:FsCard>
```

**Note**: You can create custom controls and containers using standard MAUI controls. The key is to use design tokens instead of hardcoded values so your custom controls respect the theme.

## Advanced Patterns

### Runtime Theme Switching

While the primary use case is static theming, FlagstoneUI's token architecture enables runtime theme switching:

```csharp
// In App.xaml.cs
public static void SwitchTheme(string themeName)
{
    // Clear existing themes
    Current!.Resources.MergedDictionaries.Clear();
    
    // Add base styles
    Current.Resources.MergedDictionaries.Add(
        new YourApp.Resources.Styles.Colors());
    Current.Resources.MergedDictionaries.Add(
        new YourApp.Resources.Styles.Styles());
    
    // Add selected theme
    switch (themeName)
    {
        case "Material":
            Current.Resources.MergedDictionaries.Add(
                new FlagstoneUI.Themes.Material.Theme());
            break;
        case "CustomTheme":
            Current.Resources.MergedDictionaries.Add(
                new YourApp.Themes.CustomTheme());
            break;
    }
}
```

### Animated Borders with MCT Integration

The optional MCT integration provides animated gradient borders:

```xml
xmlns:fs="clr-namespace:FlagstoneUI.Core.Controls;assembly=FlagstoneUI.Core"
xmlns:anim="clr-namespace:FlagstoneUI.Integrations.MCT.Animations;assembly=FlagstoneUI.Integrations.MCT"

<ContentPage.Resources>
    <anim:FsEditorBorderAnimation x:Key="AiBorderAnimation" Length="3000">
        <anim:FsEditorBorderAnimation.Gradient>
            <LinearGradientBrush StartPoint="0,0" EndPoint="1,1">
                <GradientStop Color="Gold" Offset="0.0" />
                <GradientStop Color="Orange" Offset="0.25" />
                <GradientStop Color="Goldenrod" Offset="0.5" />
                <GradientStop Color="DarkGoldenrod" Offset="0.75" />
                <GradientStop Color="Gold" Offset="1.0" />
            </LinearGradientBrush>
        </anim:FsEditorBorderAnimation.Gradient>
    </anim:FsEditorBorderAnimation>
</ContentPage.Resources>

<fs:FsEditor 
    x:Name="AiEditor"
    Placeholder="🤖 Ask me anything..."
    Focused="OnAiEditorFocused"
    Unfocused="OnAiEditorUnfocused" />
```

```csharp
private CancellationTokenSource? _animationCts;

private async void OnAiEditorFocused(object? sender, EventArgs e)
{
    _animationCts = new CancellationTokenSource();
    var animation = (FsEditorBorderAnimation)Resources["AiBorderAnimation"];
    
    try
    {
        await animation.Animate(AiEditor, _animationCts.Token);
    }
    catch (OperationCanceledException) { }
}

private void OnAiEditorUnfocused(object? sender, EventArgs e)
{
    _animationCts?.Cancel();
}
```

## Available Tools

### CLI Tools

- **Bootstrap Theme Converter**: Available in the codebase but not yet published as a .NET CLI tool. Will be available as `flagstone-bootstrap convert --help` once published.
- **Tailwind Palette Converter**: Tool for converting Tailwind color palettes to FlagstoneUI tokens (planned)

### Integration Tools

- **Crosswind**: Steven Thewissen's Tailwind-to-MAUI utility classes project (external, compatible with FlagstoneUI)
- **Figma Converter**: Design token export from Figma to FlagstoneUI themes (future consideration per ADR009)

## Project Requirements

### Prerequisites

- **.NET 10 SDK** (minimum version specified in `global.json`)
- **MAUI Workload**: `dotnet workload install maui`

### Installation

**Via NuGet (Preview):**

```bash
dotnet add package FlagstoneUI.Core --version 0.0.1-preview1
dotnet add package FlagstoneUI.Themes.Material --version 0.0.1-preview1

# Optional MCT integration
dotnet add package FlagstoneUI.Integrations.MCT --version 0.0.1-preview1
```

**Via Project Reference (Development):**

```xml
<ItemGroup>
  <ProjectReference Include="..\flagstone-ui\src\FlagstoneUI.Core\FlagstoneUI.Core.csproj" />
  <ProjectReference Include="..\flagstone-ui\src\FlagstoneUI.Themes.Material\FlagstoneUI.Themes.Material.csproj" />
</ItemGroup>
```

## Control Reference

### FsButton

Button control with full visual customization.

**Key Properties:**
- `Text`, `Command`, `CommandParameter` - Standard button properties
- `BackgroundColor`, `TextColor` - Colors (use tokens)
- `CornerRadius` - Corner radius (Int32, use `Radius.Button.*` tokens)
- `Padding` - Internal spacing
- `BorderColor`, `BorderWidth` - Uniform border
- `FontSize`, `FontFamily`, `FontAttributes` - Typography

**Example:**

```xml
<fs:FsButton Text="Primary Action" />
<fs:FsButton Text="Secondary" Style="{StaticResource OutlinedButton}" />
<fs:FsButton Text="Tertiary" Style="{StaticResource TextButton}" />
```

### FsCard

Container control with elevation, borders, and corner radius.

**Key Properties:**
- `Content` - ContentView content property
- `BackgroundColor` - Background color (use tokens)
- `CornerRadius` - Corner radius (Double, use `Radius.*` tokens)
- `Padding` - Internal spacing
- `Elevation` - Shadow depth (0-4, follows Material Design elevation levels)
- `BorderColor`, `BorderWidth` - Uniform border
- Per-edge borders: `BorderTopThickness`, `BorderRightThickness`, `BorderBottomThickness`, `BorderLeftThickness` with corresponding `Brush` properties
- `Border` - Shorthand for border (e.g., `"2 Blue"` or `"2 White, 2 Gray, 2 Gray, 2 White"`)

**Example:**

```xml
<fs:FsCard>
    <Label Text="Default filled card" />
</fs:FsCard>

<fs:FsCard Style="{StaticResource ElevatedCard}">
    <Label Text="Card with shadow" />
</fs:FsCard>
```

### FsEntry

Single-line text input with full visual control.

**Key Properties:**
- `Text`, `Placeholder` - Text content
- `TextColor`, `PlaceholderColor` - Colors (use tokens)
- `FontSize`, `FontFamily` - Typography
- `Keyboard` - Keyboard type (Email, Numeric, etc.)
- `IsPassword` - Password masking
- `Background` - Background brush (use tokens, can be gradient)
- `CornerRadius` - Corner radius (Double, use `Radius.*` tokens)
- `Padding` - Internal spacing
- `BorderBrush`, `BorderWidth` - Uniform border
- Per-edge borders: `BorderTopThickness`, `BorderRightThickness`, `BorderBottomThickness`, `BorderLeftThickness` with corresponding `Brush` properties
- `Border` - Shorthand for border
- `HeightRequest` - Control height
- Visual states: `Focused`, `Normal`

**Example:**

```xml
<fs:FsEntry Placeholder="Email" Keyboard="Email" />
<fs:FsEntry Placeholder="Password" IsPassword="True" />
<fs:FsEntry Placeholder="Outlined" Style="{StaticResource OutlinedEntry}" />
```

### FsEditor

Multi-line text input with full visual control.

**Key Properties:**
- `Text`, `Placeholder` - Text content
- `TextColor`, `PlaceholderColor` - Colors (use tokens)
- `FontSize`, `FontFamily` - Typography
- `Background` - Background brush (use tokens, can be gradient)
- `CornerRadius` - Corner radius (Double, use `Radius.*` tokens)
- `Padding` - Internal spacing
- `BorderBrush`, `BorderWidth` - Uniform border
- Per-edge borders: `BorderTopThickness`, `BorderRightThickness`, `BorderBottomThickness`, `BorderLeftThickness` with corresponding `Brush` properties
- `Border` - Shorthand for border
- `MinimumHeightRequest` - Minimum control height
- `AutoSize` - Auto-expand behavior
- Visual states: `Focused`, `Normal`

**Example:**

```xml
<fs:FsEditor Placeholder="Enter description..." 
             MinimumHeightRequest="120" />

<fs:FsEditor Placeholder="Outlined editor"
             Style="{StaticResource OutlinedEditor}" />
```

## Understanding the Sample App

The sample app (`samples/FlagstoneUI.SampleApp/`) demonstrates FlagstoneUI capabilities but is **atypical**:

- It showcases **multiple themes** and theme switching
- It demonstrates **per-edge borders** and advanced styling
- It shows **MCT integration** examples

**Typical consumer apps:**
- Use **one theme** (Material or custom)
- Apply theme in `App.xaml` and forget about it
- Use standard control patterns without advanced styling
- May not use MCT integration at all

**Key Takeaway:** The sample app shows what's *possible*, not what's *typical*. Most apps will have simpler, more straightforward usage.

## Best Practices Summary

1. **Always use tokens** for colors, spacing, typography, and other visual properties
2. **Reference tokens with DynamicResource** to support theming and dark mode
3. **Use semantic color tokens** based on meaning, not appearance
4. **Prefer theme style variants** over custom inline styling
5. **Use the `fs:` prefix** for all FlagstoneUI controls
6. **Be consistent** - don't mix standard MAUI and FlagstoneUI controls
7. **Choose uniform OR per-edge borders** - don't mix both approaches
8. **Validate against tokens** - if you're hardcoding values, you're doing it wrong

## Troubleshooting

### Theme Not Applied

- Verify `<material:Theme />` is in `App.xaml` MergedDictionaries
- Check XAML namespace declaration is correct
- Ensure project reference to theme package exists
- Clean and rebuild solution

### Controls Not Found

- Verify `xmlns:fs="clr-namespace:FlagstoneUI.Core.Controls;assembly=FlagstoneUI.Core"` is declared
- Check project reference to FlagstoneUI.Core exists
- Ensure control name is correct (e.g., `FsButton` not `FsButton`)

### Validation Not Working

- Install `FlagstoneUI.Integrations.MCT` package
- Add MCT namespace: `xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"`
- Set `Flags` property (e.g., `ValidateOnValueChanged`)
- Ensure styles target `fs:FsEntry` not `Entry`

### Build Errors

- Ensure .NET 10 SDK is installed (check `global.json`)
- Install MAUI workload: `dotnet workload install maui`
- Restore packages: `dotnet restore`
- Clean and rebuild: `dotnet clean && dotnet build`

## Documentation and Examples

- **Documentation**: `/docs/` directory in repository
- **Sample App**: `samples/FlagstoneUI.SampleApp/`
- **Theme Playground**: `samples/FlagstoneUI.ThemePlayground/`
- **Control Documentation**: `/docs/Controls/` directory
- **Quickstart Guide**: `/docs/quickstart.md`
- **Theming Guide**: `/docs/theming-guide.md`
- **Architecture**: `/docs/architecture.md`

## Contributing

FlagstoneUI is under active development. When contributing:

- Follow the token-first design philosophy
- Use existing tokens before creating new ones
- Keep controls platform-neutral
- Add documentation for new patterns
- Update this AGENTS.MD file when patterns change

## Future Considerations

Per ADR009, FlagstoneUI may add:

- **Figma Converter**: Import design tokens from Figma
- **Additional Themes**: Community-contributed themes
- **More Controls**: Navigation, pickers, lists, dialogs
- **FlagstoneUI.Blocks**: Pre-built screens (sign-in, CRUD, settings)
- **Project Templates**: .NET templates with FlagstoneUI pre-configured

---

**Last Updated**: January 2026  
**Version**: 0.0.1-preview (POC phase)  
**Status**: Experimental, evolving toward MVP

---
name: MAUI Expert
description: Support development of .NET MAUI cross-platform apps with controls, XAML, handlers, and performance best practices.
---

# .NET MAUI Coding Expert Agent

You are an expert .NET MAUI developer specializing in high-quality, performant, and maintainable cross-platform applications with particular expertise in .NET MAUI controls.

## Critical Rules (NEVER Violate)

- **NEVER use ListView** - obsolete, will be deleted. Use CollectionView
- **NEVER use TableView** - obsolete. Use Grid/VerticalStackLayout layouts
- **NEVER use AndExpand** layout options - obsolete
- **NEVER use BackgroundColor** - always use `Background` property
- **NEVER place ScrollView/CollectionView inside StackLayout** - breaks scrolling/virtualization
- **NEVER reference images as SVG** - always use PNG (SVG only for generation)
- **NEVER mix Shell with NavigationPage/TabbedPage/FlyoutPage**
- **NEVER use renderers** - use handlers instead

## Control Reference

### Status Indicators
| Control | Purpose | Key Properties |
|---------|---------|----------------|
| ActivityIndicator | Indeterminate busy state | `IsRunning`, `Color` |
| ProgressBar | Known progress (0.0-1.0) | `Progress`, `ProgressColor` |

### Layout Controls
| Control | Purpose | Notes |
|---------|---------|-------|
| **Border** | Container with border | **Prefer over Frame** |
| ContentView | Reusable custom controls | Encapsulates UI components |
| ScrollView | Scrollable content | Single child; **never in StackLayout** |
| Frame | Legacy container | Only for shadows |

### Shapes
BoxView, Ellipse, Line, Path, Polygon, Polyline, Rectangle, RoundRectangle - all support `Fill`, `Stroke`, `StrokeThickness`.

### Input Controls
| Control | Purpose |
|---------|---------|
| Button/ImageButton | Clickable actions |
| CheckBox/Switch | Boolean selection |
| RadioButton | Mutually exclusive options |
| Entry | Single-line text |
| Editor | Multi-line text (`AutoSize="TextChanges"`) |
| Picker | Drop-down selection |
| DatePicker/TimePicker | Date/time selection |
| Slider/Stepper | Numeric value selection |
| SearchBar | Search input with icon |

### List & Data Display
| Control | When to Use |
|---------|-------------|
| **CollectionView** | Lists >20 items (virtualized); **never in StackLayout** |
| BindableLayout | Small lists ≤20 items (no virtualization) |
| CarouselView + IndicatorView | Galleries, onboarding, image sliders |

### Interactive Controls
- **RefreshView**: Pull-to-refresh wrapper
- **SwipeView**: Swipe gestures for contextual actions

### Display Controls
- **Image**: Use PNG references (even for SVG sources)
- **Label**: Text with formatting, spans, hyperlinks
- **WebView**: Web content/HTML
- **GraphicsView**: Custom drawing via ICanvas
- **Map**: Interactive maps with pins

## Best Practices

### Layouts
```xml
<!-- DO: Use Grid for complex layouts -->
<Grid RowDefinitions="Auto,*" ColumnDefinitions="*,*">

<!-- DO: Use Border instead of Frame -->
<Border Stroke="Black" StrokeThickness="1" StrokeShape="RoundRectangle 10">

<!-- DO: Use specific stack layouts -->
<VerticalStackLayout> <!-- Not <StackLayout Orientation="Vertical"> -->
```

### Compiled Bindings (Critical for Performance)
```xml
<!-- Always use x:DataType for 8-20x performance improvement -->
<ContentPage x:DataType="vm:MainViewModel">
    <Label Text="{Binding Name}" />
</ContentPage>
```

```csharp
// DO: Expression-based bindings (type-safe, compiled)
label.SetBinding(Label.TextProperty, static (PersonViewModel vm) => vm.FullName?.FirstName);

// DON'T: String-based bindings (runtime errors, no IntelliSense)
label.SetBinding(Label.TextProperty, "FullName.FirstName");
```

### Binding Modes
- `OneTime` - data won't change
- `OneWay` - default, read-only
- `TwoWay` - only when needed (editable)
- Don't bind static values - set directly

### Handler Customization
```csharp
// In MauiProgram.cs ConfigureMauiHandlers
Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("Custom", (handler, view) =>
{
#if ANDROID
    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.HotPink);
#elif IOS
    handler.PlatformView.BackgroundColor = UIKit.UIColor.SystemPink;
#endif
});
```

### Shell Navigation (Recommended)
```csharp
Routing.RegisterRoute("details", typeof(DetailPage));
await Shell.Current.GoToAsync("details?id=123");
```
- Set `MainPage` once at startup
- Don't nest tabs

### Platform Code
```csharp
#if ANDROID
#elif IOS
#elif WINDOWS
#elif MACCATALYST
#endif
```
- Prefer `BindableObject.Dispatcher` or inject `IDispatcher` via DI for UI updates from background threads; use `MainThread.BeginInvokeOnMainThread()` as a fallback

### Performance
1. Use compiled bindings (`x:DataType`)
2. Use Grid > StackLayout, CollectionView > ListView, Border > Frame

### Security
```csharp
await SecureStorage.SetAsync("oauth_token", token);
string token = await SecureStorage.GetAsync("oauth_token");
```
- Never commit secrets
- Validate inputs
- Use HTTPS

### Resources
- `Resources/Images/` - images (PNG, JPG, SVG→PNG)
- `Resources/Fonts/` - custom fonts
- `Resources/Raw/` - raw assets
- Reference images as PNG: `<Image Source="logo.png" />` (not .svg)
- Use appropriate sizes to avoid memory bloat

## Common Pitfalls
1. Mixing Shell with NavigationPage/TabbedPage/FlyoutPage
2. Changing MainPage frequently
3. Nesting tabs
4. Gesture recognizers on parent and child (use `InputTransparent = true`)
5. Using renderers instead of handlers
6. Memory leaks from unsubscribed events
7. Deeply nested layouts (flatten hierarchy)
8. Testing only on emulators - test on actual devices
9. Some Xamarin.Forms APIs not yet in MAUI - check GitHub issues

## Reference Documentation
- [Controls](https://learn.microsoft.com/dotnet/maui/user-interface/controls/)
- [XAML](https://learn.microsoft.com/dotnet/maui/xaml/)
- [Data Binding](https://learn.microsoft.com/dotnet/maui/fundamentals/data-binding/)
- [Shell Navigation](https://learn.microsoft.com/dotnet/maui/fundamentals/shell/)
- [Handlers](https://learn.microsoft.com/dotnet/maui/user-interface/handlers/)
- [Performance](https://learn.microsoft.com/dotnet/maui/deployment/performance)

## Your Role

1. **Recommend best practices** - proper control selection
2. **Warn about obsolete patterns** - ListView, TableView, AndExpand, BackgroundColor
3. **Prevent layout mistakes** - no ScrollView/CollectionView in StackLayout
4. **Suggest performance optimizations** - compiled bindings, proper controls
5. **Provide working XAML examples** with modern patterns
6. **Consider cross-platform implications**

# Architectural Guidelines

## Purpose

This document codifies the architectural rules and guardrails for the **MyContoso** mobile app.

It exists to:

* Reduce cognitive load during development
* Eliminate entire classes of predictable runtime bugs
* Make architectural intent explicit and enforceable
* Enable parallel work without ambiguity

These rules are **deliberately opinionated**. They are not intended to be universally correct; they are intended to be *locally safe*.

The rules apply only to the mobile app, except where patterns intersect with the full stack (for example shared DTOs).

---

## Core Principles

### 1. Single Authoritative Source of Truth

Every piece of state must have **one clear owner**.

If you cannot immediately answer *"where does this truth live?"*, the design is incomplete.

---

### 2. Illegal States Should Be Unrepresentable (Where Cheap)

If a mistake can be prevented structurally at low cost, it should be.

We prefer:

* Structural guardrails
* Compile-time guarantees
* Lifetime-based correctness

Over:

* Discipline
* Conventions
* Runtime checks

---

### 3. Concrete Before Abstract

Architectural abstractions must be justified by concrete failure modes.

We do not introduce patterns preemptively.
We introduce them *in response to demonstrated problems*.

---

## Layer Responsibilities

### Model (Domain)

The **Model** represents the domain and owns truth.

Includes:

* Domain entities
* Domain services
* State-owning services

Rules:

* All **publishers** live here
* State may be mutable or immutable
* No dependency on UI concepts

---

### ViewModel

The **ViewModel** adapts domain state for presentation.

Includes:

* Commands
* Presentation state
* Subscriptions to domain publishers

Rules:

* All **subscribers** live here
* Must not publish domain events
* Must not own authoritative domain state

---

### View (UI)

The **View** owns visual behaviour.

Includes:

* Layout
* Styling
* Visual State Manager
* Animations

Rules:

* No domain logic
* No subscriptions
* Responds to ViewModel state only

---

## Dependency Flow

Allowed:

* View → ViewModel
* ViewModel → Model

Disallowed:

* Model → ViewModel
* ViewModel → View
* Horizontal dependencies

Diagonal dependencies (ViewModel → Domain Service) are allowed **by design**, but must be explicit.

---

## Messaging and Events

### Rule: No Message Buses

The app must **not** use:

* MessagingCenter
* EventAggregator-style global buses
* CommunityToolkit Messenger

Reason:

* Subscribing to messages removes ownership
* Dependencies become invisible
* Correctness becomes a runtime concern

---

### Rule: Subscribe to Publishers, Not Channels

Allowed communication mechanisms:

* .NET events
* Reactive Extensions (IObservable)

Constraints:

* Publishers live in the Model
* Subscribers live in ViewModels
* Subscriptions must be explicit and disposable

---

## Navigation

### Navigation Is a System

Navigation is not incidental glue code.

Rules:

* Routes must be defined centrally
* Magic strings are discouraged
* Navigation parameters must be explicit

The navigation system must make it difficult to:

* Navigate to unregistered routes
* Navigate with invalid parameters

---

## Page Lifetime Rules

Page lifetime is a **correctness decision**, not a performance tweak.

### Singleton Pages

Use when:

* State is shared
* Content is long-lived
* Reinitialisation is expensive

Example:

* Company feed

---

### Transient Pages

Use when:

* State is parameterised
* Content is short-lived
* State leakage would be harmful

Example:

* Employee profile

Transient lifetimes are preferred when they reduce the blast radius of mistakes.

---

## State Management

### No Global State God Objects

State must be:

* Owned
* Scoped
* Discoverable

Cross-cutting visibility does **not** justify global ownership.

---

### State Propagation

State changes must be propagated via:

* Explicit events, or
* Explicit observables

Never via broadcast or implicit discovery.

---

## Modules

### Modular Structure

Apps exceeding five pages must be modularised.

Each module is organised by **domain concept**, not technical role.

Standard module layout:

* Pages/
* ViewModels/
* Models/
* Services/
* Routes.cs
* DependencyInjection.cs

---

### Module Boundaries

Rules:

* Modules must not depend on each other
* Internal implementation must remain internal
* Explicit sharing requires a PublicAPI surface

---

## WET vs DRY

### Early Development

Being WET is acceptable when:

* UI and backend evolve independently
* Models are provisional

Early UI models are considered **disposable**.

---

### Stabilisation Phase

Once domain contracts stabilise:

* Shared DTOs are introduced
* Duplication is removed deliberately
* Ownership is clarified

DRY is about **behaviour**, not classes.

---

## Non-Goals

This architecture explicitly does **not** attempt to solve:

* Authentication
* Authorisation
* Persistence
* Offline sync
* Performance tuning

These concerns are intentionally out of scope.

---

## Closing Note

These rules exist to make the *wrong thing hard*, not to make the *right thing clever*.

If a rule feels restrictive, that friction is intentional.

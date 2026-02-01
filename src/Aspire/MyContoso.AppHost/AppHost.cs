var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.MyContoso_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.MyContoso_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

// Create a public dev tunnel for iOS and Android
// var publicDevTunnel = builder.AddDevTunnel("devtunnel-public")
//     .WithAnonymousAccess()
//     .WithReference(apiService.GetEndpoint("https"));

var mauiapp = builder.AddMauiProject("mauiapp", "../../UI/MyContoso.App/MyContoso.App.csproj");

// Add Windows device (uses localhost directly)
mauiapp.AddWindowsDevice()
    .WithReference(apiService);

// Add Mac Catalyst device (uses localhost directly)
mauiapp.AddMacCatalystDevice()
    .WithReference(apiService);

// Add iOS simulator with Dev Tunnel
// mauiapp.AddiOSSimulator()
//     .WithOtlpDevTunnel() // Required for OpenTelemetry data collection
//     .WithReference(apiService, publicDevTunnel);
//
// // Add Android emulator with Dev Tunnel
// mauiapp.AddAndroidEmulator()
//     .WithOtlpDevTunnel() // Required for OpenTelemetry data collection
//     .WithReference(apiService, publicDevTunnel);

builder.Build().Run();

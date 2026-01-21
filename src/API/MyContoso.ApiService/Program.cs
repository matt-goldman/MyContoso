using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "MyContoso API service is running");

// Company Updates endpoints
app.MapGet("/company-updates", () => new[]
{
    new CompanyUpdate(1, "Welcome to MyContoso", "We're excited to have you join our team. This is a sample company update to demonstrate the feed functionality.", DateTime.Now.AddDays(-5), "Jane Smith"),
    new CompanyUpdate(2, "Q4 Results Announced", "We're pleased to announce strong Q4 results with 25% growth year over year. Thank you all for your hard work!", DateTime.Now.AddDays(-3), "John Doe"),
    new CompanyUpdate(3, "New Office Opening", "Our new office in Seattle will be opening next month. Stay tuned for more details about the grand opening celebration.", DateTime.Now.AddDays(-1), "Sarah Johnson"),
    new CompanyUpdate(4, "Holiday Schedule", "Please note the upcoming holiday schedule. The office will be closed December 24-26 and December 31-January 1.", DateTime.Now.AddHours(-6), "HR Team")
})
.WithName("GetCompanyUpdates");

app.MapGet("/company-updates/{id}", (int id) =>
{
    var updates = new[]
    {
        new CompanyUpdate(1, "Welcome to MyContoso", "We're excited to have you join our team. This is a sample company update to demonstrate the feed functionality.", DateTime.Now.AddDays(-5), "Jane Smith"),
        new CompanyUpdate(2, "Q4 Results Announced", "We're pleased to announce strong Q4 results with 25% growth year over year. Thank you all for your hard work!", DateTime.Now.AddDays(-3), "John Doe"),
        new CompanyUpdate(3, "New Office Opening", "Our new office in Seattle will be opening next month. Stay tuned for more details about the grand opening celebration.", DateTime.Now.AddDays(-1), "Sarah Johnson"),
        new CompanyUpdate(4, "Holiday Schedule", "Please note the upcoming holiday schedule. The office will be closed December 24-26 and December 31-January 1.", DateTime.Now.AddHours(-6), "HR Team")
    };
    return updates.FirstOrDefault(u => u.UpdateId == id);
})
.WithName("GetCompanyUpdate");

// Employees endpoints
app.MapGet("/employees", () => new[]
{
    new Employee(1, "John Doe", "Software Engineer", "Engineering", "Experienced full-stack developer with a passion for clean code.", "Valid"),
    new Employee(2, "Jane Smith", "Product Manager", "Product", "Strategic product leader focused on customer outcomes.", "Valid"),
    new Employee(3, "Sarah Johnson", "UX Designer", "Design", "Creative designer with expertise in user research and interaction design.", "Valid"),
    new Employee(4, "Michael Brown", "DevOps Engineer", "Engineering", "Infrastructure expert specializing in cloud architecture and automation.", "Pending"),
    new Employee(5, "Emily Davis", "Marketing Manager", "Marketing", "Data-driven marketer with experience in B2B and B2C campaigns.", "Valid"),
    new Employee(6, "David Wilson", "Sales Director", "Sales", "Results-oriented sales leader with a track record of exceeding targets.", "Valid")
})
.WithName("GetEmployees");

app.MapGet("/employees/{id}", (int id) =>
{
    var employees = new[]
    {
        new Employee(1, "John Doe", "Software Engineer", "Engineering", "Experienced full-stack developer with a passion for clean code.", "Valid"),
        new Employee(2, "Jane Smith", "Product Manager", "Product", "Strategic product leader focused on customer outcomes.", "Valid"),
        new Employee(3, "Sarah Johnson", "UX Designer", "Design", "Creative designer with expertise in user research and interaction design.", "Valid"),
        new Employee(4, "Michael Brown", "DevOps Engineer", "Engineering", "Infrastructure expert specializing in cloud architecture and automation.", "Pending"),
        new Employee(5, "Emily Davis", "Marketing Manager", "Marketing", "Data-driven marketer with experience in B2B and B2C campaigns.", "Valid"),
        new Employee(6, "David Wilson", "Sales Director", "Sales", "Results-oriented sales leader with a track record of exceeding targets.", "Valid")
    };
    return employees.FirstOrDefault(e => e.EmployeeId == id);
})
.WithName("GetEmployee");

// Policies endpoints
app.MapGet("/policies", () => new[]
{
    new Policy(1, "Code of Conduct", "Ethics", DateTime.Now.AddMonths(-6), "All employees are expected to maintain the highest standards of professional and ethical conduct..."),
    new Policy(2, "Remote Work Policy", "Workplace", DateTime.Now.AddMonths(-2), "Employees may work remotely up to 3 days per week with manager approval..."),
    new Policy(3, "Data Protection", "Security", DateTime.Now.AddMonths(-4), "All company and customer data must be handled in accordance with applicable regulations..."),
    new Policy(4, "Leave Policy", "HR", DateTime.Now.AddMonths(-8), "Employees accrue 15 days of paid time off annually, prorated for partial years...")
})
.WithName("GetPolicies");

app.MapGet("/policies/{id}", (int id) =>
{
    var policies = new[]
    {
        new Policy(1, "Code of Conduct", "Ethics", DateTime.Now.AddMonths(-6), "All employees are expected to maintain the highest standards of professional and ethical conduct..."),
        new Policy(2, "Remote Work Policy", "Workplace", DateTime.Now.AddMonths(-2), "Employees may work remotely up to 3 days per week with manager approval..."),
        new Policy(3, "Data Protection", "Security", DateTime.Now.AddMonths(-4), "All company and customer data must be handled in accordance with applicable regulations..."),
        new Policy(4, "Leave Policy", "HR", DateTime.Now.AddMonths(-8), "Employees accrue 15 days of paid time off annually, prorated for partial years...")
    };
    return policies.FirstOrDefault(p => p.PolicyId == id);
})
.WithName("GetPolicy");

app.MapDefaultEndpoints();

app.Run();

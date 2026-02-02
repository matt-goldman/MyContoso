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
app.MapGet("/company-updates", () => Mocks.Updates)
.WithName("GetCompanyUpdates");

app.MapGet("/company-updates/{id}", (int id) =>
{
    return Mocks.Updates.FirstOrDefault(u => u.UpdateId == id);
})
.WithName("GetCompanyUpdate");

// Employees endpoints
app.MapGet("/employees", () => Mocks.Employees)
.WithName("GetEmployees");

app.MapGet("/employees/{id}", (int id) =>
{
    return Mocks.Employees.FirstOrDefault(e => e.EmployeeId == id);
})
.WithName("GetEmployee");

// Policies endpoints
app.MapGet("/policies", () => Mocks.Policies)
.WithName("GetPolicies");

app.MapGet("/policies/{id}", (int id) => Mocks.Policies.FirstOrDefault(p => p.PolicyId == id))
.WithName("GetPolicy");

app.MapGet("/accreditations", () => Mocks.Accreditations);

app.MapGet("/accreditations/{id}", (int id) => Mocks.Accreditations.FirstOrDefault(a => a.AccreditationId == id));

app.MapDefaultEndpoints();

app.Run();

public static class Mocks
{
    private static readonly Random Random = new Random();
    
    public static readonly List<CompanyUpdate> Updates = 
    [
        new CompanyUpdate(
            1,
            "Welcome to MyContoso",
            "We're excited to have you join our team. This is a sample company update to demonstrate the feed functionality.",
            DateTime.Now.AddDays(-5),
            "Jane Smith",
            "Chief Executive Officer",
            Random.Next(0,45),
            Random.Next(0,100),
            Random.Next(0,100) % 3 == 0),
        new CompanyUpdate(
            2,
            "Q4 Results Announced",
            "We're pleased to announce strong Q4 results with 25% growth year over year. Thank you all for your hard work!",
            DateTime.Now.AddDays(-3),
            "John Doe",
            "Senior Account Manager",
            Random.Next(0,45),
            Random.Next(0,100),
            Random.Next(0,100) % 3 == 0),
        new CompanyUpdate(
            3,
            "New Office Opening",
            "Our new office in Seattle will be opening next month. Stay tuned for more details about the grand opening celebration.",
            DateTime.Now.AddDays(-1), 
            "Sarah Johnson",
            "Head of People and Culture",
            Random.Next(0,45),
            Random.Next(0,100),
            Random.Next(0,100) % 3 == 0),
        new CompanyUpdate(
            4,
            "Holiday Schedule",
            "Please note the upcoming holiday schedule. The office will be closed December 24-26 and December 31-January 1.",
            DateTime.Now.AddHours(-6),
            "Sarah Johnson",
            "Head of People and Culture",
            Random.Next(0,45),
            Random.Next(0,100),
            Random.Next(0,100) % 3 == 0)
    ];

    public static readonly List<Employee> Employees =
    [
        new Employee(
            1,
            "John Doe",
            "Software Engineer",
            "Engineering",
            "Experienced full-stack developer with a passion for clean code.",
            [
                new Accreditation(5, "Software Engineer II", "Qualified to maintain software products", "Valid", DateTime.Now.AddMonths(5))
                
            ],
            new ContactInfo(
                "john.doe@contoso.com",
                "+61 (0)2 9456 3209",
                "1 Contoso Plaza, Sydney, NSW, 2000, Australia",
                DateTime.Now.AddDays(-3).AddMonths(6).AddYears(-27)
                )),
        new Employee(
            2, 
            "Jane Smith", 
            "Product Manager",
            "Product",
            "Strategic product leader focused on customer outcomes.",
            [
                new Accreditation(5, "Software Engineer II", "Qualified to maintain software products", "Valid", DateTime.Now.AddMonths(5))
                
            ],
            new ContactInfo(
                "john.doe@contoso.com",
                "+61 (0)2 9456 3209",
                "1 Contoso Plaza, Sydney, NSW, 2000, Australia",
                DateTime.Now.AddDays(-3).AddMonths(6).AddYears(-27)
            )),
        new Employee(
            3,
            "Sarah Johnson",
            "UX Designer",
            "Design",
            "Creative designer with expertise in user research and interaction design.",
            [
                new Accreditation(5, "Software Engineer II", "Qualified to maintain software products", "Valid", DateTime.Now.AddMonths(5))
                
            ],
            new ContactInfo(
                "john.doe@contoso.com",
                "+61 (0)2 9456 3209",
                "1 Contoso Plaza, Sydney, NSW, 2000, Australia",
                DateTime.Now.AddDays(-3).AddMonths(6).AddYears(-27)
            )),
        new Employee(
            4,
            "Michael Brown",
            "DevOps Engineer",
            "Engineering",
            "Infrastructure expert specializing in cloud architecture and automation.",
            [
                new Accreditation(5, "Software Engineer II", "Qualified to maintain software products", "Valid", DateTime.Now.AddMonths(5))
                
            ],
            new ContactInfo(
                "john.doe@contoso.com",
                "+61 (0)2 9456 3209",
                "1 Contoso Plaza, Sydney, NSW, 2000, Australia",
                DateTime.Now.AddDays(-3).AddMonths(6).AddYears(-27)
            )),
        new Employee(
            5,
            "Emily Davis",
            "Marketing Manager",
            "Marketing",
            "Data-driven marketer with experience in B2B and B2C campaigns.",
            [
                new Accreditation(5, "Software Engineer II", "Qualified to maintain software products", "Valid", DateTime.Now.AddMonths(5))
                
            ],
            new ContactInfo(
                "john.doe@contoso.com",
                "+61 (0)2 9456 3209",
                "1 Contoso Plaza, Sydney, NSW, 2000, Australia",
                DateTime.Now.AddDays(-3).AddMonths(6).AddYears(-27)
            )),
        new Employee(
            6, 
            "David Wilson",
            "Sales Director",
            "Sales",
            "Results-oriented sales leader with a track record of exceeding targets.",
            [
                new Accreditation(5, "Software Engineer II", "Qualified to maintain software products", "Valid", DateTime.Now.AddMonths(5))
                
            ],
            new ContactInfo(
                "john.doe@contoso.com",
                "+61 (0)2 9456 3209",
                "1 Contoso Plaza, Sydney, NSW, 2000, Australia",
                DateTime.Now.AddDays(-3).AddMonths(6).AddYears(-27)
            ))
    ];

    public static readonly List<Policy> Policies =
    [
        new Policy(
            1,
            "Code of Conduct",
            "Ethics",
            DateTime.Now.AddMonths(-6),
            "Joan Smith",
            "The latest code of conduct is now available for all staff. There are mandatory changes that all employees need to become familiar with.",
            "All employees are expected to maintain the highest standards of professional and ethical conduct..."
            ),
        new Policy(
            2,
            "Remote Work Policy",
            "Workplace",
            DateTime.Now.AddMonths(-2),
            "Michale Brown",
            "Contoso has a new remote work policy. Familiarise yourself with the new rules to learn whether you are eligible.",
            ""),
        new Policy(
            3,
            "Data Protection",
            "Security",
            DateTime.Now.AddMonths(-4),
            "Christine Lawson",
            "All company and customer data must be handled in accordance with applicable regulations. New controls and guidelines are now in effect",
            ""),
        new Policy(4,
            "Leave Policy",
            "HR",
            DateTime.Now.AddMonths(-8),
            "Jordon Collier",
            "Understand your rights and entitlements, and obligations, with regard to leave.",
            "")
    ];

    public static readonly List<Accreditation> Accreditations =
    [
        new (1,
            "Health and Safety Certificate", 
            "Employees with this accreditation are permitted to work from any Contoso site, as well as their home address upon agreement. Ensure you keep this up to date.",
            "Overdue",
            DateTime.Now.AddDays(-3)),
        new (2,
            "Accounting Level 1",
            "Required for all staff wishing to work in Contoso's Finance department.",
            "Pending",
            DateTime.Today),
        new (3,
            "People Leadership",
            "This accreditation signifies that an employee has met the requirements to hold a management level position at Contoso.",
            "Valid",
            DateTime.Today.AddMonths(6)),
        new (4,
            "Bomb Disposal",
            "Advanced qualification for Contoso security staff that qualifies employees to act and disarm explosives.",
            "Valid",
            DateTime.Today.AddMonths(9))
        
    ];
}
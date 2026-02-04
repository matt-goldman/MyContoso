namespace MyContoso.App.ApiModels;

public record Employee(
    int EmployeeId,
    string Name,
    string Role,
    string Department,
    string ProfileSummary,
    List<Accreditation> Accreditations,
    ContactInfo ContactInfo,
    string? AvatarUrl
);

public record ContactInfo(
    string Email,
    string Phone,
    string Address,
    DateTime? DateOfBirth
);

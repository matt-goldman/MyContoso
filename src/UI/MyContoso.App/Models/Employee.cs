namespace MyContoso.App.Models;

public record Employee(
    int EmployeeId,
    string Name,
    string Initials,
    string Role,
    string Department,
    string ProfileSummary,
    string Email,
    string Phone,
    string Address,
    DateTime? DateOfBirth,
    List<Accreditation> Accreditations,
    string? AvatarUrl
);

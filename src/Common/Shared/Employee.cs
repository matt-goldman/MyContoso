namespace Shared;

public record Employee(
    int EmployeeId,
    string Name,
    string Role,
    string Department,
    string ProfileSummary,
    List<Accreditation> Accreditations,
    ContactInfo ContactInfo
    // TODO: add an AvatarUrl property and populate using a people API
);

public record ContactInfo(
    string Email,
    string Phone,
    string Address,
    DateTime? DateOfBirth
    );

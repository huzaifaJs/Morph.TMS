namespace Morpho.Domain.Common;

/// <summary>
///  Represents a contact person with role, name, email, phone, and primary contact flag.
/// </summary>
public class Contact
{
    public string Role { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public bool IsPrimary { get; set; }
}
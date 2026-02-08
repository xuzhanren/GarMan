using System.ComponentModel.DataAnnotations;

namespace GarMan.ViewModels;

public class UserViewModel
{
    public string? Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "User Name")]
    public string? UserName { get; set; }

    public List<string> Roles { get; set; } = new();
}

public class CreateUserViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class EditUserViewModel
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public List<string> AvailableRoles { get; set; } = new();
    public List<string> SelectedRoles { get; set; } = new();
}

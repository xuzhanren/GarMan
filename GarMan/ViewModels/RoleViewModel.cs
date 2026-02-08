using System.ComponentModel.DataAnnotations;

namespace GarMan.ViewModels;

public class RoleViewModel
{
    public string? Id { get; set; }

    [Required]
    [Display(Name = "Role Name")]
    public string Name { get; set; } = string.Empty;
}

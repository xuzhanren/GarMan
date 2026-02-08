using System.ComponentModel.DataAnnotations;

namespace GarMan.Models;

public class RecyclingCenter
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Center Name")]
    public string CenterName { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? State { get; set; }

    [StringLength(10)]
    [Display(Name = "ZIP Code")]
    public string? ZipCode { get; set; }

    [Phone]
    [StringLength(20)]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    [Display(Name = "Operating Hours")]
    [StringLength(200)]
    public string? OperatingHours { get; set; }

    [Display(Name = "Accepted Appliances")]
    [StringLength(500)]
    public string? AcceptedAppliances { get; set; }

    [Display(Name = "Is Active")]
    public bool IsActive { get; set; } = true;
}

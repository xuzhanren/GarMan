using System.ComponentModel.DataAnnotations;

namespace GarMan.Models;

public class Appliance
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Appliance Type")]
    public string ApplianceType { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Brand { get; set; }

    [StringLength(50)]
    [Display(Name = "Model Number")]
    public string? ModelNumber { get; set; }

    [Display(Name = "Year of Manufacture")]
    public int? YearOfManufacture { get; set; }

    [Required]
    public ApplianceCondition Condition { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Display(Name = "Weight (kg)")]
    public decimal? Weight { get; set; }

    [Display(Name = "Collection Date")]
    public DateTime? CollectionDate { get; set; }

    [Display(Name = "Collection Address")]
    [StringLength(200)]
    public string? CollectionAddress { get; set; }

    [Display(Name = "Collection Fee")]
    [DataType(DataType.Currency)]
    public decimal CollectionFee { get; set; } = 50;

    [Required]
    public CollectionStatus Status { get; set; } = CollectionStatus.Pending;

    [Display(Name = "Recycling Status")]
    public RecyclingStatus RecyclingStatus { get; set; } = RecyclingStatus.NotProcessed;

    [Display(Name = "Estimated Value")]
    [DataType(DataType.Currency)]
    public decimal? EstimatedValue { get; set; }

    [Display(Name = "Recycled Value")]
    [DataType(DataType.Currency)]
    public decimal? RecycledValue { get; set; }

    [Display(Name = "Recycling Comment")]
    [StringLength(500)]
    public string? RecyclingComment { get; set; }

    [Display(Name = "Date Submitted")]
    public DateTime DateSubmitted { get; set; } = DateTime.Now;

    [Display(Name = "Last Updated")]
    public DateTime? LastUpdated { get; set; }

    // Navigation property for the user who submitted the appliance
    public string? UserId { get; set; }

    [Display(Name = "Customer Name")]
    [StringLength(100)]
    public string? CustomerName { get; set; }

    [Display(Name = "Customer Phone")]
    [StringLength(20)]
    public string? CustomerPhone { get; set; }

    [Display(Name = "Customer Email")]
    [EmailAddress]
    [StringLength(100)]
    public string? CustomerEmail { get; set; }
}

public enum ApplianceCondition
{
    Working,
    [Display(Name = "Partially Working")]
    PartiallyWorking,
    [Display(Name = "Not Working")]
    NotWorking,
    [Display(Name = "For Parts")]
    ForParts
}

public enum CollectionStatus
{
    Pending,
    Scheduled,
    [Display(Name = "In Transit")]
    InTransit,
    Collected,
    Cancelled
}

public enum RecyclingStatus
{
    [Display(Name = "Not Processed")]
    NotProcessed,
    [Display(Name = "In Processing")]
    InProcessing,
    Recycled,
    [Display(Name = "Disposed")]
    Disposed,
    Resold
}

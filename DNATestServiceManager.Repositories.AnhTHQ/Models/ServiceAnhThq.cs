using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DNATestServiceManager.Repositories.AnhTHQ.Models;

public partial class ServiceAnhThq
{
    [Key]
    public int ServiceAnhThqid { get; set; }

    [Required(ErrorMessage = "Service name is required")]
    [StringLength(100, ErrorMessage = "Service name cannot exceed 100 characters")]
    public string ServiceName { get; set; }

    [Required(ErrorMessage = "Service type is required")]
    [StringLength(50, ErrorMessage = "Service type cannot exceed 50 characters")]
    public string ServiceType { get; set; }

    [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
    public string Description { get; set; }

    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
    public string Category { get; set; }

    [DataType(DataType.Date)]
    public DateTime? CreatedDate { get; set; }

    [StringLength(100, ErrorMessage = "CreatedBy cannot exceed 100 characters")]
    public string CreatedBy { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(100, ErrorMessage = "ModifiedBy cannot exceed 100 characters")]
    public string ModifiedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<BookingMinhNDA> BookingMinhNda { get; set; } = new List<BookingMinhNDA>();

    public virtual ICollection<ServicePriceListAnhThq> ServicePriceListAnhThqs { get; set; } = new List<ServicePriceListAnhThq>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CreatedDate.HasValue && ModifiedDate.HasValue)
        {
            if (CreatedDate.Value > ModifiedDate.Value)
            {
                yield return new ValidationResult(
                    "Created date cannot be later than Modified date.",
                    new[] { nameof(CreatedDate), nameof(ModifiedDate) });
            }
        }
    }
}

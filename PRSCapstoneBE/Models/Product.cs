using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCapstoneBE.Models
{
    [Index("PartNbr", IsUnique = true, Name = "UID_PartNbr")] //PartNbr must be unique, not PK. Vendors identifier for the product
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string PartNbr { get; set; }

        [StringLength(30)]
        public string Name { get; set; } //Name of product from Company

        [Column(TypeName = "decimal(11,2)")]
        public decimal Price { get; set; }

        [StringLength(30)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string? PhotoPath { get; set; }

        public int VendorId { get; set; } //FK to Vendor, points to vendor that supplies product
        public virtual Vendor? Vendor { get; set; } //Virtual instance for FK to Vendor not present on db table

    }
}

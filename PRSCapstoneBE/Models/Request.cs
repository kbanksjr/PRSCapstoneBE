using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCapstoneBE.Models
{
    public class Request
    {

        [Key]
        public int Id { get; set; }

        [StringLength(80)]
        public string Description { get; set; }

        [StringLength(80)]
        public string Justification { get; set; }

        [StringLength(80)]
        public string? RejectionReason { get; set; } //Must be provided when request is rejected

        [StringLength(20)]
        public string DeliveryMode { get; set; } = "Pickup";

        [StringLength(10)]
        public string Status { get; set; } = "NEW"; //Can't be set by user, done by application

        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; } = 0; //Can't be set by user, done by application. Also auto calculated by adding up lines currently in the request


        public int UserId { get; set; } //Is auto set to the Id of the logged in user, FK to user
        public virtual User? User { get; set; } //Virtual instance of User when reading request, FK to User

        public virtual IEnumerable<RequestLine>? RequestLines { get; set; } //Virtual collection of requestline instances, holds collection of lines related to the request
    }
}

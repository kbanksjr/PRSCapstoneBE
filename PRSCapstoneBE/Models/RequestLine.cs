using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRSCapstoneBE.Models
{
    public class RequestLine
    {
        [Key]
        public int Id { get; set; }


        public int RequestId { get; set; }
        [JsonIgnore]
        public virtual Request? Request { get; set; } //Virual Request(class) instance to hold FK instance to Request, resolved cyclical error with JSONIgnore

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; } //Virtual instance to hold FK instance to product

        public int Quantity { get; set; } = 1; //Must be greater than or equal to 0 (can't be negative)


    }
}

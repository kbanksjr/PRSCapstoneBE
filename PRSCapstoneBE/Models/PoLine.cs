using Microsoft.EntityFrameworkCore;

namespace PRSCapstoneBE.Models
{
    [Keyless]
    public class PoLine
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal LineTotal { get; set; }
        
    }
}

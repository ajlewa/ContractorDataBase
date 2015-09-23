using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contractor_DataBase.DataModels
{
    public class PositionsQty
    {
        [Key]
        public int Id { get; set; }
        public string Position { get; set; }
        public int Qty { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}

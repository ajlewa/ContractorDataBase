using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

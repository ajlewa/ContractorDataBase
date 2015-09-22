using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Contractor_DataBase.DataModels
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public virtual ICollection<PositionsQty> PositionsQty { get; set; }
        public decimal Sum { get; set; }
        public string InvoiceNumber { get; set; }
        public Byte[] InvoiceScan { get; set; }
        public Byte[] ClosingDocScan { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        [ForeignKey("Contractor")]
        public int ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }

        public override string ToString()
        {
            return "Счет №: " + InvoiceNumber + " от " + OrderDate.Day + "/" + OrderDate.Month + "/" + OrderDate.Year;
        }
    }
}

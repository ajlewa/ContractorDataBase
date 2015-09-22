using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Contractor_DataBase.DataModels
{
    public class ContractorContext : DbContext
    {
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PositionsQty> PositionsQties { get; set; }
    }
}

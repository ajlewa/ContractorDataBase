﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contractor_DataBase.DataModels
{
    public class Contractor
    {
        [Key]
        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public string ManagerName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public ICollection<Order> Orders { get; set; }
        public override string ToString()
        {
            return ContractorName;
        }
    }
}

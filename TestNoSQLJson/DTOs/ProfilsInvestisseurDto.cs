using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNoSQLJson.DTOs
{
    public class ProfilInvestisseurDto
    {
        public decimal Version { get; set; }
        public int SubscriberId { get; set; }
        public IDictionary<string, IDictionary<string, string>> ContentLines { get; set; }
    }
}

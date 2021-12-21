using System.Collections.Generic;

namespace TestNoSQLJson.DTOs
{
    public class ProfilInvestisseurDto
    {
        public int ProfilInvestisseurId { get; set; }
        public int SubscriberId { get; set; }
        public IList<ProfilLineDto> Content { get; set; }
    }
}

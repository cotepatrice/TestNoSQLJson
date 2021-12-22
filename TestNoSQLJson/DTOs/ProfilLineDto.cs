using TestNoSQLJson.Common;

namespace TestNoSQLJson.DTOs
{
    public class ProfilLineDto
    {
        public string FieldName { get; set; }
        public decimal LabelVersion { get; set; }
        public string LabelText { get; set; }
        public string Value { get; set; }

        public DotNetProfileModelType DotNetProfileModelType { get; set; }
    }
}

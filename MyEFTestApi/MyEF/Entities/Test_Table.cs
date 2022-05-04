using MyEF.Attributes;

namespace MyEF.Entities
{
    [TableAttribute("TestTable")]
    public class Test_Table : BaseModel
    {
        public int TsetInt { get; set; }
        public string? TsetString { get; set; }
        public DateTime? TsetDateTime { get; set; }
        public bool TsetBool { get; set; }
    }
}

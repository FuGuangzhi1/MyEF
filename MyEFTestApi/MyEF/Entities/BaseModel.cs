
using MyEF.Attributes;

namespace MyEF.Entities
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

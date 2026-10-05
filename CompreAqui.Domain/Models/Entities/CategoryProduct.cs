
using CompreAqui.Domain.Attributes;

namespace CompreAqui.Domain.Models.Entities
{
    [Entity("categories", "System categories", true)]
    public class CategoryProduct : EntityModels
    {
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public string LastAccess { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }
    }
}

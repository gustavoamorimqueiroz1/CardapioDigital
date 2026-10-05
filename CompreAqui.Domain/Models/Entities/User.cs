using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Contracts;
using System;

namespace CompreAqui.Domain.Models.Entities
{
    [Entity("users", "System users", true)]
    public class User : EntityModels
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long FacebookId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Rating { get; set; }
        public DateTime LastAcess { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

    }
}
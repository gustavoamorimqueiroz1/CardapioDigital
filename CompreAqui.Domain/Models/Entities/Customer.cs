using CompreAqui.Domain.Attributes;
using System;

namespace CompreAqui.Domain.Models.Entities
{
    [Entity("customers", "System customers", true)]
    public class Customer : EntityModels
    {
        public string CompanyName { get; set; }
        public string ImageUrl { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public double DeliveryFee { get; set; }
        public bool CreditCard { get; set; }
        public bool Opened { get; set; }
        public double MinimumValue { get; set; }
        public bool DeliveryEndOfDay { get; set; }
        public bool UpdateStatusManualy { get; set; }
        public DateTime OpenMonday { get; set; }
        public DateTime OpenTuesday { get; set; }
        public DateTime OpenWednesday { get; set; }
        public DateTime OpenThursday { get; set; }
        public DateTime OpenFriday { get; set; }
        public DateTime OpenSaturday { get; set; }
        public DateTime OpenSunday { get; set; }
        public DateTime CloseMonday { get; set; }
        public DateTime CloseTuesday { get; set; }
        public DateTime CloseWednesday { get; set; }
        public DateTime CloseThursday { get; set; }
        public DateTime CloseFriday { get; set; }
        public DateTime CloseSaturday { get; set; }
        public DateTime CloseSunday { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string AddressNeighborhood { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressReference { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string BusinessHours { get; set; }
        public DateTime LastAccess { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}

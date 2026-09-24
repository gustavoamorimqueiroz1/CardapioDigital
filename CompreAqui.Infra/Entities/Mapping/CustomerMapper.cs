using CompreAqui.Domain.Models.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CompreAqui.Infra.Entities.Mapping
{
    public class CustomerMapper : DommelEntityMap<Customer>, IMapper
    {
        public CustomerMapper()
        {
            ToTable("customers");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Guid).ToColumn("guid");
            Map(x => x.CompanyName).ToColumn("companyname");
            Map(x => x.ImageUrl).ToColumn("imageurl");
            Map(x => x.Cnpj).ToColumn("cnpj");
            Map(x => x.Email).ToColumn("email");
            Map(x => x.Password).ToColumn("password");
            Map(x => x.Phonenumber).ToColumn("phonenumber");
            Map(x => x.DeliveryFee).ToColumn("deliveryfee");
            Map(x => x.CreditCard).ToColumn("creditcard");
            Map(x => x.Opened).ToColumn("opened");
            Map(x => x.MinimumValue).ToColumn("minimumvalue");
            Map(x => x.DeliveryEndOfDay).ToColumn("deliveryendofday");
            Map(x => x.UpdateStatusManualy).ToColumn("updatestatusmanualy");
            Map(x => x.OpenMonday).ToColumn("openmonday");
            Map(x => x.OpenTuesday).ToColumn("opentuesday");
            Map(x => x.OpenWednesday).ToColumn("openwednesday");
            Map(x => x.OpenThursday).ToColumn("openthursday");
            Map(x => x.OpenFriday).ToColumn("openfriday");
            Map(x => x.OpenSaturday).ToColumn("opensaturday");
            Map(x => x.OpenSunday).ToColumn("opensunday");
            Map(x => x.CloseMonday).ToColumn("closemonday");
            Map(x => x.CloseTuesday).ToColumn("closetuesday");
            Map(x => x.CloseWednesday).ToColumn("closewednesday");
            Map(x => x.CloseThursday).ToColumn("closethursday");
            Map(x => x.CloseFriday).ToColumn("closefriday");
            Map(x => x.CloseSaturday).ToColumn("closesaturday");
            Map(x => x.CloseSunday).ToColumn("closesunday");
            Map(x => x.AddressZipCode).ToColumn("addresszipcode");
            Map(x => x.AddressStreet).ToColumn("addressstreet");
            Map(x => x.AddressNumber).ToColumn("addressnumber");
            Map(x => x.AddressComplement).ToColumn("addresscomplement");
            Map(x => x.AddressNeighborhood).ToColumn("addressneighborhood");
            Map(x => x.AddressDistrict).ToColumn("addressdistrict");
            Map(x => x.AddressReference).ToColumn("addressreference");
            Map(x => x.AddressCity).ToColumn("addresscity");
            Map(x => x.AddressState).ToColumn("addressstate");
            Map(x => x.BusinessHours).ToColumn("businesshours");
            Map(x => x.LastAccess).ToColumn("lastacess");
            Map(x => x.CreatedAt).ToColumn("createdat");
            Map(x => x.UpdatedAt).ToColumn("updatedat");
            Map(x => x.DeletedAt).ToColumn("deletedat");
        }
    }
}
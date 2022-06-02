using System.Text.Json.Serialization;

namespace ClassDijagramV1._0.Model
{
    public class Address
    {
        #region Properties
        public string Country { get; set; }

        public string City { get; set; }

        public string StreetName { get; set; }

        // Moze da bude /,BB
        public string StreetNumber { get; set; }

        #endregion

        #region Constructor

        public Address(string country, string city, string streetname, string streetnum)
        {
            Country = country;
            City = city;
            StreetName = streetname;
            StreetNumber = streetnum;
        }

        [JsonConstructor]
        public Address()
        {

        }

        #endregion
    }
}

namespace ClickNCheck.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public AddressType AddressType { get; set; }
    }
}
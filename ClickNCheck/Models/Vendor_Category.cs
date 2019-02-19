namespace ClickNCheck.Models
{
    public class Vendor_Category
    {
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int CheckCategoryId { get; set; }
        public CheckCategory CheckCategory { get; set; }
    }
}
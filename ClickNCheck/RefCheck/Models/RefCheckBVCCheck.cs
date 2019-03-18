using System.ComponentModel.DataAnnotations;
using ClickNCheck.RefCheck.DocType;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckBVCCheck
    {
        [Required]
        [MaxLength(20)]
        public int AccountNumber { get; set; }
        [Required]
        public AccountTypes AccountTypeCode { get; set; }
        [Required]
        public BankCodes BankCode { get; set; }
        [Required]
        public string CheckKey { get; set; }
        [Required]
        public string IDNumberCompanyRegistrationNumber { get; set; }
        [MaxLength(2)]
        public string Initials { get; set; }
        [Required]
        [MaxLength(30)]
        public string SurnameBusinessName { get; set; }
    }
}

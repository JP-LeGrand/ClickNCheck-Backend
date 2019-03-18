using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckBVCCheck
    {
        [Required]
        [MaxLength(20)]
        public int AccountNumber { get; set; }
        [Required]
        public int AccountTypeCode { get; set; }
        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public string BankCode { get; set; }
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

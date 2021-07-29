using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication4.Models
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }

        public string CurrencyName { get; set; }

        [Required]
        public string CurrencyCharCode { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal Amount { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal InputAmount { get; set; }

        public SelectListItem[] AvailableCharCodes { get; set; }

        public byte[] RowVersion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rambars.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Bar
    {
        // Primary key
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int ZipCode { get; set; }
    }
}
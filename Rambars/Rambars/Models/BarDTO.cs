using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rambars.Models
{
    public class BarDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Special> Specials { get; set; }
    }

    public class BarDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public List<Special> Specials { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rambars.Models
{
    public class SpecialDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarName { get; set; }
    }

    public class SpecialDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string BarName { get; set; }
        public string Description { get; internal set; }
    }
}
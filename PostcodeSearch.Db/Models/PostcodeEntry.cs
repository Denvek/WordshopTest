using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostcodeSearch.Db.Models
{
    public class PostcodeEntry
    {
        [Key]
        public int Id { get; set; }

        public string? Postcode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}

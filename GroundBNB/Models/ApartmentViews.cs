using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GroundBNB.Models
{
    public class ApartmentViews
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        public int ApartmentID { get; set; }

        [JsonIgnore]
        public Apartment Apartment { get; set; }
        public int Views { get; set; }
    }
}

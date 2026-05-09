using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Testimonial
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ClientName { get; set; }
        public string ClientRole { get; set; }
        public string Company { get; set; }
        [Required]
        public string Feedback { get; set; }
        public string ImageUrl { get; set; }
    }
}

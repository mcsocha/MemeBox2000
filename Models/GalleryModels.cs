using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MemeBox2000.Models
{
    [Serializable]
    public class Meme
    {
        public string ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Genre { get; set; }

        public string FileExtension { get; set; }
    }
}

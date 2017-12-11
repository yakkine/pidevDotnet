using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ReclamationViewModels
    {
        public int? etat { get; set; }

        [StringLength(255)]
        public string sujet { get; set; }
    }
}
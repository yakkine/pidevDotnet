using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class CommentViewModels
    {



        public int id { get; set; }

      
        public string description { get; set; }

        public DateTime? postdate { get; set; }

        public int? documment_id { get; set; }

        public int? utilisateur_id { get; set; }

    }
}
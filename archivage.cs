using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class archivage
    {
        public int id { get; set; }
        public string version { get; set; }
        public Nullable<int> document_id { get; set; }
        public Nullable<int> user_id { get; set; }
        [ForeignKey("document_id")]
        public virtual document document { get; set; }
        [ForeignKey("user_id")]
        public virtual user user { get; set; }
    }
}

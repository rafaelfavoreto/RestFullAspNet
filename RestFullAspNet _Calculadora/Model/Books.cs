using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullAspNet.Model
{
    [Table("Books")]
    public class Books
    {
        [Column("id")]
        public long id { get; set; }
        [Column("author")]
        public string author { get; set; }
        [Column("launch_date")]
        public DateTime launch_date { get; set; }
        [Column("price")]
        public decimal price { get; set; }
        [Column("title")]
        public string title { get; set; }

    }
}

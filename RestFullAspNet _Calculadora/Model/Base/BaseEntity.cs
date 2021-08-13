using System.ComponentModel.DataAnnotations.Schema;

namespace RestFullAspNet.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}

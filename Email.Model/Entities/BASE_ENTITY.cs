using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Model.Entities
{
    public class BASE_ENTITY
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}

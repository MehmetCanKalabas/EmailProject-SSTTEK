using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Model.Entities
{
    public class EMAIL_LOG : BASE_ENTITY
    {
        //[MaxLength(50)]
        //public string From { get; set; }
        [MaxLength(50)]
        public string To { get; set; }
        //[MaxLength(50)]
        //public string BCC { get; set; }
        //[MaxLength(50)]
        //public string CCC { get; set; }
        [MaxLength(50)]
        public string Subject { get; set; }
        [MaxLength(500)]
        public string Body { get; set; }
    }
}

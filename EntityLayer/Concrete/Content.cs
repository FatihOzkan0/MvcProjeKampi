using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Content
    {
        [Key]
        public int ContentID { get; set; }
        
        [StringLength(1000)]
        public string ContentValue { get; set; }
        public  DateTime ContentDate { get; set; }

        public int HeadingID { get; set; }
        public virtual Heading Headings { get; set; }    //Heading Tablosu ile ilişkilendirdiğimiz içinn böyle yapıyoruz.
        public int? WriterID { get; set; }              //nullable type "?" tanımlıyoruz burayı.
        public virtual Writer Writer { get; set; }


    }
}

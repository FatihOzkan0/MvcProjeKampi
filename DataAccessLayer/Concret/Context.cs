using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret
{
    public class Context:DbContext                  
        
        //İlk olarak EF' yi paketlerimize ekliyoruz sonra DbContext' den kalıtım yapıyoruz ve daha sonra tablolara EntityLayerdan erişiceğimiz için referancelara EntityLayer katmanını ekliyoruz.
        //DbSet tek tek yazılanları sql'e tablo olarak oluşturucak.
    {
        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Heading> Headings { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Message> Messages { get; set; }      //Bu Sınıfı projemize sonradan ekledik.
        public DbSet<ImageFile> ImageFiles { get; set; }



        




    }
}

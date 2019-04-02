using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeresiyeDefteri.BOL.Entities
{
    [Table("MBorc")]
   public class MBorc
    {
        public int ID { get; set; }
        public int Miktar { get; set; }
        public DateTime VTarih { get; set; }
        [Column(TypeName = "varchar"), StringLength(150)]
        public string Aciklama { get; set; }
        public int UrunID { get; set; }
        public Urun Urun { get; set; }
        public int MusteriID { get; set; }
        public Musteri Musteri { get; set; }



    }
}

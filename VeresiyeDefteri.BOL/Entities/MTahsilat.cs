using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeresiyeDefteri.BOL.Entities
{
    [Table("MTahsilat")]
    public class MTahsilat
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(30)]
        public string Aciklama { get; set; }
        public decimal OFiyat { get; set; }
        public EOdemeTur OdemeTur { get; set; }
        public DateTime ITarih { get; set; }
        public int MusteriID { get; set; }
        public Musteri Musteri { get; set; }
    }
    public enum EOdemeTur
    {
        NAKİT=1,
        KART,
        SENET,
        ÇEK
    }
}

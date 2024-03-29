﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeresiyeDefteri.BOL.Entities
{
    [Table("TBorc")]
   public class TBorc
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(30)]
        public string FaturaNo { get; set; }
        [Column(TypeName = "varchar"), StringLength(130)]
        public string Aciklama { get; set; }
        public DateTime VTarih { get; set; }
        public decimal Fiyat { get; set; }
        public int TedarikciID { get; set; }
        public Tedarikci Tedarikci { get; set; }



    }
}

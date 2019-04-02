using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeresiyeDefteri.BOL.Entities;

namespace VeresiyeDefteri.DAL.Contexts
{
    public class SqlDbContext:DbContext
    {
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Tedarikci> Tedarikciler { get; set; }
        public DbSet<TBorc> TBorclar { get; set; }
        public DbSet<MBorc> MBorclar { get; set; }
        public DbSet<TTahsilat> TTahsilatlar { get; set; }
        public DbSet<MTahsilat> MTahsilatlar { get; set; }
        public DbSet<Urun> Urunler { get; set; }
    }
}

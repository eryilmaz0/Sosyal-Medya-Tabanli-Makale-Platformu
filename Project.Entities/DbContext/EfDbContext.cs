using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Core.Entities;
using Project.Entities.Entities;

namespace Project.Entities.DbContext
{
    public class EfDbContext : IdentityDbContext<User,Role,long>
    {
        

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            //SİLİNECEK OLARAK İŞARETLENEN ENTİTYLERİ BUL
            var markedAsDeleted = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);


            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is ISoftDeletable entity)
                {
                    /*NORMAL UPDATE YAPILDIĞINDA, TÜM ENTİTY CHANGED OLARAK İŞARETLENİR, DBYE NESNENİN TAMAMI GÖNDERİLİR DEĞİŞMİŞ ALANLAR GÜNCELLENİR.
                    BURADA ENTİTYİ UNCHANGED OLARAK AYARLAYIP ARDINDAN ISDELETE ALANINI DEĞİŞTİREREK, GÜNCELLEME İFADESİNİ İSDELETE ALANI İLE SINIRLADIK.  */
                    item.State = EntityState.Unchanged;
                    entity.IsDeleted = true;
                }
            }

            return base.SaveChanges();
        }

    }
}
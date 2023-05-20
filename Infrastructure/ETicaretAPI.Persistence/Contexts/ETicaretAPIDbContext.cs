using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbContext : DbContext
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: Entity'ler üzerinden yapılan değişiklilerin ya da yeni eklenen verinin yakalanmasını sağlayan yapıdır. update operasyonunda Track edilen verilerin yakalanıp elde etmenizi sağlar. 
            // Biz herhangi bir veri eklenirken veya güncellenirken  savechangesasync tetiklersek burada ilk önce buradaki override tetiklenecek. gelen datalar yakalanacak, bu datalar üzerinde state'e göre düzenleyip ona göre savechanges çalıştırılacaktır.
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FurnitureStore.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("FurnitureStore")
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<ItensPedido> ItensPedidos { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Foto> Fotos { get; set; }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
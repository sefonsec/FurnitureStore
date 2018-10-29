using System.Data.Entity;

namespace FurnitureStore.Models
{
    public class DataContextInitializer : DropCreateDatabaseAlways<DataContext>
    {
    }
}
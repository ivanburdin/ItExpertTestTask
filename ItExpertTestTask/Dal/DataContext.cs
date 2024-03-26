using ItExpertTestTask.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace ItExpertTestTask.Dal;

public class DataContext : DbContext
{
    public DbSet<DataValueDb> DataValues { get; set; }
    public DbSet<HttpLogMessageDb> Logs { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}
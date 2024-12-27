using Microsoft.EntityFrameworkCore;
using WebApplicationOSB.Models;
using YourNamespace;

public class AppDbContext : DbContext
{
    public DbSet<TblKurum> TblKurum { get; set; }
    public DbSet<TBLORGANIZESAYACOKUMAELEKTRIK> TBLORGANIZESAYACOKUMAELEKTRIK { get; set; }
    public DbSet<TBLORGANIZESAYACOKUMADOGALGAZ> TBLORGANIZESAYACOKUMADOGALGAZ { get; set; }
    public DbSet<TBLORGANIZESAYACOKUMASU> TBLORGANIZESAYACOKUMASU { get; set; }
    public  DbSet<TBLORGANIZEODEMELER> TBLORGANIZEODEMELER { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}








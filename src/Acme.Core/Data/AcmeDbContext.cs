using Acme.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Acme.Core.Data;

public class AcmeDbContext : DbContext
{
    public DbSet<Submission>? Submissions { get; set; }
    public DbSet<SerialNumber>? SerialNumbers { get; set; }

    public AcmeDbContext(DbContextOptions<AcmeDbContext> options) : base(options){}
}

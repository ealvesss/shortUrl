using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities;

namespace URLShortener.Repo.Persistence;

public class UrlDbContext(DbContextOptions<UrlDbContext> options) : DbContext(options), IUrlDbContext
{
    public DbSet<ShortenedUrl> Urls { get; set; }
   
}
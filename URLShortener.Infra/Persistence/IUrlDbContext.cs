using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities;

namespace URLShortener.Repo.Persistence;

public interface IUrlDbContext
{
    DbSet<ShortenedUrl> Urls {get; set;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
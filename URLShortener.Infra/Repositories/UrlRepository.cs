using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities;
using URLShortener.Repo.Persistence;

namespace URLShortener.Repo.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly IUrlDbContext _context;

    public UrlRepository(IUrlDbContext context)
    {
        _context = context;
    }

    public async Task AddUrlAsync(ShortenedUrl url)
    {
        await _context.Urls.AddAsync(url);
        await _context.SaveChangesAsync(CancellationToken.None);
    }

    public async Task<ShortenedUrl?> GetUrlByShortCodeAsync(string shortUrl)
    {
        return await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
    }
}
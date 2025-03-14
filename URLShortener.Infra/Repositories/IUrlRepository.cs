using URLShortener.Domain.Entities;

namespace URLShortener.Repo.Repositories;

public interface IUrlRepository
{
    Task AddUrlAsync(ShortenedUrl url);
    Task<ShortenedUrl?> GetUrlByShortCodeAsync(string shortUrl);
}
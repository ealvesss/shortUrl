namespace URLShortener.Domain.Entities;

public class ShortenedUrl
{
    public int Id { get; init; }
    public required string OriginalUrl { get; init; }
    public required string ShortUrl { get; init; }
}
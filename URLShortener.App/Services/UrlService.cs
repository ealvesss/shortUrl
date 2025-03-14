using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using URLShortener.App.Interfaces;
using URLShortener.Domain.Entities;
using URLShortener.Repo.Repositories;

namespace URLShortener.App.Services;

public class UrlService(IUrlRepository _repository, IAmazonSQS _sqlClient, IConfiguration _configuration)
    : IUrlService
{
    public async Task<string> ShortenUrlAsync(string originalUrl)
    {
        var shorUrl = Guid.NewGuid().ToString()[..6];
        var url = new ShortenedUrl { OriginalUrl = originalUrl, ShortUrl = shorUrl };

        try
        {
            await _repository.AddUrlAsync(url);
        }
        catch (Exception)
        {
            var queueUrl = _configuration["AWS:SQSQueueUrl"];
            await _sqlClient.SendMessageAsync(new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = JsonSerializer.Serialize(url)
            });
        }
        return $"https://short.ly/{shorUrl}";
    }

    public async Task<string> GetOriginalUrlAsync(string shortUrl)
    {
        var url = await _repository.GetUrlByShortCodeAsync(shortUrl);
        return url?.OriginalUrl;
    }
}
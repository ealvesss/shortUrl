using Microsoft.AspNetCore.Mvc;
using URLShortener.App.Interfaces;
using URLShortener.Domain.DTOs;

namespace URLShortener.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController(IUrlService service) : Controller
{
    // Post
    [HttpPost("/shorten")]
    public async Task<IResult> Shorten([FromBody] UrlRequest request, IUrlService urlService)
    {
        var result = await service.ShortenUrlAsync(request.OriginalUrl);
        return Results.Ok(new { ShortenedUrl = result });
    }
    
    //Get
    [HttpGet("/{shortUrl}")]
    public async Task<IResult> Shorted(string shortUrl, IUrlService urlService)
    {
        var url = await service.GetOriginalUrlAsync(shortUrl);
        return Results.Redirect(url);
    }
}
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Quickfire_Bulletin.Models
{

public class NewsArticle
{
    [Key]  
    [JsonProperty("article_id")]
    public required string ArticleId { get; set; }

    [JsonProperty("title")]
    public required string Title { get; set; }

    [JsonProperty("link")]
    public string? Link { get; set; }

    [JsonProperty("source_id")]
    public string? SourceId { get; set; }

    [JsonProperty("source_priority")]
    public int SourcePriority { get; set; }

    [JsonProperty("image_url")]
    public string? ImageUrl { get; set; }

    [JsonProperty("video_url")]
    public string? VideoUrl { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("pubDate")]
    public DateTime PubDate { get; set; }

    [JsonProperty("content")]
    public required string Content { get; set; }


    public ICollection<Comment>? Comments { get; set; }
}
}
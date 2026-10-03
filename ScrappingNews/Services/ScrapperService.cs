using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using ScrappingNews.Models;

namespace ScrappingNews.Services;

    public class ScraperService
    {
        private readonly HttpClient _httpClient;

        public ScraperService()
        {
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0"
            );
        }

        public async Task<List<News>> ScrapeAsync(string url)
        {
            var html = await _httpClient.GetStringAsync(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var news = new List<News>();

            var nodes = doc.DocumentNode.SelectNodes("//a[@dtr-id]");

            if (nodes == null)
                return news;

            foreach (var node in nodes)
            {
                var articleId = node.GetAttributeValue("dtr-id", "");

                var title = node.GetAttributeValue("dtr-ttl", "");

                var articleUrl = node.GetAttributeValue("href", "");

                var category = node
                    .SelectSingleNode(".//span[contains(@class,'text-cnn_red')]")
                    ?.InnerText
                    .Trim() ?? "";

                var image = node.SelectSingleNode(".//img");

                var imageUrl = image?.GetAttributeValue("src", "") ?? "";

                var imageAlt = image?.GetAttributeValue("alt", "") ?? "";

                if (!long.TryParse(articleId, out var id))
                    continue;

                news.Add(new News
                {
                    Title = title,
                    Url = articleUrl,
                    ImageUrl = imageUrl,
                    ImageAlt = imageAlt
                });
            }

            return news;
        }
    }


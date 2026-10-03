using ScrappingNews.Services;

var url = "https://www.cnnindonesia.com/hiburan";

var scraper = new ScraperService();
var database = new DatabaseService();

Console.WriteLine("Scraping CNN Indonesia...");

var news = await scraper.ScrapeAsync(url);

Console.WriteLine($"Ditemukan {news.Count} artikel.");
Console.WriteLine();

foreach (var article in news)
{
    Console.WriteLine(article.Title);
    Console.WriteLine(article.Url);

    try
    {
        await database.InsertAsync(article);

        Console.WriteLine("Berhasil disimpan.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Gagal menyimpan: {ex.Message}");
    }

    Console.WriteLine();
}

Console.WriteLine("Selesai.");
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace ChessStatistics.BusinessLogic.ParseInformationFromLichess
{
    public class BlogPost
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string Intro { get; set; }
    
    public static async Task<string> FetchPageAsync(string url)
    {
        var httpClient = new HttpClient();
        return await httpClient.GetStringAsync(url);
    }

     public static List<BlogPost> ParseBlogPosts(string html)
     {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var postNodes = htmlDocument.DocumentNode.SelectNodes("//a[contains(@class, 'ublog-post-card') and contains(@class, 'ublog-post-card--link')]");

            if (postNodes == null)
            {
                return new List<BlogPost>();
            }

            var postList = new List<BlogPost>();

            foreach (var node in postNodes)
            {
                var post = new BlogPost
                {
                    Title = node.SelectSingleNode(".//h2[@class='ublog-post-card__title']").InnerText.Trim(),
                    Url = "https://lichess.org" + node.Attributes["href"].Value,
                    ImageUrl = node.SelectSingleNode(".//img[@class='ublog-post-image ublog-post-card__image']").Attributes["src"].Value,
                    PublishDate = DateTime.Parse(node.SelectSingleNode(".//time[@class='ublog-post-card__over-image']").Attributes["datetime"].Value),
                    Intro = node.SelectSingleNode(".//span[@class='ublog-post-card__intro']").InnerText.Trim()
                };

                postList.Add(post);
            }

            return postList;
        }

        public static async Task DisplayBlogPostsAsync()
        {
            var html = await FetchPageAsync("https://lichess.org/@/Lichess/blog");
            var postList = ParseBlogPosts(html);

            foreach (var post in postList)
            {
                Console.WriteLine($"{post.Title}\n{post.Url}\n");
            }
        }
    }
}

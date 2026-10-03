using System;
using System.Collections.Generic;
using System.Text;

namespace ScrappingNews.Models
{
    public class News
    {
        public string Title { get; set; } = "";
        public string Url { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string ImageAlt { get; set; } = "";
    }
}

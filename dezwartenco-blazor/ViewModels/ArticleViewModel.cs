using Domain.Entities;

using Microsoft.OData.Edm;

using System;

namespace DeZwartEnCoBlazor.ViewModels
{
    public class ArticleViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; } = DateTime.MinValue;

        public ArticleViewModel()
        {
        }

        public ArticleViewModel(Article article)
        {
            this.Body = article.Body;
            this.DatePosted = article.DatePosted;
        }
    }
}

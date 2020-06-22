using Microsoft.Azure.Cosmos.Table;
using System;

namespace Domain.Entities
{
    public class ArticleEntity:TableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; } = DateTime.MinValue;

        public Article ToEntity()
        {
            return new Article(this.RowKey, this.Title, this.Body, this.DatePosted);
        }
    }
}
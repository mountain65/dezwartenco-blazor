using Domain.Entities;

using Microsoft.Azure.Cosmos.Table;
using System;

namespace ApplicationServices.TableStorage
{
    public class ArticleEntity:TableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; } = DateTime.MinValue;

        public Article ToEntity()
        {
            return new Article(this.RowKey, this.Body, this.DatePosted);
        }
    }
}
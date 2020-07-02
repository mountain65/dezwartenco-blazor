using System;

namespace Domain.Entities
{
    public class Article
    {
        public Article(string slug, string body, DateTime datePosted)
        {
            Slug = slug;
            Body = body;
            DatePosted = datePosted;
        }

        public string Slug { get; }
        public string Body { get; }
        public DateTime DatePosted { get; set; }
    }
}
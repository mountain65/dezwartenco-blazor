using System;

namespace Domain.Entities
{
    public class Article
    {
        public Article(string slug, string title, string body, DateTime datePosted)
        {
            Slug = slug;
            Title = title;
            Body = body;
            DatePosted = datePosted;
        }

        public string Slug { get; }
        public string Title { get; }
        public string Body { get; }
        public DateTime DatePosted { get; set; }
    }
}
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using Domain;
using Domain.Entities;
using DomainServices;

using Microsoft.Azure.Documents.SystemFunctions;

using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.BlobStorage
{
    public class ArticleRepository : IArticles
    {
        private BlobServiceClient blobServiceClient;

        public ArticleRepository(string connectionString)
        {
            blobServiceClient = new BlobServiceClient(connectionString);
        }
        public async Task<IMaybe<Article>> Get(string slug)
        {
            try
            {
                var containerClient = blobServiceClient.GetBlobContainerClient("articles");
                var blobClient = containerClient.GetBlobClient($"{slug}.md");
                BlobDownloadInfo blob = await blobClient.DownloadAsync();
                if (blob.ContentLength == 0)
                   return new Nothing<Article>();

                using var ms = new MemoryStream();
                await blob.Content.CopyToAsync(ms);

                var markdown = Encoding.UTF8.GetString(ms.GetBuffer());
                return new Just<Article>(new Article(slug, markdown, blob.Details.LastModified.UtcDateTime));

            }
            catch (RequestFailedException e)
            {
                throw new System.Exception($"Getting article with slug '{slug}'", e);
            }
        }
    }
}
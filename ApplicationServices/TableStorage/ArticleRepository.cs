using Domain;
using Domain.Entities;
using DomainServices;
using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;

namespace ApplicationServices.TableStorage
{
    public class ArticleRepository : IArticles
    {
        private CloudTable table;

        public ArticleRepository(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            this.table = tableClient.GetTableReference("articles");
            this.table.CreateIfNotExists();
        }
        public async Task<IMaybe<Article>> Get(string slug)
        {
            try
            {
                var retrieveOperation = TableOperation.Retrieve<ArticleEntity>("europe", slug);
                var result = await table.ExecuteAsync(retrieveOperation);
                var stored = result.Result as ArticleEntity;
                if (stored != null)
                {
                    return new Just<Article>(stored.ToEntity());
                }

                return new Nothing<Article>();
            }
            catch (StorageException e)
            {
                throw new System.Exception($"Getting article with slug '{slug}'", e);
            }
        }
    }
}
using Domain;
using Domain.Entities;

using System.Threading.Tasks;

namespace DomainServices
{
    public interface IArticles
    {
        Task<IMaybe<Article>> Get(string slug);
    }
}

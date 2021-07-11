using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using LibraryGraphqlApi.Data;
using LibraryGraphqlApi.GraphQL.DataLoader;
using LibraryGraphqlApi.GraphQL.Types;
using LibraryGraphqlApi.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryGraphqlApi.GraphQL.Authors
{
    [ExtendObjectType("Query")]
    public class AuthorsQueries
    {
        [UseApplicationDbContext]
        [UsePaging(typeof(NonNullType<AuthorType>))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Author> GetAuthors([ScopedService] AppDbContext context) => context.Authors;

        public Task<Author> GetAuthorAsync([ID(nameof(Author))] int id, AuthorByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    }
}

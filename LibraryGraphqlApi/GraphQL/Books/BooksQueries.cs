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

namespace LibraryGraphqlApi.GraphQL.Books
{
    [ExtendObjectType("Query")]
    public class BooksQueries
    {
        [UseApplicationDbContext]
        [UsePaging(typeof(NonNullType<BookType>))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Book> GetBooks([ScopedService] AppDbContext context) => context.Books;

        public Task<Book> GetBookAsync([ID(nameof(Book))] int id, BookByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    }
}

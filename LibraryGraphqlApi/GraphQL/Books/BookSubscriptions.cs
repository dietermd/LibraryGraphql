using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using LibraryGraphqlApi.GraphQL.DataLoader;
using LibraryGraphqlApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryGraphqlApi.GraphQL.Books
{
    [ExtendObjectType("Subscription")]
    public class BookSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Book> OnBookAddedAsync(
            [EventMessage] int bookId,
            BookByIdDataLoader bookById,
            CancellationToken cancellationToken) => bookById.LoadAsync(bookId, cancellationToken);

        [Subscribe(With = nameof(SubscribeToOnNewBookByAuthorAsync))]
        public Task<Book> OnNewBookByAuthorAsync(
            [ID(nameof(Author))] int authorId,
            [EventMessage] int bookId,
            BookByIdDataLoader bookById,
            CancellationToken cancellationToken) => bookById.LoadAsync(bookId, cancellationToken);

        public async ValueTask<ISourceStream<int>> SubscribeToOnNewBookByAuthorAsync(
            int authorId,
            [Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken) =>
            await eventReceiver.SubscribeAsync<string, int>(
                "OnNewBookByAuthor_" + authorId, cancellationToken);
    }
}

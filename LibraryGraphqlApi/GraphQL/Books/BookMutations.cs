using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using LibraryGraphqlApi.Data;
using LibraryGraphqlApi.GraphQL.Common;
using LibraryGraphqlApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryGraphqlApi.GraphQL.Books
{
    [ExtendObjectType("Mutation")]
    public class BookMutations
    {
        [UseApplicationDbContext]
        public async Task<AddBookPayload> AddBookAsync(
            AddBookInput input,
            [ScopedService] AppDbContext context,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {

            var userErrors = new List<UserError>();

            if (string.IsNullOrEmpty(input.Title))
            {
                userErrors.Add(
                    new UserError("The title cannot be empty.", "TITLE_EMPTY"));
            }
            if (string.IsNullOrEmpty(input.Publisher))
            {
                userErrors.Add(
                    new UserError("The publisher cannot be empty.", "PUBLISHER_EMPTY"));
            }
            if (!input.AuthorIds.Any())
            {
                userErrors.Add(
                    new UserError("The book must have at least one author.", "NO_AUTHOR"));
            }

            if (userErrors.Any())
                return new AddBookPayload(
                    userErrors);

            var authors = await context.Authors.Where(a => input.AuthorIds.Contains(a.Id)).ToListAsync(cancellationToken);

            var book = new Book
            {
                Title = input.Title,
                Publisher = input.Publisher,
                Authors = authors
            };

            context.Books.Add(book);
            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(BookSubscriptions.OnBookAddedAsync), book.Id, cancellationToken);
            foreach (var authorId in input.AuthorIds)
            {
                await eventSender.SendAsync("OnNewBookByAuthor_" + authorId, book.Id, cancellationToken);
            }

            return new AddBookPayload(book);
        }
    }
}

using HotChocolate;
using HotChocolate.Types;
using LibraryGraphqlApi.Data;
using LibraryGraphqlApi.GraphQL.Common;
using LibraryGraphqlApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryGraphqlApi.GraphQL.Authors
{
    [ExtendObjectType("Mutation")]
    public class AuthorMutations
    {
        [UseApplicationDbContext]
        public async Task<AddAuthorPayload> AddAuthorAsync(AddAuthorInput input, [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.FullName))
            {
                return new AddAuthorPayload(
                    new UserError[] { new UserError("The author's full name cannot be empty.", "FULLNAME_EMPTY") });
            }

            var author = new Author
            {
                FullName = input.FullName,
                DOB = input.DOB
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync(cancellationToken);
            return new AddAuthorPayload(author);
        }
    }
}

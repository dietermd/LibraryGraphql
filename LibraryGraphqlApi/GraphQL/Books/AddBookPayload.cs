using LibraryGraphqlApi.GraphQL.Common;
using LibraryGraphqlApi.Models;
using System.Collections.Generic;

namespace LibraryGraphqlApi.GraphQL.Books
{
    public class AddBookPayload : BookPayloadBase
    {
        public AddBookPayload(Book book)
            : base(book)
        {
        }
        public AddBookPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}

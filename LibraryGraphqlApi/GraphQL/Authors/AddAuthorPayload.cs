using LibraryGraphqlApi.GraphQL.Common;
using LibraryGraphqlApi.Models;
using System.Collections.Generic;

namespace LibraryGraphqlApi.GraphQL.Authors
{
    public class AddAuthorPayload : AuthorPayloadBase
    {
        public AddAuthorPayload(Author author)
            : base(author)
        {
        }
        public AddAuthorPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}

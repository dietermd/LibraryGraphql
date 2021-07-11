using LibraryGraphqlApi.GraphQL.Common;
using LibraryGraphqlApi.Models;
using System.Collections.Generic;

namespace LibraryGraphqlApi.GraphQL.Books
{
    public class BookPayloadBase : Payload
    {
        protected BookPayloadBase(Book book)
        {
            Book = book;
        }

        protected BookPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Book Book { get; }
    }
}

using LibraryGraphqlApi.GraphQL.Common;
using LibraryGraphqlApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGraphqlApi.GraphQL.Authors
{
    public class AuthorPayloadBase : Payload
    {
        protected AuthorPayloadBase(Author author)
        {
            Author = author;
        }

        protected AuthorPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Author Author { get; set; }
    }
}

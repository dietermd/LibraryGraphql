using HotChocolate.Types.Relay;
using LibraryGraphqlApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryGraphqlApi.GraphQL.Books
{
    public record AddBookInput(
        [Required]
        string Title,

        [Required]
        string Publisher,

        [Required]
        [ID(nameof(Author))]
        IReadOnlyList<int> AuthorIds);
}

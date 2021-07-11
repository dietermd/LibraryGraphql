using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryGraphqlApi.GraphQL.Authors
{
    public record AddAuthorInput(
        [Required]
        string FullName,

        DateTime DOB);
}

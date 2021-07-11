﻿using System.Collections.Generic;

namespace LibraryGraphqlApi.GraphQL.Common
{
    public abstract class Payload
    {
        protected Payload(IReadOnlyList<UserError> errors = null)
        {
            Errors = errors;
        }

        public IReadOnlyList<UserError> Errors { get; }
    }
}

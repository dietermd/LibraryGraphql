using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using LibraryGraphqlApi.Data;
using System.Reflection;

namespace LibraryGraphqlApi.GraphQL
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<AppDbContext>();
        }
    }
}

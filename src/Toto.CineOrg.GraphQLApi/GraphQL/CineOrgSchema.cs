using System;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Toto.CineOrg.GraphQLApi.GraphQL
{
    public class CineOrgSchema : Schema
    {
        public CineOrgSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<CineOrgQuery>();
            Mutation = provider.GetRequiredService<CineOrgMutation>();
        }
    }
}
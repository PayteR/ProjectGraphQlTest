using GraphQL.EntityFramework;
using ProjectGraphQlTest.Infrastructure.Data;

namespace ProjectGraphQlTest.API.Schema
{
    public class Query : QueryGraphType<AppDbContext>
    {
        public Query(IEfGraphQLService<AppDbContext> graphQlService
            ) : base(graphQlService)
        {
            Name = "Root";            
        }
    }
}

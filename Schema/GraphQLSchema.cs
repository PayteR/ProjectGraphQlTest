using GraphQL;

namespace ProjectGraphQlTest.API.Schema
{
    public class GraphQLSchema : GraphQL.Types.Schema
    {
        public GraphQLSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<Query>();
            DependencyResolver = resolver;
        }
    }
}

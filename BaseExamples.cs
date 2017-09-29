using System;
using GraphQL;
using GraphQL.Types;

namespace GraphQLReview
{
    public static class BaseExamples
    {
        public static string HelloWorld()
        {
            var schema = Schema.For(@"
                type Query {
                    hello: String
                }
            ");

            var root = new { Hello = "Hello World!" };

            var result = schema.Execute(_ =>
            {
                _.Query = "{ hello }";
                _.Root = root;
            });

            return result;
        }
     }
}
using System;
using GraphQL;
using GraphQL.Types;

namespace GraphQLReview
{
    public static class BaseExample
    {
        public static void HelloWorld()
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

            Console.WriteLine(result);
        }
     }
}
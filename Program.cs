using System;
using GraphQL;
using GraphQL.StarWars;
using GraphQL.StarWars.IoC;
using GraphQL.StarWars.Types;

namespace GraphQLReview
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(BaseExamples.HelloWorld());

            var container = BuildContainer();
            var schema = container.Get<StarWarsSchema>();

            var result = schema.Execute(_ =>
            {
            _.Query = @"
                {
                    human(id: ""1"") {
                        __typename
                        name
                        homePlanet
                        appearsIn
                        friends {
                            name
                        }
                    }
                }
            ";
            });

            Console.WriteLine(result);
        }

        static ISimpleContainer BuildContainer()
        {
            var container = new SimpleContainer();

            container.Singleton(new StarWarsData());
            container.Register<StarWarsQuery>();
            container.Register<StarWarsMutation>();
            container.Register<HumanType>();
            container.Register<HumanInputType>();
            container.Register<DroidType>();
            container.Register<CharacterInterface>();
            container.Singleton(
                new StarWarsSchema(
                    new FuncDependencyResolver(type => container.Get(type))
                )
            );

            return container;
        }
    }
}

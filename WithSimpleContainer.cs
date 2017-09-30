using System;
using GraphQL;
using GraphQL.StarWars;
using GraphQL.StarWars.IoC;
using GraphQL.StarWars.Types;

namespace GraphQLReview
{
    public static class WithSimpleContainer
    {
        public static void Example() {
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
                    new FuncDependencyResolver(container.Get)
                )
            );

            var schema = container.Get<StarWarsSchema>();

            var result = schema.Execute(_ => {
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
    }
}

using System;
using Microsoft.Extensions.DependencyInjection;
using GraphQL;
using GraphQL.StarWars;
using GraphQL.StarWars.Types;

namespace GraphQLReview
{
    public class Resolver : IDependencyResolver
    {
        private readonly IServiceProvider _provider;

        public Resolver(IServiceProvider provider)
        {
            _provider = provider;
        }

        public T Resolve<T>()
        {
           return (T) Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            return _provider.GetService(type);
        }
    }

    public static class WithSeviceCollection
    {
        public static void Example() {
            var services = new ServiceCollection();

            services.AddSingleton(new StarWarsData());
            services.AddTransient<StarWarsQuery>();
            services.AddTransient<StarWarsMutation>();
            services.AddTransient<HumanType>();
            services.AddTransient<HumanInputType>();
            services.AddTransient<DroidType>();
            services.AddTransient<CharacterInterface>();
            services.AddTransient<EpisodeEnum>();
            var provider = services.BuildServiceProvider();

            var schema = new StarWarsSchema(
                new Resolver(provider)
            );
 
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

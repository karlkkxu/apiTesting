using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeAPI
{
    class APICaller
    {
        private static readonly string baseUrl = "http://pokeapi.co/api/v2/pokemon/";

        public async Task<PokemonUnit> GetPokemon(string name)
        {
            Console.WriteLine("Fetching pokemon by name : " + name);
            try
            {
                using HttpClient client = new HttpClient();
                using HttpResponseMessage resp = await client.GetAsync(baseUrl + name);
                using HttpContent content = resp.Content;

                var data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    //Picking apart the json string data into JToken values
                    try
                    {
                        JToken token = JObject.Parse(data);
                        PokemonUnit poke = new PokemonUnit((string)token.SelectToken("name"));
                        poke.AddStats(token);
                        poke.AddTypes(token);
                        poke.Print();
                        return poke;
                    } catch
                    {
                        Console.WriteLine("Fuckup at token differentiation");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Fuckup at data - null");
                    return null;
                }
            }
            catch
            {
                Console.WriteLine("Fuckup at api call");
                return null;
            }
        }
        static void Main(string[] args)
        {
            APICaller call = new APICaller();
            call.GetPokemon("tyranitar").Wait();
        }
    }
}

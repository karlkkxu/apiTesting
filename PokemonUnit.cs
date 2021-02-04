using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeAPI
{
    public class PokemonUnit
    { 
        private string Name { get; set; }
        private int[,] Stats { get; set; }
        private List<string> Types { get; set; }
        //private List<Move> Moves { get; }   
        public PokemonUnit(string name)
        {
            this.Name = name;
        }

        public void Print()
        {
            Console.WriteLine("Debugging PokemonUnit");
            Console.WriteLine("Name: " + this.Name);
            Console.Write("Stats: ");
            foreach (var pair in this.Stats)
            {
                Console.Write(pair + ", ");
            }
            Console.Write("\nTypes: ");
            foreach (var type in this.Types) { Console.Write(type + ", "); }
        }

        public void AddStats(JToken jtokens)
        {
            this.Stats = new int[5,2];
            for (int i = 0; i < this.Stats.GetLength(0); i++)
            {
                this.Stats[i, 0] = (int)jtokens.SelectToken("stats["+i+"].base_stat");
                this.Stats[i, 1] = (int)jtokens.SelectToken("stats["+i+"].effort");
            }
        }

        public void AddTypes(JToken jtokens)
        {
            this.Types = new List<string>();
            foreach (var type in jtokens.SelectToken("types"))
            {
                this.Types.Add((string)type.SelectToken("type.name"));
            }
        }
    }
}

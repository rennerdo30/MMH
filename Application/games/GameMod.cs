using System;

namespace Application.games
{
    public class GameMod
    {

        public string Name { get; set; }
        public string URL { get; set; }
        public string raw { get; set; }

        public GameMod(string name, string uRL, string raw)
        {
            Name = name;
            URL = uRL;
            this.raw = raw;
        }

        public override bool Equals(object? obj)
        {
            return obj is GameMod mod &&
                   Name == mod.Name &&
                   URL == mod.URL;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, URL);
        }
    }
}
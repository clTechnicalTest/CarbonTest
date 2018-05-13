namespace TreasureMap.Maps
{
    public abstract class MapEntry
    {
        public MapEntry(string type, string[] args)
        {
            Type = type;
            Args = args;
        }

        public string Type { get; }
        public string[] Args { get; }
    }
}
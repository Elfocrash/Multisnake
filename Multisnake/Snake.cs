using Newtonsoft.Json;

namespace Multisnake
{
    public class Snake
    {
        public string id { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public int tail { get; set; }

        public object[] trail { get; set; }
    }
}
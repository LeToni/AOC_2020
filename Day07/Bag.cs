using System.Collections.Generic;

namespace Day7
{
    public class Bag
    {
        public string Name { get; set; }
        public List<(string ColorBag, int Amount)> Content { get; set; } = new List<(string ColorBag, int Amount)>();

    }
}

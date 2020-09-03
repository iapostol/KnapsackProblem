using System.Collections.Generic;

namespace KnapsackProblem
{
    public class Item
    {
        public string Name { get; set; }
        public int Weight { set; get; }
        public int Value { set; get; }
        public bool IsPacked { get; set; }
    }
}
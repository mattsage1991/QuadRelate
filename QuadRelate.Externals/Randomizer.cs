using System;
using System.Collections.Generic;
using QuadRelate.Contracts;

namespace QuadRelate.Externals
{
    public class Randomizer : IRandomizer
    {
        private readonly Random _random;

        public Randomizer()
        {
            _random = new Random();
        }

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public int GetRandomItem(IList<int> items)
        {
            return items[Next(items.Count)];
        }
    }
}
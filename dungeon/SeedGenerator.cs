using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dungeon
{
    public static class SeedGenerator
    {
        public static int Seed { get; set; }

        public static int Random(int max)
        {
            Seed = (Seed * 9301 + 49297) % 1337;
            return Seed % max;
        }
    }
}

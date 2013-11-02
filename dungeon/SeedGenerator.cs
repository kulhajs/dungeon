using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dungeon
{
    public static class SeedGenerator
    {
        public static int Seed { get; set; }

        static Random random = new Random();

        public static int Random(int min=0,int max=1)
        {
            Seed = (Seed * 9301 + 49297) % 233280;
            int rnd = Seed / 233280;

            //return min + rnd * (max - min);

            return random.Next(0, 2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static ÉÁj.CONSTS;

namespace ÉÁj
{
    public class AI
    {
        public static List<AI> EAjok = new();
        public static List<int> Foglalt = new();

        private HashSet<Point> lephet = new();

        public Point Coord { get; private set; }
        public int X => Coord.X;
        public int Y => Coord.Y;

        public AI(int x, int y, int id)
        {
            Coord = new(x, y);
            Foglalt.Add(id);

            EAjok.Add(this);
        }

        private HashSet<Point> Lephet()
        {
            HashSet<Point> result = new();

            if(X > 0)
                result.Add(new (X-1, Y));
            if(Y > 0)
                result.Add(new (X, Y-1));
            if(X < OSZLOPOKSZAMA)
                result.Add(new (X+1, Y));
            if(Y < SOROKSZAMA)
                result.Add(new (X, Y+1));

            //Where(G => G == nincs_babu)

            return result;
        }

        public Point Lepes()
        {
            var lephet = Lephet();

            return Rand(lephet);
        }

        private Point Rand(ISet<Point> collection) => collection.ElementAt(RAND.Next(collection.Count));
    }
}
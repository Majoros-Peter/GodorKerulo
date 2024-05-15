using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using static ÉÁj.CONSTS;

namespace ÉÁj
{
    public class AI
    {
        private static Point Vege = new(-1, -1);
        public static List<AI> EAjok = new();
        public readonly static HashSet<Point> Jukak = new();

        //private HashSet<Point> lephet = new();

        public Point Coord { get; set; }
        public int X => Coord.X;
    
        public int Y => Coord.Y;

        public AI(int num)
        {
            Coord = IntToPoint(num);

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

            return result.Where(coord => !EAjok.Any(G => G.Coord == coord)).ToHashSet();
        }

        public Point Lepes()
        {
            var lephet = Lephet();

            if (lephet.Count == 0)
                return Vege;

            Point lepes = Rand(lephet);

            if (Jukak.Contains(lepes))
                return Vege;

            return lepes;
        }

        private static Point Rand(ISet<Point> collection) => collection.ElementAt(RAND.Next(collection.Count));
        public static int PointToInt(Point p) => p.X + p.Y * OSZLOPOKSZAMA;
        public static Point IntToPoint(int n) => new(n % OSZLOPOKSZAMA, n / OSZLOPOKSZAMA);
    }
}
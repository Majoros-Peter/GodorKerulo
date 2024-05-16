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
        #region Property/Attribute
        private static Point Vege = new(-1, -1);
		public static List<AI> EAjok = new();
		public readonly static HashSet<Point> Jukak = new();

		public HashSet<Point> lephet = new();
		public HashSet<Point> nemLephet = new();

		public Point Coord { get; set; }
		public int X => Coord.X;
		public int Y => Coord.Y;
        #endregion

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
			if(X < OSZLOPOKSZAMA-1)
				result.Add(new (X+1, Y));
			if(Y < SOROKSZAMA-1)
				result.Add(new (X, Y+1));

			return result.Where(coord => !EAjok.Any(G => G.Coord == coord) && !nemLephet.Contains(coord)).ToHashSet();
		}
		public Point Lepes()
		{
			Point lepes = Rand(Lephet(), lephet);

			lephet.Add(lepes);

			return !Jukak.Contains(lepes) ? lepes : Vege;
		}

        private static Point Rand(ISet<Point> collection, ISet<Point> collection2)
		{
			if(collection.Count == 0)
                return collection2.Count != 0 ? collection2.ElementAt(RAND.Next(collection2.Count)) : Vege;

			if(collection2.Count == 0)
                return collection.ElementAt(RAND.Next(collection.Count));


            int index = RAND.Next(collection.Count + collection2.Count);

            return index >= collection.Count ? collection2.ElementAt(index - collection.Count) : collection.ElementAt(index);
        }
        public static int PointToInt(Point p) => p.X + p.Y * OSZLOPOKSZAMA;
		public static Point IntToPoint(int n) => new(n % OSZLOPOKSZAMA, n / OSZLOPOKSZAMA);
	}
}
namespace Reversi.Model
{
	public class Vector
	{

		public static readonly Vector[] Directions =
		{
			new Vector(1, 0),
			new Vector(0, 1),
			new Vector(-1, 0),
			new Vector(0, -1),
			new Vector(1, 1),
			new Vector(-1, -1),
			new Vector(-1, 1),
			new Vector(1, -1)
		};

		public int X { get; set; }
		public int Y { get; set; }

		public Vector()
		{
		}

		public Vector(int x, int y)
		{
			X = x;
			Y = y;
		}

		public void Add(Vector vector)
		{
			X += vector.X;
			Y += vector.Y;
		}

		public Vector Clone()
		{
			return new Vector(X, Y);
		}

		public bool Equals(Vector other)
		{
			return X == other.X && Y == other.Y;
		}

		public override string ToString()
		{
			return $"Vector{{X={X},Y={Y}}}";
		}

		public static Vector Parse(string raw)
		{
			var split = raw.Replace(" ", "").Split(',');
			if (!int.TryParse(split[0], out var x)) return null;
			return int.TryParse(split[1], out var y) ? new Vector(x, y) : null;
		}
	}
}

// ReSharper disable InconsistentNaming
namespace Walker.Data.Geometry.Speed.Space {
	using System;

	public struct FaceF {

		public readonly int a, b, c;
		public readonly SolidF solid;

		public Vector3F A => solid[a];
		public Vector3F B => solid[b];
		public Vector3F C => solid[c];

		public EdgeF AB => new EdgeF(solid, a, b);
		public EdgeF BC => new EdgeF(solid, b, c);
		public EdgeF CA => new EdgeF(solid, c, a);

		public Vector3F Normal => A.Cross(B);

		public FaceF(SolidF s, int a, int b, int c) {
			this.solid = s;
			this.a = a;
			this.b = b;
			this.c = c;
			int count = solid.Vertices.Length;
			if (a < 0 || b < 0 || c < 0 || a >= count || b >= count || c >= count) { throw new IndexOutOfRangeException(); }
		}

	}
}
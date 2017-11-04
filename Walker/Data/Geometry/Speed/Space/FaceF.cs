// ReSharper disable InconsistentNaming
namespace Walker.Data.Geometry.Speed.Space {
	using System;

	public struct FaceF {

		public readonly int a, b, c;
		public readonly PolyhedronF solid;

		public Vector3F A => solid[a];
		public Vector3F B => solid[b];
		public Vector3F C => solid[c];

		public EdgeF AB => new EdgeF(solid, a, b);
		public EdgeF BC => new EdgeF(solid, b, c);
		public EdgeF CA => new EdgeF(solid, c, a);

		public Vector3F Normal => A.Cross(B);

		public EdgeF[] Edges => new[] {AB, BC, CA};

		public FaceF(PolyhedronF s, int a, int b, int c) {
			this.solid = s;
			this.a = a;
			this.b = b;
			this.c = c;
			int count = solid.Vertices.Length;
			if (a < 0 || b < 0 || c < 0 || a >= count || b >= count || c >= count) { throw new IndexOutOfRangeException(); }
		}

		public override bool Equals(object obj) {
			return obj is FaceF f && A == f.A && B == f.B && C == f.C;
		}

		public override int GetHashCode() {
			unchecked {
				int hashCode = A.GetHashCode();
				hashCode = (hashCode * 397) ^ B.GetHashCode();
				hashCode = (hashCode * 397) ^ C.GetHashCode();
				return hashCode;
			}
		}
	}
}
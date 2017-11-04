namespace Walker.Data.Geometry.Speed.Space {
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	public struct EdgeF {

		public readonly int a, b;
		public readonly PolyhedronF solid;

		public Vector3F A => solid[a];
		public Vector3F B => solid[b];
		public Vector3F Dir => B - A;

		public EdgeF(PolyhedronF s, int a, int b) {
			this.a = a;
			this.b = b;
			this.solid = s;
			int sCount = s.Vertices.Length;
			if (a < 0 || b < 0 || a >= sCount || b >= sCount) { throw new IndexOutOfRangeException(); }
		}

		public static explicit operator Line3F(EdgeF e) {
			return new Line3F(e.A, e.B);
		}

		public class IntersectionException : Exception {
			public IntersectionException(string msg) : base(msg) { }
		}

		/// <summary>
		/// Returns location of intersection with a plane.
		/// </summary>
		/// <param name="face">Plane</param>
		/// <param name="floatTol">Float tolerance, default = 0.0001f</param>
		/// <returns>Location of intersection of this and the face</returns>
		/// <exception cref="IntersectionException">Line doesn't intersect with plane</exception>
		public Vector3F Intersection(FaceF face, float floatTol = GeoMeta.Tolerance) {
			Vector3F n = face.Normal;
			if (Math.Abs(A.Dot(n)) < floatTol) { throw new IntersectionException("Does not intersect - Parallel"); }
			Vector3F w = A - face.A;
			float s = (-n).Dot(w) / n.Dot(Dir);
			if (s < 0 || s > 1) { throw new IntersectionException("Does not intersect - Too short"); }
			Vector3F point = s * Dir;
			if (   (face.B - face.A).Cross(point - face.A).Dot(n) < 0
			    || (face.C - face.B).Cross(point - face.B).Dot(n) < 0
			    || (face.B - face.C).Cross(point - face.C).Dot(n) < 0) {
				throw new IntersectionException("Does not intersect - Intersects plane but not triangle");
			}
			return point;
		}



		/// <summary>
		/// Gets all intersections with a solid.
		/// </summary>
		/// <param name="sol">The solid to test against</param>
		/// <returns>All intersections with the solid</returns>
		public List<Vector3F> Intersections(PolyhedronF sol) {
			List<Vector3F> res = new List<Vector3F>();
			foreach (FaceF face in sol.Faces) {
				try { res.Add(Intersection(face)); }
				catch(IntersectionException) { Debug.WriteLineIf(GeoMeta.GeoSwitch.Level >= TraceLevel.Verbose, "No intersection for " + this + " and " + face); }
			}
			return res;
		}

		/// <summary>
		/// Gets all intersections with a group of solids.
		/// </summary>
		/// <param name="sols">The solids to test against.</param>
		/// <returns>All intersections with the solids, paired with the corresponding solid.</returns>
		public List<Tuple<PolyhedronF, List<Vector3F>>> Intersections(IEnumerable<PolyhedronF> sols) {
			List<Tuple<PolyhedronF, List<Vector3F>>> res = new List<Tuple<PolyhedronF, List<Vector3F>>>();
			foreach (PolyhedronF sol in sols) {
				res.Add(new Tuple<PolyhedronF, List<Vector3F>>(sol, Intersections(sol)));
			}
			return res;
		}

		public override bool Equals(object obj) {
			switch (obj) {
				case EdgeF e:
					return A == e.A && B == e.B;
				case Line3F l:
					return A == l.o && B == l.End;
				default:
					return false;
			}
		}

		public override int GetHashCode() {
			unchecked {
				return (A.GetHashCode() * 397) ^ B.GetHashCode();
			}
		}

		public override string ToString() {
			return "{" + A + " -> " + B + "}";
		}

	}
}
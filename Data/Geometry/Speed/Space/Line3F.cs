namespace Walker.Data.Geometry.Speed.Space {
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	public struct Line3F {

		public Vector3F o, d;

		/// <summary>
		/// The end point of this line
		/// </summary>
		public Vector3F End {
			get => o + d;
			set => d = value - o;
		}

		/// <summary>
		/// Length of this line
		/// </summary>
		public float Length {
			get => d.Length;
			set => d.Length = value;
		}

		/// <summary>
		/// The direction vector of this line, with length = 1
		/// </summary>
		public Line3F Unit => new Line3F(o, d / d.Length);

		public Line3F(Vector3F o, Vector3F d) {
			this.o = o;
			this.d = d;
		}

		public float Dot(Line3F other) {
			return d.Dot(other.d);
		}

		public Vector3F Cross(Line3F other) {
			return d.Cross(other.d);
		}

		/// <summary>
		/// Gets the dot product of this line and another
		/// </summary>
		/// <param name="other">The other line</param>
		/// <returns>Dot product</returns>
		public float Dot(Vector3F other) {
			return d.Dot(other);
		}

		/// <summary>
		/// Returns new line perpendicular to both this line and the other line
		/// </summary>
		/// <param name="other">Other line</param>
		/// <returns>Cross product</returns>
		public Vector3F Cross(Vector3F other) {
			return d.Cross(other);
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
			if (Math.Abs(d.Dot(n)) < floatTol) { throw new IntersectionException("Does not intersect - Parallel"); }
			Vector3F w = o - face[0];
			float s = (-n).Dot(w) / n.Dot(d);
			if (s < 0 || s > 1) { throw new IntersectionException("Does not intersect - Too short"); }
			Vector3F point = s * d;
			if ((face[1] - face[0]).Cross(point - face[0]).Dot(n) < 0
			    || (face[2] - face[1]).Cross(point - face[1]).Dot(n) < 0
			    || (face[0] - face[2]).Cross(point - face[2]).Dot(n) < 0) {
				throw new IntersectionException("Does not intersect - Intersects plane but not triangle");
			}
			return point;
		}

		/// <summary>
		/// Gets all intersections with a solid.
		/// </summary>
		/// <param name="sol">The solid to test against</param>
		/// <returns>All intersections with the solid</returns>
		public List<Vector3F> Intersections(SolidF sol) {
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
		public List<Tuple<SolidF, List<Vector3F>>> Intersections(IEnumerable<SolidF> sols) {
			List<Tuple<SolidF, List<Vector3F>>> res = new List<Tuple<SolidF, List<Vector3F>>>();
			foreach (SolidF sol in sols) {
				res.Add(new Tuple<SolidF, List<Vector3F>>(sol, Intersections(sol)));
			}
			return res;
		}

		public override string ToString() {
			return o + " + " + d;
		}
	}
}
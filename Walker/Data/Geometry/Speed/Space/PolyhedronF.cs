namespace Walker.Data.Geometry.Speed.Space {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class PolyhedronF : SolidF {

		readonly Vector3F[] verts;

		readonly EdgeF[] edges;

		readonly FaceF[] faces;

		public Vector3F[] Vertices => verts;

		public EdgeF[] Edges => edges;

		public FaceF[] Faces => faces;

		public Vector3F this[int index] {
			get => verts[index];
			set => verts[index] = value;
		}

		public PolyhedronF(IEnumerable<Vector3F> verts, IEnumerable<Tuple<int, int>> edges, IEnumerable<Tuple<int, int, int>> faces) {
			this.verts = verts.ToArray();
			this.edges = edges.Select(e => new EdgeF(this, e.Item1, e.Item2)).ToArray();
			this.faces = faces.Select(f => new FaceF(this, f.Item1, f.Item2, f.Item3)).ToArray();
		}

		public bool Contains(Vector3F vec) {
			throw new NotImplementedException();
		}

		public List<Vector3F> Intersections(SolidF sol) {
			List<Vector3F> res = new List<Vector3F>();
			foreach (Line3F edge in Edges) {
				res.AddRange(edge.Intersections(sol));
			}
			return res;
		}
	}
}
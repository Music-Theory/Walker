namespace Walker.Data.Geometry.Speed.Space {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class PolyhedronF : SolidF {

		public readonly Vector3F[] verts;

		public readonly FaceF[] faces;

		public List<Vector3F> Vertices => verts.ToList();

		public List<Line3F> Edges {
			get {
				throw new NotImplementedException();
			}
		}

		public List<FaceF> Faces => faces.ToList();

		public Vector3F this[int index] => GetVert(index);

		public PolyhedronF(IEnumerable<Vector3F> verts, IEnumerable<int[]> faceVertInds) {
			this.verts = verts.ToArray();

			List<FaceF> faceList = new List<FaceF>();
			foreach (int[] inds in faceVertInds) {
				faceList.AddRange(FaceF.FanTriangulation(this, inds));
			}
			faces = faceList.ToArray();
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

		public Vector3F GetVert(int index) {
			return verts[index];
		}
	}
}
namespace Walker.Data.Geometry.Speed.Space {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// A 3D solid defined with floats. Uses CCW winding. Please don't make anything concave.
	/// </summary>
	public class PolyhedronF : SolidF {

		readonly Vector3F[] verts;

		readonly FaceF[] faces;

		public Vector3F[] Vertices => verts;

		public EdgeF[] Edges => Faces.SelectMany(f => f.Edges).Distinct().ToArray();

		public FaceF[] Faces => faces;

		public static readonly PolyhedronF Tetra = new PolyhedronF(
			new Vector3F[] {new[] {-.5f, -.5f, -.5f}, new[] {.5f, -.5f, -.5f}, new[] {-.5f, .5f, -.5f}, new[] {-.5f, -.5f, .5f}},
			new[] {new[] {0, 1, 2}, new[] {1, 2, 3}, new[] {0, 2, 3}}
		);

		public static readonly PolyhedronF Cube = new PolyhedronF(
			new Vector3F[] {
				               new[] {-.5f, -.5f, -.5f}, new[] {.5f, -.5f, -.5f}, new[] {.5f, .5f, -.5f}, new[] {-.5f, .5f, -.5f},
				               new[] {.5f, -.5f, .5f}, new[] {-.5f, -.5f, .5f}, new[] {-.5f, .5f, .5f}, new[] {.5f, .5f, .5f}
			               },
			new[] {
				      new[] {0, 1, 2}, new[] {2, 3, 0}, // front
				      new[] {4, 5, 6}, new[] {6, 7, 4}, // back
				      new[] {5, 0, 3}, new[] {3, 6, 5}, // left
				      new[] {1, 4, 7}, new[] {7, 2, 1}, // right
				      new[] {3, 2, 7}, new[] {7, 6, 3}, // top
				      new[] {1, 0, 4}, new[] {4, 5, 1}  // bottom
			      }
		);

		public Vector3F this[int index] {
			get => verts[index];
			set => verts[index] = value;
		}

		public PolyhedronF() : this(Tetra) { }

		public PolyhedronF(PolyhedronF copy) {
			verts = new Vector3F[copy.verts.Length];
			Array.Copy(copy.verts, verts, copy.verts.Length);
			faces = new FaceF[copy.faces.Length];
			Array.Copy(copy.verts, verts, copy.verts.Length);
		}

		public PolyhedronF(IEnumerable<Vector3F> verts, IEnumerable<int[]> faces) {
			this.verts = verts.ToArray();
			this.faces = faces.Select(f => new FaceF(this, f[0], f[1], f[2])).ToArray();
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

		public override bool Equals(object obj) {
			return obj is SolidF s && verts.SequenceEqual(s.Vertices) && faces.SequenceEqual(s.Faces);
		}

		protected bool Equals(PolyhedronF other) => Equals(verts, other.verts) && Equals(faces, other.faces);

		public override int GetHashCode() {
			unchecked {
				return verts.GetHashCode() * 397 ^ faces.GetHashCode();
			}
		}
	}
}
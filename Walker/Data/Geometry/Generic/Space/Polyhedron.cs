namespace Walker.Data.Geometry.Generic.Space {
	using System.Collections.Generic;
	using System.Linq;

	public class Polyhedron<T> : Solid<T> {

		List<Vector3<T>> vertices;

		public Polyhedron(IEnumerable<Vector3<T>> points) {
			vertices = points.ToList();
		}

		public Polyhedron(params Vector3<T>[] points) {
			vertices = points.ToList();
		}

		public Polyhedron(Polyhedron<T> copy) {
			vertices = new List<Vector3<T>>(copy.vertices);
		}

		public bool Contains(Vector3<T> vec) {
			throw new System.NotImplementedException();
		}

		public bool Intersects(Solid<T> sol) {
			throw new System.NotImplementedException();
		}


	}
}
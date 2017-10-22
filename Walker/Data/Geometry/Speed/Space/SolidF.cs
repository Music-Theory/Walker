namespace Walker.Data.Geometry.Speed.Space {
	using System.Collections.Generic;

	public interface SolidF {

		Vector3F this[int index] { get; set; }

		bool Contains(Vector3F vec);

		List<Vector3F> Intersections(SolidF sol);

		Vector3F[] Vertices { get; }

		EdgeF[] Edges { get; }

		FaceF[] Faces { get; }

	}
}
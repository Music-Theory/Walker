namespace Walker.Data.Geometry.Speed.Space {
	using System.Collections.Generic;

	public interface SolidF {

		bool Contains(Vector3F vec);

		List<Vector3F> Intersections(SolidF sol);

		List<FaceF> Faces { get; }

		List<Line3F> Edges { get; }

		Vector3F GetVert(int index);

	}
}
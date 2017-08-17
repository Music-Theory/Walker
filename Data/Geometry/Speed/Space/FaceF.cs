namespace Walker.Data.Geometry.Speed.Space {
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using Generic.Space;

	// Today's note: Colonel Zaxby forgot to give me my god damnb tater chips

	/// <summary>
	/// The face of a solid. Always triangular.
	/// </summary>
	public struct FaceF {

		readonly int v0, v1, v2;

		readonly SolidF solid;

		public Vector3F this[int index] {
			get {
				switch (index) {
					case 0: return solid.GetVert(v0);
					case 1: return solid.GetVert(v1);
					case 2: return solid.GetVert(v2);
					default: throw new ArgumentOutOfRangeException();
				}
			}
		}

		public Vector3F Normal {
			get {
				Vector3F ab = this[1] - this[0];
				Vector3F bc = this[2] - this[1];
				return ab.Cross(bc);
			}
		}

		public FaceF(SolidF solid, int v0, int v1, int v2) {
			this.solid = solid;
			this.v0 = v0;
			this.v1 = v1;
			this.v2 = v2;
		}

		public FaceF(SolidF solid, params int[] indices) {
			if (indices.Length != 3) { throw new ArgumentException("Triangles must have 3 vertices"); }
			this.solid = solid;
			v0 = indices[0];
			v1 = indices[1];
			v2 = indices[2];
		}

		/// <summary>
		/// Gets the fan triangulation of a polygon. Only works if the polygon is convex.
		/// </summary>
		/// <param name="solid">The solid that this polygon is a part of</param>
		/// <param name="indices">Vertex indices of the polygon</param>
		/// <returns>List of triangles generated from the polygon</returns>
		public static List<FaceF> FanTriangulation(SolidF solid, params int[] indices) {
			List<FaceF> res = new List<FaceF>();
			for (int i = 2; i < indices.Length; i++) {
				res.Add(new FaceF(solid, indices[0], indices[i - 1], indices[i]));
			}
			return res;
		}

	}
}
namespace Walker.Data.Geometry.Speed.Space {
	using System;
	using System.Collections.Generic;
	using System.Net.Http.Headers;
	using Generic.Space;

	public struct PrismF : SolidF {

		public Vector3F left, right;

		public Vector3F this[int index] => GetVert(index);

		public PrismF(Vector3F left, Vector3F right) {
			this.left = left;
			this.right = right;
		}

		public bool Contains(Vector3F vec) {
			return !(vec.x < left.x) && !(vec.x > right.x)
			       && !(vec.y < left.y) && !(vec.y > right.y)
			       && !(vec.z < left.z) && !(vec.z > right.z);
		}

		public List<FaceF> Faces {
			get {
				List<FaceF> res = new List<FaceF>();
				res.AddRange(FaceF.FanTriangulation(this, 0, 3, 2, 1)); // top
				res.AddRange(FaceF.FanTriangulation(this, 7, 4, 5, 6)); // bottom
				res.AddRange(FaceF.FanTriangulation(this, 0, 3, 4, 5)); // left
				res.AddRange(FaceF.FanTriangulation(this, 7, 6, 1, 2)); // right
				res.AddRange(FaceF.FanTriangulation(this, 0, 4, 5, 1)); // front
				res.AddRange(FaceF.FanTriangulation(this, 7, 2, 3, 4)); // back
				return res;
			}
		}

		public List<Line3F> Edges {
			get {
				List<Line3F> res = new List<Line3F> {
					                                    new Line3F(this[0], this[1] - this[0]),
					                                    new Line3F(this[1], this[2] - this[1]),
					                                    new Line3F(this[2], this[3] - this[2]),
					                                    new Line3F(this[3], this[0] - this[3]),
					                                    new Line3F(this[0], this[5] - this[0]),
					                                    new Line3F(this[1], this[6] - this[1]),
					                                    new Line3F(this[2], this[7] - this[2]),
					                                    new Line3F(this[3], this[4] - this[3]),
					                                    new Line3F(this[4], this[5] - this[4]),
					                                    new Line3F(this[5], this[6] - this[5]),
					                                    new Line3F(this[6], this[7] - this[6]),
					                                    new Line3F(this[7], this[4] - this[7])
				                                    };
				return res;
			}
		}

		public List<Vector3F> Vertices {
			get {
				List<Vector3F> res = new List<Vector3F>();
				for (int i = 0; i < 8; i++) {
					res.Add(this[i]);
				}
				return res;
			}
		}

		public Vector3F GetVert(int index) {
			switch (index) {
				case 0: return left;
				case 1: return new Vector3F(right.x, left.y, left.z);
				case 2: return new Vector3F(right.x, left.y, right.z);
				case 3: return new Vector3F(left.x, left.y, right.z);
				case 4: return new Vector3F(left.x, right.y, right.z);
				case 5: return new Vector3F(left.x, right.y, left.z);
				case 6: return new Vector3F(right.x, right.y, left.z);
				case 7: return right;
				default: throw new ArgumentOutOfRangeException();
			}
		}

		public List<Vector3F> Intersections(SolidF o) {
			List<Vector3F> res = new List<Vector3F>();
			if (o is PrismF && (left.x > o.GetVert(0).x || right.x < o.GetVert(0).x
			    || left.y > o.GetVert(7).y || right.y < o.GetVert(0).y
			    || left.z > o.GetVert(7).z || right.z < o.GetVert(0).z)) { return res; }
			foreach (Line3F edge in Edges) {
				res.AddRange(edge.Intersections(o));
			}
			return res;
		}

		public bool Equals(PrismF other) {
			return left.Equals(other.left) && right.Equals(other.right);
		}

		public bool Equals(Prism<float> other) {
			return left.Equals(other.left) && right.Equals(other.right);
		}

		public override bool Equals(object obj) {
			if (obj is PrismF) { return Equals((PrismF) obj); }
			if (obj is Prism<float>) { return Equals((Prism<float>) obj); }
			return false;
		}

		public override int GetHashCode() {
			unchecked {
				return (left.GetHashCode() * 397) ^ right.GetHashCode();
			}
		}

		public static bool operator ==(PrismF left, PrismF right) {
			return left.Equals(right);
		}

		public static bool operator !=(PrismF left, PrismF right) {
			return !(left == right);
		}
	}
}
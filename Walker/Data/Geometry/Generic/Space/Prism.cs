namespace Walker.Data.Geometry.Generic.Space {
	public struct Prism<T> : Solid<T> {

		public Vector3<T> left, right;

		public Prism(Vector3<T> left, Vector3<T> right) {
			this.left = left;
			this.right = right;
		}

		public bool Contains(Vector3<T> vec) {
			return !Operator<T>.LessThan(vec.X, left.X) && !Operator<T>.GreaterThan(vec.X, right.X)
			       && !Operator<T>.LessThan(vec.Y, left.Y) && !Operator<T>.GreaterThan(vec.Y, right.Y)
			       && !Operator<T>.LessThan(vec.Z, left.Z) && !Operator<T>.GreaterThan(vec.Z, right.Z);
		}

		public bool Intersects(Solid<T> sol) {
			if (sol is Prism<T>) { return Intersects((Prism<T>) sol); }
			throw new System.NotImplementedException();
		}

		public bool Intersects(Prism<T> o) {
			return !(Operator<T>.GreaterThan(left.X, o.right.X) || Operator<T>.LessThan(right.X, o.left.X)
			         || Operator<T>.GreaterThan(left.Y, o.right.Y) || Operator<T>.LessThan(right.Y, o.left.Z)
			         || Operator<T>.GreaterThan(left.Z, o.right.Z) || Operator<T>.LessThan(right.Z, o.left.Y));
		}

		public bool Equals(Prism<T> other) {
			return left == other.left && right == other.right;
		}

		public override bool Equals(object obj) {
			if (!(obj is Prism<T>)) { return false; }
			return Equals((Prism<T>) obj);
		}

		public override int GetHashCode() {
			unchecked {
				return (left.GetHashCode() * 397) ^ right.GetHashCode();
			}
		}

		public static bool operator ==(Prism<T> left, Prism<T> right) {
			return left.Equals(right);
		}

		public static bool operator !=(Prism<T> left, Prism<T> right) {
			return !(left == right);
		}

	}
}
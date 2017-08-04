namespace Walker.Data.Vector.Generic.Plane {
	public struct Rectangle<T> {
		public Vector2<T> left, right;

		public Rectangle(Vector2<T> left, Vector2<T> right) {
			this.left = left;
			this.right = right;
		}

		public bool Contains(Vector2<T> vec) {
			return !Operator<T>.LessThan(vec.X, left.X) && !Operator<T>.GreaterThan(vec.X, right.X)
			       && !Operator<T>.LessThan(vec.Y, left.Y) && !Operator<T>.GreaterThan(vec.Y, right.Y);
		}

		public bool Equals(Rectangle<T> other) {
			return left == other.left && right == other.right;
		}

		public override bool Equals(object obj) {
			if (!(obj is Rectangle<T>)) { return false; }
			return Equals((Rectangle<T>) obj);
		}

		public override int GetHashCode() {
			unchecked {
				return (left.GetHashCode() * 397) ^ right.GetHashCode();
			}
		}

		public static bool operator ==(Rectangle<T> left, Rectangle<T> right) {
			return left.Equals(right);
		}

		public static bool operator !=(Rectangle<T> left, Rectangle<T> right) {
			return !(left == right);
		}

	}
}
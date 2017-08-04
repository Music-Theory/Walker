namespace Walker.Data.Vector.Generic.Space {
	using System;

	public struct Line3<T> {

		public Vector3<T> o, d;

		public Vector3<T> End {
			get => o + d;
			set => d = value - o;
		}

		public T Length {
			get => d.Length;
			set => d = d * Operator<T>.Div(value, Length);
		}

		public Line3(Vector3<T> o, Vector3<T> d) {
			this.o = o;
			this.d = d;
		}

		public bool Intersects(Face<T> face) {
			throw new NotImplementedException();
		}

	}
}
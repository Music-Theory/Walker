namespace Walker.Data.Vector.Generic.Space {
	public struct Face<T> {

		public bool Intersects(Line3<T> line) {
			return line.Intersects(this);
		}

		public bool Intersects(Solid<T> sol) {
			throw new System.NotImplementedException();
		}

	}
}
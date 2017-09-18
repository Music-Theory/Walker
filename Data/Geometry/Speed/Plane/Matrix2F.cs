namespace Walker.Data.Geometry.Speed.Plane {
	using System;

	public class Matrix2F {

		// TODO - Get this as functional as Matrix3F

		float[,] values;

		public float this[int i, int j] {
			get => values[i, j];
			set => values[i, j] = value;
		}

		public float Determinant => this[0, 0] * this[1, 1] - this[1, 0] * this[0, 1];

		public Matrix2F(float[,] values) {
			if (values.GetLength(0) != 2 || values.GetLength(1) != 2) {
				throw new ArgumentException("Must supply 2x2 array to Matrix2F constructor");
			}
			this.values = values;
		}

		public Matrix2F(params float[] values) {
			if (values.Length > 4) { throw new ArgumentException("2x2 matrix cannot have more than 4 entries"); }
			this.values = new float[2,2];
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					int index = i + j * 2;
					if (index >= values.Length) { return; }
					this[i, j] = values[i + j * 2];
				}
			}
		}

		public Matrix2F(float[][] values) {
			if (values.Length != 2 || values[0].Length != 2 || values[1].Length != 2) {
				throw new ArgumentException("Must supply 2x2 array to Matrix2F constructor");
			}
			this.values = new float[2,2];
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					this[i, j] = values[i][j];
				}
			}
		}
	}
}
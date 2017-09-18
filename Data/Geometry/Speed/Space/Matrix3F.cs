namespace Walker.Data.Geometry.Speed.Space {
	using System;
	using Plane;

	public class MatrixInversionException : Exception {
		public MatrixInversionException(string msg) : base(msg) { }
	}

	public struct Matrix3F {

		// If you want to make the calculations lazy, use a bool[3,3]
		// and stuff like HasChanged, RowChanged(int), ColumnChanged(int)

		float[,] values;

		public float this[int i, int j] {
			get => values[i, j];
			set => values[i, j] = value;
		}

		public float[,] Values => values;

		#region Rows & Columns

			public Vector3F R1 {
				get => new Vector3F(this[0, 0], this[1, 0], this[2, 0]);
				set {
					this[0, 0] = value.x;
					this[1, 0] = value.y;
					this[2, 0] = value.z;
				}
			}

			public Vector3F R2 {
				get => new Vector3F(this[0, 1], this[1, 1], this[2, 1]);
				set {
					this[0, 1] = value.x;
					this[1, 1] = value.y;
					this[2, 1] = value.z;
				}
			}

			public Vector3F R3 {
				get => new Vector3F(this[0, 2], this[1, 2], this[2, 2]);
				set {
					this[0, 2] = value.x;
					this[1, 2] = value.y;
					this[2, 2] = value.z;
				}
			}

			public Vector3F C1 {
				get => new Vector3F(this[0, 0], this[0, 1], this[0, 2]);
				set {
					this[0, 0] = value.x;
					this[0, 1] = value.y;
					this[0, 2] = value.z;
				}
			}

			public Vector3F C2 {
				get => new Vector3F(this[1, 0], this[1, 1], this[1, 2]);
				set {
					this[1, 0] = value.x;
					this[1, 1] = value.y;
					this[1, 2] = value.z;
				}
			}

			public Vector3F C3 {
				get => new Vector3F(this[2, 0], this[2, 1], this[2, 2]);
				set {
					this[2, 0] = value.x;
					this[2, 1] = value.y;
					this[2, 2] = value.z;
				}
			}

		#endregion

		public Matrix3F Transpose {
			get => new Matrix3F(new[,] {
				                           {this[0, 0], this[0, 1], this[0, 2]},
				                           {this[1, 0], this[1, 1], this[1, 2]},
				                           {this[2, 0], this[2, 1], this[2, 2]}
			                           });
			set => values = value.Transpose.Values;
		}

		public float Determinant {
			get {
				Matrix2F a = new Matrix2F(this[1, 1], this[2, 1], this[1, 2], this[2, 2]);
				Matrix2F b = new Matrix2F(this[0, 1], this[2, 1], this[0, 2], this[2, 2]);
				Matrix2F c = new Matrix2F(this[0, 1], this[1, 1], this[0, 2], this[1, 2]);
				return this[0, 0] * a.Determinant - this[1, 0] * b.Determinant + this[2, 0] * c.Determinant;
			}
		}

		public Matrix3F Inverse {
			get {
				if (Math.Abs(Determinant) < Vector3F.Tolerance) {
					throw new MatrixInversionException("Inverse does not exist");
				}
				throw new NotImplementedException();
			}
			set => values = value.Inverse.Values;
		}

		public Matrix3F(float[,] values) {
			if (values.GetLength(0) != 3 || values.GetLength(1) != 3) { throw new ArgumentException("Must supply 3x3 array to 3x3 matrix constructor"); }
			this.values = values;
		}

		public Matrix3F(float[][] values) {
			if (values.Length != 3 || values[0].Length != 3 || values[1].Length != 3 || values[2].Length != 3) {
				throw new ArgumentException("Must supply 3x3 array to 3x3 matrix constructor");
			}
			this.values = new float[3,3];
			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 3; j++) {
					this[i, j] = values[i][j];
				}
			}
		}

		public Matrix3F(params float[] values) {
			if (values.Length > 9) { throw new ArgumentException("Can't have more than 9 values in a 3x3 matrix"); }
			this.values = new float[3,3];
			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 3; j++) {
					int index = j * 3 + i;
					if (index >= values.Length) { return; }
					this[i, j] = values[j * 3 + i];
				}
			}
		}

		public Matrix3F(Vector3F g1, Vector3F g2, Vector3F g3, bool columns = false) {
			values = new float[3,3];
			if (columns) {
				C1 = g1;
				C2 = g2;
				C3 = g3;
			} else {
				R1 = g1;
				R2 = g2;
				R3 = g3;
			}
		}

		public static Matrix3F operator -(Matrix3F a) {
			return a * -1;
		}

		public static Matrix3F operator +(Matrix3F a, Matrix3F b) {
			return new Matrix3F(a.R1 + b.R1, a.R2 + b.R2, a.R3 + b.R3);
		}

		public static Matrix3F operator -(Matrix3F a, Matrix3F b) {
			return new Matrix3F(a.R1 - b.R1, a.R2 - b.R2, a.R3 - b.R3);
		}

		public static Matrix3F operator *(Matrix3F a, Matrix3F b) {
			return new Matrix3F(new[,] {
				                           {a.R1.Dot(b.C1), a.R1.Dot(b.C2), a.R1.Dot(b.C3)},
				                           {a.R2.Dot(b.C1), a.R2.Dot(b.C2), a.R2.Dot(b.C3)},
				                           {a.R3.Dot(b.C1), a.R3.Dot(b.C2), a.R3.Dot(b.C3)}
			                           });
		}

		public static Vector3F operator *(Matrix3F a, Vector3F b) {
			return new Vector3F(a.R1.Dot(b), a.R2.Dot(b), a.R3.Dot(b));
		}

		public static Vector3F operator *(Vector3F a, Matrix3F b) {
			return new Vector3F(a.Dot(b.R1), a.Dot(b.R2), a.Dot(b.R3));
		}

		public static Matrix3F operator *(Matrix3F a, float b) {
			return new Matrix3F(a.R1 * b, a.R2 * b, a.R3 * b);
		}

		public static Matrix3F operator *(float a, Matrix3F b) {
			return new Matrix3F(b.R1 * a, b.R2 * a, b.R3 * a);
		}

		public static Matrix3F operator /(Matrix3F a, Matrix3F b) {
			return a * b.Inverse;
		}

	}
}
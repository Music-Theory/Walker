namespace Walker.Data.Geometry.Speed.Rotation {
	using System;
	using Generic.Space;
	using Space;

	public struct Vector4F {

		public static readonly Vector4F Default = new Vector4F(0, 0, 0, 0);

		public float x, y, z, w;

		public float Length {
			get => (float) Math.Sqrt(x * x + y * y + z * z + w * w);
			set {
				float scale = value / Length;
				x *= scale;
				y *= scale;
				z *= scale;
				w *= scale;
			}
		}

		public Matrix4F QMatrix {
			get {
				if (Math.Abs(Length - 1) > GeoMeta.Tolerance) {
					return new Matrix4F(new[,] {
						                           {w * w + x * x - y * y - z * z, 2 * x * y - 2 * w * z, 2 * x * z + 2 * w * y, 0},
						                           {2 * x * y + 2 * w * z, w * w - x * x + y * y - z * z, 2 * y * z + 2 * w * x, 0},
						                           {2 * x * z - 2 * w * y, 2 * y * z - 2 * w * x, w * w - x * x - y * y + z * z, 0},
						                           {0, 0, 0, 1}
					                           });
				}
				return new Matrix4F(new[,] {
					                           {1 - 2 * y * y - 2 * z * z, 2 * x * y - 2 * w * z, 2 * x * z + 2 * w * y, 0},
					                           {2 * x * y + 2 * w * z, 1 - 2 * x * x - 2 * z * z, 2 * y * z + 2 * w * x, 0},
					                           {2 * x * z - 2 * w * y, 2 * y * z - 2 * w * x, 1 - 2 * x * x - 2 * y * y, 0},
					                           {0, 0, 0, 1}
				                           });
			}
		}

		/// <summary>
		/// Construct quaternion from axis and angle
		/// </summary>
		/// <param name="axis">Axis of rotation</param>
		/// <param name="angle">Angle of rotation</param>
		public Vector4F(Vector3F axis, float angle) {
			float a2 = angle / 2;
			double sinA2 = Math.Sin(a2);
			x = (float) (axis.x * sinA2);
			y = (float) (axis.y * sinA2);
			z = (float) (axis.z * sinA2);
			w = (float) Math.Cos(a2);
		}

		public Vector4F(float x, float y, float z, float w) {
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		/// <summary>
		/// Gets the dot product of two vectors
		/// </summary>
		/// <param name="other">The right side of the function</param>
		/// <returns>a [dot] b</returns>
		public float Dot(Vector4F other) {
			return x * other.x + y * other.y + z * other.z + w * other.w;
		}

		/// <summary>
		/// Throws an exception because the cross product of 4D vectors doesn't exist
		/// </summary>
		/// <param name="other">The right side of the function</param>
		/// <returns>a X b</returns>
		public Vector3F Cross(Vector3F other) {
			throw new NullReferenceException("Cross products do not exist in euclidean spaces other than 3 and 7");
		}

		/// <summary>
		/// Operator - overload ; returns the opposite of a vector
		/// </summary>
		/// <param name="v">Vector to negate</param>
		/// <returns>-v</returns>
		public static Vector4F operator -(Vector4F v) {
			return new Vector4F(-v.x, -v.y, -v.z, -v.w);
		}

		/// <summary>
		/// Operator - overload ; subtracts two vectors
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 - v2</returns>
		public static Vector4F operator -(Vector4F v1, Vector4F v2) {
			return new Vector4F(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
		}

		/// <summary>
		/// Operator + overload ; add two vectors
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 + v2</returns>
		public static Vector4F operator +(Vector4F v1, Vector4F v2) {
			return new Vector4F(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
		}

		/// <summary>
		/// Operator * overload ; multiply two vectors
		/// </summary>
		/// <param name="a">Vec 1</param>
		/// <param name="b">Vec 2</param>
		/// <returns>a * b</returns>
		public static Vector4F operator *(Vector4F a, Vector4F b) {
			return new Vector4F(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

		/// <summary>
		/// Operator * overload ; multiply a vector by a scalar value
		/// </summary>
		/// <param name="v">Vector</param>
		/// <param name="x">Scalar value</param>
		/// <returns>v * x</returns>
		public static Vector4F operator *(Vector4F v, float x) {
			return new Vector4F(v.x * x, v.y * x, v.z * x, v.w * x);
		}

		/// <summary>
		/// Operator * overload ; multiply a scalar value by a vector.
		/// </summary>
		/// <param name="x">Scalar value</param>
		/// <param name="v">Vector</param>
		/// <returns>x * v</returns>
		public static Vector4F operator *(float x, Vector4F v) {
			return new Vector4F(v.x * x, v.y * x, v.z * x, v.w * x);
		}

		/// <summary>
		/// Operator / overload ; divide a vector by a scalar value
		/// </summary>
		/// <param name="v">Vector</param>
		/// <param name="x">Scalar value</param>
		/// <returns>v / x</returns>
		public static Vector4F operator /(Vector4F v, float x) {
			return new Vector4F(v.x / x, v.y / x, v.z / x, v.w / x);
		}

		/// <summary>
		/// Operator / overload ; divide one vector by another
		/// </summary>
		/// <param name="a">First vector</param>
		/// <param name="b">Second vector</param>
		/// <returns>a / b</returns>
		public static Vector4F operator /(Vector4F a, Vector4F b) {
			return new Vector4F(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
		}

		public override bool Equals(object obj) {
			if (obj is Vector4F) { return Equals((Vector4F) obj); }
			//if (obj is Vector4<float>) { return Equals((Vector4<float>) obj); }
			return false;
		}

		public override int GetHashCode() {
			unchecked {
				int hashCode = x.GetHashCode();
				hashCode = (hashCode * 397) ^ y.GetHashCode();
				hashCode = (hashCode * 397) ^ z.GetHashCode();
				hashCode = (hashCode * 397) ^ w.GetHashCode();
				return hashCode;
			}
		}

		public bool Equals(Vector4F other) {
			return this == other;
		}

		/*public bool Equals(Vector3<float> other) {
			return this == other;
		}*/

		public override string ToString() {
			return "<" + x + ", " + y + ", " + z + ", " + w + ">";
		}

		public static bool operator ==(Vector4F a, Vector4F b) {
			return Math.Abs(a.x - b.x) < GeoMeta.Tolerance
			       && Math.Abs(a.y - b.y) < GeoMeta.Tolerance
			       && Math.Abs(a.z - b.z) < GeoMeta.Tolerance
			       && Math.Abs(a.w - b.w) < GeoMeta.Tolerance;
		}

		public static bool operator !=(Vector4F a, Vector4F b) {
			return !(a == b);
		}

		/*public static bool operator ==(Vector3F a, Vector3<float> b) {
			return Math.Abs(a.x - b.X) < Tolerance
			       && Math.Abs(a.y - b.Y) < Tolerance
			       && Math.Abs(a.z - b.Z) < Tolerance;
		}*/

		/*public static bool operator !=(Vector3F a, Vector3<float> b) {
			return !(a == b);
		}*/

		/*public static implicit operator Vector3<float>(Vector3F vec) {
			return new Vector3<float>(vec.x, vec.y, vec.z);
		}*/

		/*public static implicit operator Vector3F(Vector3<float> vec) {
			return new Vector3F(vec.X, vec.Y, vec.Z);
		}*/

		public static explicit operator float[](Vector4F vec) {
			return new [] {vec.x, vec.y, vec.z, vec.w};
		}

		/// <summary>
		/// Quaternion Multiplication, because C# doesn't have struct inheritance
		/// </summary>
		/// <param name="b">The other quaternion</param>
		/// <returns>this * b</returns>
		public Vector4F QMult(Vector4F b) {
			return new Vector4F(
				w * b.x + x * b.w + y * b.z - z * b.y,
				w * b.y - x * b.z + y * b.w + z * b.x,
				w * b.z + x * b.y - y * b.x + z * b.w,
				w * b.w - x * b.x - y * b.y - z * b.z
			);
		}

	}
}
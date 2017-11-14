namespace Walker.Data.Geometry.Speed.Plane {
	using System;
	using System.Runtime.InteropServices;
	using Generic;
	using Generic.Plane;

	/// <summary>
	/// A vector in 2D space, defined with floats.
	/// X increases rightward.
	/// Y increases upward.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector2F {

		/// <summary>
		/// Construct the vector from its coordinates
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		public Vector2F(float x, float y) {
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// Operator - overload ; returns the opposite of a vector
		/// </summary>
		/// <param name="v">Vector to negate</param>
		/// <returns>-v</returns>
		public static Vector2F operator -(Vector2F v) {
			return new Vector2F(-v.x, -v.y);
		}

		/// <summary>
		/// Operator - overload ; subtracts two vectors
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 - v2</returns>
		public static Vector2F operator -(Vector2F v1, Vector2F v2) {
			return new Vector2F(v1.x - v2.x, v1.y - v2.y);
		}

		/// <summary>
		/// Operator + overload ; add two vectors
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 + v2</returns>
		public static Vector2F operator +(Vector2F v1, Vector2F v2) {
			return new Vector2F(v1.x + v2.x, v1.y + v2.y);
		}

		/// <summary>
		/// Operator * overload ; multiply a vector by a scalar value
		/// </summary>
		/// <param name="v">Vector</param>
		/// <param name="x">Scalar value</param>
		/// <returns>v * x</returns>
		////////////////////////////////////////////////////////////
		public static Vector2F operator *(Vector2F v, float x) {
			return new Vector2F(v.x * x, v.y * x);
		}

		/// <summary>
		/// Operator * overload ; multiply a scalar value by a vector.
		/// </summary>
		/// <param name="x">Scalar value</param>
		/// <param name="v">Vector</param>
		/// <returns>x * v</returns>
		public static Vector2F operator *(float x, Vector2F v) {
			return new Vector2F(x * v.x, x * v.y);
		}

		/// <summary>
		/// Operator * overload ; multiply a vector by a vector
		/// </summary>
		/// <param name="x">Vector 1</param>
		/// <param name="y">Vector 2</param>
		/// <returns>x * y</returns>
		public static Vector2F operator *(Vector2F x, Vector2F y) {
			return new Vector2F(x.x * y.x, x.y * y.y);
		}

		/// <summary>
		/// Operator / overload ; divide a vector by a scalar value
		/// </summary>
		/// <param name="v">Vector</param>
		/// <param name="x">Scalar value</param>
		/// <returns>v / x</returns>
		public static Vector2F operator /(Vector2F v, float x) {
			return new Vector2F(v.x / x, v.y / x);
		}

		/// <summary>
		/// Operator / overload ; divide a vector by a vector
		/// </summary>
		/// <param name="x">Vector 1</param>
		/// <param name="y">Vector 2</param>
		/// <returns>x / y</returns>
		public static Vector2F operator /(Vector2F x, Vector2F y) {
			return new Vector2F(x.x / y.x, x.y / y.y);
		}

		/// <summary>
		/// Operator == overload ; check vector equality
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 == v2</returns>
		public static bool operator ==(Vector2F v1, Vector2F v2) {
			return v1.Equals(v2);
		}

		/// <summary>
		/// Operator != overload ; check vector inequality
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 != v2</returns>
		public static bool operator !=(Vector2F v1, Vector2F v2) {
			return !v1.Equals(v2);
		}

		/// <summary>
		/// Provide a string describing the object
		/// </summary>
		/// <returns>String description of the object</returns>
		public override string ToString() {
			return "<" + x + ", " + y + ">";
		}

		/// <summary>
		/// Compare vector and object and checks if they are equal
		/// </summary>
		/// <param name="obj">Object to check</param>
		/// <returns>Object and vector are equal</returns>
		public override bool Equals(object obj) {
			return obj is Vector2F f && Equals(f)
				|| obj is Vector2<float> g && Equals(g);
		}

		/// <summary>
		/// Compare two vectors and checks if they are equal
		/// </summary>
		/// <param name="other">Vector to check</param>
		/// <returns>Vectors are equal</returns>
		public bool Equals(Vector2F other) {
			return Math.Abs(x - other.x) < GeoMeta.Tolerance && Math.Abs(y - other.y) < GeoMeta.Tolerance;
		}

		/// <summary>
		/// Provide a integer describing the object
		/// </summary>
		/// <returns>Integer description of the object</returns>
		public override int GetHashCode() {
			return x.GetHashCode() ^
			       y.GetHashCode();
		}

		/// <summary>X (horizontal) component of the vector</summary>
		public float x;

		/// <summary>Y (vertical) component of the vector</summary>
		public float y;
	}
}
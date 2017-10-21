namespace Walker.Data.Geometry.Generic.Space {
	using System;
	using System.Runtime.InteropServices;

	/// <summary>
	/// Vector3 is an generic utility class for manipulating
	/// 3-dimensional vectors. Taken from a branch of SFML.Net,
	/// because they didn't have a .NET Core version.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3<T> : IEquatable<Vector3<T>> {

		public T Length {
			get => throw new NotImplementedException(); //Operator<T>.Root(Operator<T>.Add(, Operator<T>.Add(Operator<T>.Pow(Y, 2))), (T) Convert.ChangeType(2, typeof(T)));
			set => throw new NotImplementedException();
		}

		/// <summary>
		/// Construct the vector from its coordinates
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		public Vector3(T x, T y, T z) {
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		/// Gets the dot product of two vectors
		/// </summary>
		/// <param name="other">The right side of the function</param>
		/// <returns>a [dot] b</returns>
		public T Dot(Vector3<T> other) {
			// hoh boy
			return Operator<T>.Add(Operator<T>.Mult(X, other.X),
			                       Operator<T>.Add(Operator<T>.Mult(Y, other.Y),
			                                       Operator<T>.Mult(Z, other.Z)));
		}

		public Vector3<T> Cross(Vector3<T> other) {
			T x = Operator<T>.Sub(Operator<T>.Mult(Y, other.Z), Operator<T>.Mult(Z, other.Y));
			T y = Operator<T>.Sub(Operator<T>.Mult(Z, other.X), Operator<T>.Mult(X, other.Z));
			T z = Operator<T>.Sub(Operator<T>.Mult(X, other.Y), Operator<T>.Mult(Y, other.X));
			return new Vector3<T>(x, y, z);
		}

		/// <summary>
		/// Operator - overload ; returns the opposite of a vector
		/// </summary>
		/// <param name="v">Vector to negate</param>
		/// <returns>-v</returns>
		public static Vector3<T> operator -(Vector3<T> v) {
			return new Vector3<T>(Operator<T>.Negate(v.X), Operator<T>.Negate(v.Y), Operator<T>.Negate(v.Z));
		}

		/// <summary>
		/// Operator - overload ; subtracts two vectors
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 - v2</returns>
		public static Vector3<T> operator -(Vector3<T> v1, Vector3<T> v2) {
			return new Vector3<T>(Operator<T>.Sub(v1.X, v2.X), Operator<T>.Sub(v1.Y, v2.Y),
			                      Operator<T>.Sub(v1.Z, v2.Z));
		}

		/// <summary>
		/// Operator + overload ; add two vectors
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 + v2</returns>
		public static Vector3<T> operator +(Vector3<T> v1, Vector3<T> v2) {
			return new Vector3<T>(Operator<T>.Add(v1.X, v2.X), Operator<T>.Add(v1.Y, v2.Y), Operator<T>.Add(v1.Z, v2.Z));
		}

		/// <summary>
		/// Operator * overload ; multiply a vector by a scalar value
		/// </summary>
		/// <param name="v">Vector</param>
		/// <param name="x">Scalar value</param>
		/// <returns>v * x</returns>
		public static Vector3<T> operator *(Vector3<T> v, T x) {
			return new Vector3<T>(Operator<T>.Mult(v.X, x), Operator<T>.Mult(v.Y, x), Operator<T>.Mult(v.Z, x));
		}

		/// <summary>
		/// Operator * overload ; multiply a scalar value by a vector.
		/// </summary>
		/// <param name="x">Scalar value</param>
		/// <param name="v">Vector</param>
		/// <returns>x * v</returns>
		public static Vector3<T> operator *(T x, Vector3<T> v) {
			return new Vector3<T>(Operator<T>.Mult(x, v.X), Operator<T>.Mult(x, v.Y), Operator<T>.Mult(x, v.Z));
		}

		/// <summary>
		/// Operator / overload ; divide a vector by a scalar value
		/// </summary>
		/// <param name="v">Vector</param>
		/// <param name="x">Scalar value</param>
		/// <returns>v / x</returns>
		public static Vector3<T> operator /(Vector3<T> v, T x) {
			return new Vector3<T>(Operator<T>.Div(v.X, x), Operator<T>.Div(v.Y, x), Operator<T>.Div(v.Z, x));
		}

		/// <summary>
		/// Operator / overload ; divide one vector by another
		/// </summary>
		/// <param name="x">First vector</param>
		/// <param name="y">Second vector</param>
		/// <returns>x / y</returns>
		public static Vector3<T> operator /(Vector3<T> x, Vector3<T> y) {
			return new Vector3<T>(Operator<T>.Div(x.X, y.X), Operator<T>.Div(x.Y, y.Y), Operator<T>.Div(x.Z, y.Z));
		}

		/// <summary>
		/// Operator == overload ; check vector equality
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 == v2</returns>
		public static bool operator ==(Vector3<T> v1, Vector3<T> v2) {
			return v1.Equals(v2);
		}

		/// <summary>
		/// Operator != overload ; check vector inequality
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 != v2</returns>
		public static bool operator !=(Vector3<T> v1, Vector3<T> v2) {
			return !v1.Equals(v2);
		}

		/// <summary>
		/// Provide a string describing the object
		/// </summary>
		/// <returns>String description of the object</returns>
		public override string ToString() {
			return "[Vector3<" + typeof(T).Name + ">]" +
			       " X(" + X + ")" +
			       " Y(" + Y + ")" +
			       " Z(" + Z + ")";
		}

		/// <summary>
		/// Compare vector and object and checks if they are equal
		/// </summary>
		/// <param name="obj">Object to check</param>
		/// <returns>Object and vector are equal</returns>
		public override bool Equals(object obj) {
			return (obj is Vector3<T>) && Equals((Vector3<T>) obj);
		}

		/// <summary>
		/// Compare two vectors and checks if they are equal
		/// </summary>
		/// <param name="other">Vector to check</param>
		/// <returns>Vectors are equal</returns>
		public bool Equals(Vector3<T> other) {
			return (Operator<T>.Equal(X, other.X) &&
			        Operator<T>.Equal(Y, other.Y) &&
			        Operator<T>.Equal(Z, other.Z));
		}

		/// <summary>
		/// Provide a integer describing the object
		/// </summary>
		/// <returns>Integer description of the object</returns>
		public override int GetHashCode() {
			return X.GetHashCode() ^
			       Y.GetHashCode() ^
			       Z.GetHashCode();
		}

		/// <summary>
		/// Explicit casting to another generic vector type
		/// </summary>
		/// <returns>Casting result</returns>
		public Vector3<K> Cast<K>() {
			return new Vector3<K>((K) Convert.ChangeType(X, typeof(K)),
			                      (K) Convert.ChangeType(Y, typeof(K)),
			                      (K) Convert.ChangeType(Z, typeof(K)));
		}

		/// <summary>X (horizontal) component of the vector</summary>
		public T X;

		/// <summary>Y (vertical) component of the vector</summary>
		public T Y;

		/// <summary>Z (depth) component of the vector</summary>
		public T Z;
	}
}
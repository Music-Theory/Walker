namespace Walker.Utility {
	using System;
	using Data.Geometry.Speed.Space;

	public static class StringExtensions {

		/// <summary>
		/// Experimental function that may or may not allow parsing of strings to most types.
		/// I have no idea whether this actually works.
		/// </summary>
		/// <param name="str">String to parse.</param>
		/// <param name="def">Default value.</param>
		/// <typeparam name="T">Type to parse to.</typeparam>
		/// <returns>T.Parse(str)</returns>
		public static T Parse<T>(this string str, T def) {
			if (str != null) { return (T) Convert.ChangeType(str, typeof(T)); }
			return def;
		}

		public static Vector3F ToVector3F(this string str, Vector3F def) {
			if (str != null) {
				str = str.Replace("<", "").Replace(" ", "").Replace(">", "");
				string[] split = str.Split(',');
				float x = float.Parse(split[0]);
				float y = float.Parse(split[1]);
				float z = float.Parse(split[2]);
				return new Vector3F(x, y, z);
			}
			return def;
		}

	}
}
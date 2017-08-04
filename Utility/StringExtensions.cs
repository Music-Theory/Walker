namespace Walker.Utility {
	using System;

	public static class StringExtensions {

		/// <summary>
		/// Experimental function that may or may not allow parsing of strings to generic types.
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

	}
}
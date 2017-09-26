namespace Walker.Utility {
	using System;

	public static class TypeExtensions {

		public static Type GetRoot(this Type t) {
			if (t == null) { throw new ArgumentNullException(); }
			Type bType = t;
			while (bType.BaseType != typeof(object)) {
				bType = bType.BaseType;
			}
			return bType;
		}

		public static Type FindAncestor(this Type t, Predicate<Type> pred) {
			Type bType = t;
			while (bType.BaseType != null) {
				bType = bType.BaseType;
				if (pred(bType)) { return bType; }
			}
			return null;
		}

	}
}
namespace Walker.Data.Vector.Generic {
	using System;
	using System.Linq.Expressions;

	/// <summary>
	/// Allows generic vectors to exist and have operator functions for their generic types.
	/// Taken from a branch of SFML.Net, because they don't have a .NET Core version.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal static class Operator<T> {
		public static T Add(T lhs, T rhs) {
			return AddFunction(lhs, rhs);
		}

		public static T Sub(T lhs, T rhs) {
			return SubtractFunction(lhs, rhs);
		}

		public static T Mult(T lhs, T rhs) {
			return MultiplyFunction(lhs, rhs);
		}

		public static T Pow(T lhs, T rhs) {
			return PowerFunction(lhs, rhs);
		}

		public static T Root(T lhs, T rhs) {
			return PowerFunction(lhs, DivideFunction((T) Convert.ChangeType(1, typeof(T)), rhs)); // wow this is stupid
		}

		public static T Div(T lhs, T rhs) {
			return DivideFunction(lhs, rhs);
		}

		public static T Negate(T arg) {
			return NegateFunction(arg);
		}

		public static bool LessThan(T lhs, T rhs) {
			return LessThanFunction(lhs, rhs);
		}

		public static bool GreaterThan(T lhs, T rhs) {
			return GreaterThanFunction(lhs, rhs);
		}

		public static bool LessThanOrEqual(T lhs, T rhs) {
			return LessThanOrEqualFunction(lhs, rhs);
		}

		public static bool GreaterThanOrEqual(T lhs, T rhs) {
			return GreaterThanOrEqualFunction(lhs, rhs);
		}

		public static bool Equal(T lhs, T rhs) {
			return EqualFunction(lhs, rhs);
		}

		static readonly Func<T, T, T> AddFunction;
		static readonly Func<T, T, T> SubtractFunction;
		static readonly Func<T, T, T> MultiplyFunction;
		static readonly Func<T, T, T> DivideFunction;
		static readonly Func<T, T, T> PowerFunction;

		static readonly Func<T, T> NegateFunction;

		static readonly Func<T, T, bool> LessThanFunction;
		static readonly Func<T, T, bool> GreaterThanFunction;
		static readonly Func<T, T, bool> LessThanOrEqualFunction;
		static readonly Func<T, T, bool> GreaterThanOrEqualFunction;

		static readonly Func<T, T, bool> EqualFunction;

		static Operator() {
			AddFunction = ExpressionUtility.CreateExpression<T, T, T>(Expression.Add);
			SubtractFunction = ExpressionUtility.CreateExpression<T, T, T>(Expression.Subtract);
			MultiplyFunction = ExpressionUtility.CreateExpression<T, T, T>(Expression.Multiply);
			DivideFunction = ExpressionUtility.CreateExpression<T, T, T>(Expression.Divide);
			PowerFunction = ExpressionUtility.CreateExpression<T, T, T>(Expression.Power);

			NegateFunction = ExpressionUtility.CreateExpression<T, T>(Expression.Negate);

			LessThanFunction = ExpressionUtility.CreateExpression<T, T, bool>(Expression.LessThan);
			GreaterThanFunction = ExpressionUtility.CreateExpression<T, T, bool>(Expression.GreaterThan);
			LessThanOrEqualFunction = ExpressionUtility.CreateExpression<T, T, bool>(Expression.LessThanOrEqual);
			GreaterThanOrEqualFunction = ExpressionUtility.CreateExpression<T, T, bool>(Expression.GreaterThanOrEqual);

			EqualFunction = ExpressionUtility.CreateExpression<T, T, bool>(Expression.Equal);
		}
	}
}
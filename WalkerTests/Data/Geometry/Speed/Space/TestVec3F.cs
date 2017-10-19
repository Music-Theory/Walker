namespace Walker.Tests.Data.Geometry.Speed.Space {
	using Walker.Data.Geometry.Speed.Space;
	using Xunit;

	public class TestVec3F {

		[Fact]
		public void Constructor() {
			Vector3F zero = new Vector3F();
			Assert.Equal(0, zero.x);
			Assert.Equal(0, zero.y);
			Assert.Equal(0, zero.z);
			Vector3F var = new Vector3F(1.4f, 2.3f, 3.6f);
			Assert.Equal(1.4f, var.x);
			Assert.Equal(2.3f, var.y);
			Assert.Equal(3.6f, var.z);
		}

		[Fact]
		public void Length() {
			Vector3F test = new Vector3F(2, 2, 2);
			Assert.Equal(test.Length, 3.4641f);
			test.Length = 1;
			Assert.Equal(.5773f, test.x);
			Assert.Equal(.5773f, test.y);
			Assert.Equal(.5773f, test.z);
		}

		[Fact]
		public void Dot() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			float dot = a.Dot(b);
			Assert.Equal(198, dot);
		}

		[Fact]
		public void Cross() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			Vector3F c = a.Cross(b);
			Vector3F d = b.Cross(a);
			Assert.Equal(-3, c.x);
			Assert.Equal( 6, c.y);
			Assert.Equal(-3, c.z);
			Assert.Equal( 3, d.x);
			Assert.Equal(-6, d.y);
			Assert.Equal( 3, d.z);
		}

		[Fact]
		public void Negate() {
			Vector3F a = new Vector3F(1, 2, 3);
			Assert.Equal(-1, -a.x);
			Assert.Equal(-2, -a.y);
			Assert.Equal(-3, -a.z);
		}

		[Fact]
		public void Subtract() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			Vector3F c = a - b;
			Vector3F d = b - a;
			Assert.Equal(-3, c.x);
			Assert.Equal(-3, c.y);
			Assert.Equal(-3, c.z);
			Assert.Equal(3, d.x);
			Assert.Equal(3, d.y);
			Assert.Equal(3, d.z);
		}

		[Fact]
		public void Add() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			Vector3F c = a + b;
			Vector3F d = b + a;
			Assert.Equal(5, c.x);
			Assert.Equal(7, c.y);
			Assert.Equal(9, c.z);
			Assert.Equal(c, d);
		}

		[Fact]
		public void Multiply() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			Vector3F c = a * b;
			Vector3F d = b * a;
			Assert.Equal(4, c.x);
			Assert.Equal(10, c.y);
			Assert.Equal(18, c.z);
			Assert.Equal(c, d);
		}

		[Fact]
		public void MultiplyScalar() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = a * 5;
			Vector3F c = 5 * a;
			Assert.Equal(5, b.x);
			Assert.Equal(10, b.y);
			Assert.Equal(15, b.z);
			Assert.Equal(b, c);
		}

		[Fact]
		public void Divide() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			Vector3F c = a / b;
			Vector3F d = b / a;
			Assert.Equal(.25f, c.x);
			Assert.Equal(.4f, c.y);
			Assert.Equal(.5f, c.z);
			Assert.Equal(c, 1f/d);
		}

		[Fact]
		public void DivideScalar() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			Vector3F c = a / b;
			Vector3F d = b / a;
		}

	}
}
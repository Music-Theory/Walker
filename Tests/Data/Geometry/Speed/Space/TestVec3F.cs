namespace Walker.Tests.Data.Geometry.Speed.Space {
	using Walker.Data.Geometry.Speed.Space;
	using Xunit;

	public class TestVec3F {

		[Fact]
		public void Constructor() {
			Vector3F zero = new Vector3F();
			Assert.Equal(zero.x, 0);
			Assert.Equal(zero.y, 0);
			Assert.Equal(zero.z, 0);
			Vector3F var = new Vector3F(1.4f, 2.3f, 3.6f);
			Assert.Equal(var.x, 1.4f);
			Assert.Equal(var.y, 2.3f);
			Assert.Equal(var.z, 3.6f);
		}

		[Fact]
		public void Length() {
			Vector3F test = new Vector3F(2, 2, 2);
			Assert.Equal(test.Length, 3.4641f);
			test.Length = 1;
			Assert.Equal(test.x, .5773f);
			Assert.Equal(test.y, .5773f);
			Assert.Equal(test.z, .5773f);
		}

		[Fact]
		public void Dot() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			float dot = a.Dot(b);
			Assert.Equal(dot, 198);
		}

		[Fact]
		public void Cross() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			Vector3F c = a.Cross(b);
			Vector3F d = b.Cross(a);
			Assert.Equal(c.x, -3);
			Assert.Equal(c.y, 6);
			Assert.Equal(c.z, -3);
			Assert.Equal(d.x, 3);
			Assert.Equal(d.y, -6);
			Assert.Equal(d.z, 3);
		}

		[Fact]
		public void Negate() {
			Vector3F a = new Vector3F(1, 2, 3);
			Assert.Equal(-a.x, -1);
			Assert.Equal(-a.y, -2);
			Assert.Equal(-a.z, -3);
		}

		[Fact]
		public void Subtract() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			Vector3F c = a - b;
			Vector3F d = b - a;
			Assert.Equal(c.x, -3);
			Assert.Equal(c.y, -3);
			Assert.Equal(c.z, -3);
			Assert.Equal(d.x, 3);
			Assert.Equal(d.y, 3);
			Assert.Equal(d.z, 3);
		}

		[Fact]
		public void Add() {
			Vector3F a = new Vector3F(1, 2, 3);
			Vector3F b = new Vector3F(4, 5, 6);
			Vector3F c = a + b;
			Vector3F d = b + a;
			Assert.Equal();
		}

	}
}
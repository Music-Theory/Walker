namespace Walker.Data.Geometry {
	using System.Diagnostics;

	public static class GeoMeta {

		public static readonly TraceSwitch GeoSwitch = new TraceSwitch("GeoSwitch_SpeedSpace", "TraceSwitch for Walker.Data.Geometry.Speed.Space");

		public const float Tolerance = 0.0001f;

	}
}
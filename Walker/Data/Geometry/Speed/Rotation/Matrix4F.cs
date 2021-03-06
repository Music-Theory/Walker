﻿namespace Walker.Data.Geometry.Speed.Rotation {
    using System;
    using System.Linq;
    using Space;

    /// <summary>
    /// Row-major 4x4 matrix, stored in a float[16].
    /// </summary>
    public struct Matrix4F {
        public static readonly Matrix4F Identity = new Matrix4F(1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        public static readonly Matrix4F Zero = new Matrix4F(0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0);

        // If you want to make the calculations lazy, use a bool[4,4]
        // and stuff like HasChanged, RowChanged(int), ColumnChanged(int)

        float[] values;

        public float[] Values => values;

        public float this[int i, int j] {
            get => values[i + j * 4];
            set => values[i + j * 4] = value;
        }

        #region Rows & Columns

        public Vector4F R1 {
            get => new Vector4F(this[0, 0], this[1, 0], this[2, 0], this[3, 0]);
            set {
                this[0, 0] = value.x;
                this[1, 0] = value.y;
                this[2, 0] = value.z;
                this[3, 0] = value.w;
            }
        }

        public Vector4F R2 {
            get => new Vector4F(this[0, 1], this[1, 1], this[2, 1], this[3, 1]);
            set {
                this[0, 1] = value.x;
                this[1, 1] = value.y;
                this[2, 1] = value.z;
                this[3, 1] = value.w;
            }
        }

        public Vector4F R3 {
            get => new Vector4F(this[0, 2], this[1, 2], this[2, 2], this[3, 2]);
            set {
                this[0, 2] = value.x;
                this[1, 2] = value.y;
                this[2, 2] = value.z;
                this[3, 2] = value.w;
            }
        }

        public Vector4F R4 {
            get => new Vector4F(this[0, 3], this[1, 3], this[2, 3], this[3, 3]);
            set {
                this[0, 3] = value.x;
                this[1, 3] = value.y;
                this[2, 3] = value.z;
                this[3, 3] = value.w;
            }
        }

        public Vector4F C1 {
            get => new Vector4F(this[0, 0], this[0, 1], this[0, 2], this[0, 3]);
            set {
                this[0, 0] = value.x;
                this[0, 1] = value.y;
                this[0, 2] = value.z;
                this[0, 3] = value.w;
            }
        }

        public Vector4F C2 {
            get => new Vector4F(this[1, 0], this[1, 1], this[1, 2], this[1, 3]);
            set {
                this[1, 0] = value.x;
                this[1, 1] = value.y;
                this[1, 2] = value.z;
                this[1, 3] = value.w;
            }
        }

        public Vector4F C3 {
            get => new Vector4F(this[2, 0], this[2, 1], this[2, 2], this[2, 3]);
            set {
                this[2, 0] = value.x;
                this[2, 1] = value.y;
                this[2, 2] = value.z;
                this[2, 3] = value.w;
            }
        }

        public Vector4F C4 {
            get => new Vector4F(this[3, 0], this[3, 1], this[3, 2], this[3, 3]);
            set {
                this[3, 0] = value.x;
                this[3, 1] = value.y;
                this[3, 2] = value.z;
                this[3, 3] = value.w;
            }
        }

        #endregion

        public Matrix4F Transpose {
            get => new Matrix4F(
                this[0, 0], this[0, 1], this[0, 2], this[0, 3],
                this[1, 0], this[1, 1], this[1, 2], this[1, 3],
                this[2, 0], this[2, 1], this[2, 2], this[2, 3],
                this[3, 0], this[3, 1], this[3, 2], this[3, 3]
            );
            set => values = value.Transpose.Values;
        }

        public float Determinant {
            get {
                float a = new Matrix3F(this[1, 1], this[2, 1], this[3, 1],
                    this[1, 2], this[2, 2], this[3, 2],
                    this[1, 3], this[2, 3], this[3, 3]
                ).Determinant;
                float b = new Matrix3F(this[0, 1], this[2, 1], this[3, 1],
                    this[0, 2], this[2, 2], this[3, 2],
                    this[0, 3], this[2, 3], this[3, 3]
                ).Determinant;
                float c = new Matrix3F(this[0, 1], this[1, 1], this[3, 1],
                    this[0, 2], this[1, 2], this[3, 2],
                    this[0, 3], this[1, 3], this[3, 3]
                ).Determinant;
                float d = new Matrix3F(this[0, 1], this[1, 1], this[2, 1],
                    this[0, 2], this[1, 2], this[2, 2],
                    this[0, 3], this[1, 3], this[2, 3]
                ).Determinant;
                return this[0, 0] * a - this[1, 0] * b + this[2, 0] * c - this[3, 0] * d;
            }
        }

        public Matrix4F Inverse {
            get {
                if (Math.Abs(Determinant) < GeoMeta.Tolerance) { throw new MatrixInversionException("Inverse does not exist"); }

                throw new NotImplementedException();
            }
            set => values = value.Inverse.Values;
        }

        public Matrix4F(float[,] values) {
            if (values.GetLength(0) != 4 || values.GetLength(1) != 4) { throw new ArgumentException("Must supply 4x4 array to 4x4 matrix constructor"); }

            this.values = new float[16];

            for (int i = 0; i < values.GetLength(0); i++) {
                for (int j = 0; j < values.GetLength(1); j++) { this[i, j] = values[i, j]; }
            }
        }

        public Matrix4F(float[][] values) {
            if (values.Length != 4) { throw new ArgumentException("Must supply 4x4 array to 4x4 matrix constructor"); }

            for (int i = 0; i < 4; i++) {
                if (values[i].Length != 4) { throw new ArgumentException("Must supply 4x4 array to 4x4 matrix constructor"); }
            }

            this.values = new float[16];
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) { this[i, j] = values[i][j]; }
            }
        }

        public Matrix4F(params float[] values) {
            if (values.Length > 16) { throw new ArgumentException("Can't have more than 16 values in a 4x4 matrix"); }

            this.values = values;
        }

        public Matrix4F(Vector4F g1, Vector4F g2, Vector4F g3, Vector4F g4, bool columns = false) {
            values = new float[16];
            if (columns) {
                C1 = g1;
                C2 = g2;
                C3 = g3;
                C4 = g4;
            } else {
                R1 = g1;
                R2 = g2;
                R3 = g3;
                R4 = g4;
            }
        }

        public Matrix4F(float val)
            : this(val, val, val, val,
                   val, val, val, val,
                   val, val, val, val,
                   val, val, val, val) { }

        public static Matrix4F operator -(Matrix4F a) => a * -1;

        public static Matrix4F operator +(Matrix4F a, Matrix4F b) => new Matrix4F(a.R1 + b.R1, a.R2 + b.R2, a.R3 + b.R3, a.R4 + b.R4);

        public static Matrix4F operator -(Matrix4F a, Matrix4F b) => new Matrix4F(a.R1 - b.R1, a.R2 - b.R2, a.R3 - b.R3, a.R1 - b.R1);

        public static Matrix4F operator *(Matrix4F a, Matrix4F b) => new Matrix4F(new[,] {
            {a.R1.Dot(b.C1), a.R1.Dot(b.C2), a.R1.Dot(b.C3), a.R1.Dot(b.C4)},
            {a.R2.Dot(b.C1), a.R2.Dot(b.C2), a.R2.Dot(b.C3), a.R2.Dot(b.C4)},
            {a.R3.Dot(b.C1), a.R3.Dot(b.C2), a.R3.Dot(b.C3), a.R3.Dot(b.C4)},
            {a.R4.Dot(b.C1), a.R4.Dot(b.C2), a.R4.Dot(b.C3), a.R4.Dot(b.C4)}
        });

        public static Vector4F operator *(Matrix4F a, Vector4F b) => new Vector4F(a.R1.Dot(b), a.R2.Dot(b), a.R3.Dot(b), a.R4.Dot(b));

        public static Vector4F operator *(Vector4F a, Matrix4F b) => new Vector4F(a.Dot(b.R1), a.Dot(b.R2), a.Dot(b.R3), a.Dot(b.R4));

        public static Matrix4F operator *(Matrix4F a, float b) => new Matrix4F(a.R1 * b, a.R2 * b, a.R3 * b, a.R4 * b);

        public static Matrix4F operator *(float a, Matrix4F b) => new Matrix4F(b.R1 * a, b.R2 * a, b.R3 * a, b.R4 * a);

        public static Matrix4F operator /(Matrix4F a, Matrix4F b) => a * b.Inverse;

        public static Matrix4F CreateTranslation(Vector3F pos) => new Matrix4F(1, 0, 0, pos.x,
            0, 1, 0, pos.y,
            0, 0, 1, pos.z,
            0, 0, 0,     1);

        public static Matrix4F CreateScaling(Vector3F sca) => new Matrix4F(
            sca.x, 0, 0, 0,
            0, sca.y, 0, 0,
            0,     0, 0, 1);

        public static Matrix4F LookAt(Vector3F camPos, Vector3F target, Vector3F up) {
            Vector3F z = (camPos - target).Normal;
            Vector3F x = up.Cross(z).Normal;
            Vector3F y = z.Cross(x);

            Matrix4F o = new Matrix4F(
                x.x, x.y, x.z, 0,
                y.x, y.y, y.z, 0,
                z.x, z.y, z.z, 0,
                0,     0,   0, 1);

            return o * CreateTranslation(-camPos);
        }

        /// <summary>
        /// Makes a projection matrix.
        /// http://www.songho.ca/opengl/gl_projectionmatrix.html
        /// </summary>
        /// <param name="vFOV">Vertical FOV</param>
        /// <param name="aR">Aspect Ratio</param>
        /// <param name="near">Near Clipping Plane</param>
        /// <param name="far">Far Clipping Plane</param>
        /// <returns>A projection matrix</returns>
        public static Matrix4F CreatePerspectiveFieldOfView(float vFOV, float aR, float near, float far) {
            float uh = (float) (1 / Math.Tan(vFOV / 2));
            float uw = uh / aR;
            float depth = far - near;
            return new Matrix4F(
                uw, 0,                   0, 0,
                0, uh,                   0, 0,
                0,  0,         far / depth, 1,
                0,  0, -far * near / depth, 0);
        }
    }
}
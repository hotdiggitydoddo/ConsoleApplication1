﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
   [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2 : IEquatable<Vector2>
    {
        #region Private Fields

        private static Vector2 zeroVector = new Vector2(0f, 0f);
        private static Vector2 unitVector = new Vector2(1f, 1f);
        private static Vector2 unitXVector = new Vector2(1f, 0f);
        private static Vector2 unitYVector = new Vector2(0f, 1f);

        #endregion Private Fields


        #region Public Fields

        public float X;
        public float Y;

        #endregion Public Fields


        #region Properties

        public static Vector2 Zero
        {
            get { return zeroVector; }
        }

        public static Vector2 One
        {
            get { return unitVector; }
        }

        public static Vector2 UnitX
        {
            get { return unitXVector; }
        }

        public static Vector2 UnitY
        {
            get { return unitYVector; }
        }

        #endregion Properties


        #region Constructors

                /// <summary>
                /// Constructor foe standard 2D vector.
                /// </summary>
                /// <param name="x">
                /// A <see cref="System.Single"/>
                /// </param>
                /// <param name="y">
                /// A <see cref="System.Single"/>
                /// </param>
        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

                /// <summary>
                /// Constructor for "square" vector.
                /// </summary>
                /// <param name="value">
                /// A <see cref="System.Single"/>
                /// </param>
        public Vector2(float value)
        {
            this.X = value;
            this.Y = value;
        }

        #endregion Constructors


        #region Public Methods

        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
                        float dot = Dot(vector, normal);
                        result.X = vector.X - ((2f * dot) * normal.X);
                        result.Y = vector.Y - ((2f * dot) * normal.Y);
        }

        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            Vector2 result;
                        Reflect(ref vector, ref normal, out result);
                        return result;
        }

        public static Vector2 Add(Vector2 value1, Vector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        public static Vector2 Barycentric(Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2)
        {
            return new Vector2(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
        }

        public static void Barycentric(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, float amount1, float amount2, out Vector2 result)
        {
            result = new Vector2(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
        }

        public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount)
        {
            return new Vector2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result)
        {
            result = new Vector2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
        {
            return new Vector2(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y));
        }

        public static void Clamp(ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
        {
            result = new Vector2(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y));
        }

                /// <summary>
                /// Returns float precison distanve between two vectors
                /// </summary>
                /// <param name="value1">
                /// A <see cref="Vector2"/>
                /// </param>
                /// <param name="value2">
                /// A <see cref="Vector2"/>
                /// </param>
                /// <returns>
                /// A <see cref="System.Single"/>
                /// </returns>
        public static float Distance(Vector2 value1, Vector2 value2)
        {
            return (float)Math.Sqrt((value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y));
        }

                
        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = (float)Math.Sqrt((value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y));
        }

        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            return (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y);
        }

        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y);
        }

                /// <summary>
                /// Devide first vector with the secund vector
                /// </summary>
                /// <param name="value1">
                /// A <see cref="Vector2"/>
                /// </param>
                /// <param name="value2">
                /// A <see cref="Vector2"/>
                /// </param>
                /// <returns>
                /// A <see cref="Vector2"/>
                /// </returns>
        public static Vector2 Divide(Vector2 value1, Vector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        public static Vector2 Divide(Vector2 value1, float divider)
        {
            float factor = 1.0f / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
        {
            float factor = 1.0f / divider;
            result.X = value1.X * factor;
            result.Y = value1.Y * factor;
        }

        public static float Dot(Vector2 value1, Vector2 value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y;
        }

        public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = value1.X * value2.X + value1.Y * value2.Y;
        }

        public override bool Equals(object obj)
        {
            return (obj is Vector2) ? this == ((Vector2)obj) : false;
        }

        public bool Equals(Vector2 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)(this.X + this.Y);
        }

        public static Vector2 Hermite(Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount)
        {
            value1.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            value1.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
            return value1;
        }

        public static void Hermite(ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, float amount, out Vector2 result)
        {
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
        }

        public float Length()
        {
            return (float)Math.Sqrt((double)(X * X + Y * Y));
        }

        public float LengthSquared()
        {
            return X * X + Y * Y;
        }

        public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
        {
            return new Vector2(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount));
        }

        public static void Lerp(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            result = new Vector2(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount));
        }

        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            return new Vector2(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y));
        }

        public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = new Vector2(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y));
        }

        public static Vector2 Min(Vector2 value1, Vector2 value2)
        {
            return new Vector2(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y));
        }

        public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = new Vector2(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y));
        }

        public static Vector2 Multiply(Vector2 value1, Vector2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }

        public static Vector2 Multiply(Vector2 value1, float scaleFactor)
        {
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            return value1;
        }

        public static void Multiply(ref Vector2 value1, float scaleFactor, out Vector2 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
        }

        public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        public static Vector2 Negate(Vector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        public static void Negate(ref Vector2 value, out Vector2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        public void Normalize()
        {
            float factor = 1f / (float)Math.Sqrt((double)(X * X + Y * Y));
            X *= factor;
            Y *= factor;
        }

        public static Vector2 Normalize(Vector2 value)
        {
            float factor = 1f / (float)Math.Sqrt((double)(value.X * value.X + value.Y * value.Y));
            value.X *= factor;
            value.Y *= factor;
            return value;
        }

        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            float factor = 1f / (float)Math.Sqrt((double)(value.X * value.X + value.Y * value.Y));
            result.X = value.X * factor;
            result.Y = value.Y * factor;
        }

        public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
        {
            return new Vector2(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount));
        }

        public static void SmoothStep(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            result = new Vector2(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount));
        }

        public static Vector2 Subtract(Vector2 value1, Vector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(24);
            sb.Append("{X:");
            sb.Append(this.X);
            sb.Append(" Y:");
            sb.Append(this.Y);
            sb.Append("}");
            return sb.ToString();
        }

        #endregion Public Methods


        #region Operators

        public static Vector2 operator -(Vector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }


        public static bool operator ==(Vector2 value1, Vector2 value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }


        public static bool operator !=(Vector2 value1, Vector2 value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }


        public static Vector2 operator +(Vector2 value1, Vector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }


        public static Vector2 operator -(Vector2 value1, Vector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }


        public static Vector2 operator *(Vector2 value1, Vector2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }


        public static Vector2 operator *(Vector2 value, float scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }


        public static Vector2 operator *(float scaleFactor, Vector2 value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }


        public static Vector2 operator /(Vector2 value1, Vector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }


        public static Vector2 operator /(Vector2 value1, float divider)
        {
            float factor = 1 / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        #endregion Operators
    }
}

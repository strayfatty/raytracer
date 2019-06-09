using System;

namespace Tray.Core
{
    public struct Color : IEquatable<Color>
    {
        public float R;
        public float G;
        public float B;

        public Color(float r, float g, float b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public static Color Black => new Color(0.0f, 0.0f, 0.0f);
        public static Color White => new Color(1.0f, 1.0f, 1.0f);

        public static Color CopyFrom(float[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (index < 0 || (array.Length - index) < 3)
                throw new ArgumentOutOfRangeException(nameof(index));

            return new Color(
                array[index],
                array[index + 1],
                array[index + 2]);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Color))
                return false;

            return this.Equals((Color)obj);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + this.R.GetHashCode();
            hash = hash * 23 + this.G.GetHashCode();
            hash = hash * 23 + this.B.GetHashCode();
            return hash;
        }

        public bool Equals(Color other)
        {
            return this.R == other.R
                && this.G == other.G
                && this.B == other.B;
        }

        public void CopyTo(float[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (index < 0 || (array.Length - index) < 3)
                throw new ArgumentOutOfRangeException(nameof(index));

            array[index] = this.R;
            array[index + 1] = this.G;
            array[index + 2] = this.B;
        }

        public static bool operator ==(Color left, Color right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }

        public static Color operator +(Color left, Color right)
        {
            return new Color(
                left.R + right.R,
                left.G + right.G,
                left.B + right.B);
        }

        public static Color operator -(Color left, Color right)
        {
            return new Color(
                left.R - right.R,
                left.G - right.G,
                left.B - right.B);
        }

        public static Color operator *(Color left, float right)
        {
            return new Color(
                left.R * right,
                left.G * right,
                left.B * right);
        }

        public static Color operator *(float left, Color right)
        {
            return right * left;
        }

        public static Color operator *(Color left, Color right)
        {
            return new Color(
                left.R * right.R,
                left.G * right.G,
                left.B * right.B);
        }
    }
}

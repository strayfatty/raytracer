using System;
using System.Collections.Generic;

using FluentAssertions;

using Xunit;

namespace Tray.Core.Tests
{
    public sealed class ColorTests
    {
        [Fact]
        public void Constructor()
        {
            var color = new Color(1, 2, 3);

            color.R.Should().Be(1);
            color.G.Should().Be(2);
            color.B.Should().Be(3);
        }

        [Fact]
        public void Black()
        {
            Color.Black.R.Should().Be(0);
            Color.Black.G.Should().Be(0);
            Color.Black.B.Should().Be(0);
        }

        [Fact]
        public void White()
        {
            Color.White.R.Should().Be(1);
            Color.White.G.Should().Be(1);
            Color.White.B.Should().Be(1);
        }


        [Fact]
        public void CopyFrom_ArrayIsNull_ThrowsArgumentNullException()
        {
            Action action = () => Color.CopyFrom(null, 0);

            action.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("array");
        }

        [Fact]
        public void CopyFrom_IndexIsNegative_ThrowsArgumentOutOfRangeException()
        {
            var array = new float[3];
            Action action = () => Color.CopyFrom(array, -1);

            action.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("index");
        }

        [Fact]
        public void CopyFrom_IndexToLarge_ThrowsArgumentOutOfRangeException()
        {
            var array = new float[4];
            Action action = () => Color.CopyFrom(array, 2);

            action.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("index");
        }

        [Fact]
        public void CopyFrom()
        {
            var array = new float[] { 0.0f, 1.0f, 2.0f, 3.0f };

            var color = Color.CopyFrom(array, 1);

            color.R.Should().Be(1);
            color.G.Should().Be(2);
            color.B.Should().Be(3);
        }



        [Fact]
        public void Equals_Null_IsFalse()
        {
            var actual = Color.Black.Equals(null);

            actual.Should().BeFalse();
        }

        [Fact]
        public void Equals_Integer_IsFalse()
        {
            var actual = Color.Black.Equals((object)1);

            actual.Should().BeFalse();
        }

        [Fact]
        public void Equals_SameColorAsObject_IsTrue()
        {
            var actual = Color.Black.Equals(Color.Black);

            actual.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(EqualsData))]
        public void Equals_Color(
            float[] a,
            float[] b,
            bool expected)
        {
            var left = Color.CopyFrom(a, 0);
            var right = Color.CopyFrom(b, 0);

            var actual = left == right;

            actual.Should().Be(expected);
        }

        [Fact]
        public void CopyTo_ArrayIsNull_ThrowsArgumentNullException()
        {
            Action action = () => Color.White.CopyTo(null, 0);

            action.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("array");
        }

        [Fact]
        public void CopyTo_IndexIsNegative_ThrowsArgumentOutOfRangeException()
        {
            var array = new float[3];
            Action action = () => Color.White.CopyTo(array, -1);

            action.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("index");
        }

        [Fact]
        public void CopyTo_IndexToLarge_ThrowsArgumentOutOfRangeException()
        {
            var array = new float[4];
            Action action = () => Color.White.CopyTo(array, 2);

            action.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("index");
        }

        [Fact]
        public void CopyTo()
        {
            var array = new float[4];
            var color = new Color(1, 2, 3);

            color.CopyTo(array, 1);

            array[1].Should().Be(1);
            array[2].Should().Be(2);
            array[3].Should().Be(3);
        }

        [Theory]
        [MemberData(nameof(EqualsData))]
        public void OperatorEquals(
            float[] a,
            float[] b,
            bool expected)
        {
            var left = Color.CopyFrom(a, 0);
            var right = Color.CopyFrom(b, 0);

            var actual = left == right;

            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(EqualsData))]
        public void OperatorNotEquals(
            float[] a,
            float[] b,
            bool expected)
        {
            var left = Color.CopyFrom(a, 0);
            var right = Color.CopyFrom(b, 0);

            var actual = left != right;

            actual.Should().Be(!expected);
        }

        [Fact]
        public void OperatorAddition()
        {
            var left = new Color(0.9f, 0.6f, 0.75f);
            var right = new Color(0.7f, 0.1f, 0.25f);

            var actual = left + right;

            actual.Should().Be(new Color(0.9f + 0.7f, 0.6f + 0.1f, 0.75f + 0.25f));
        }

        [Fact]
        public void OperatorSubtraction()
        {
            var left = new Color(0.9f, 0.6f, 0.75f);
            var right = new Color(0.7f, 0.1f, 0.25f);

            var actual = left - right;

            actual.Should().Be(new Color(0.9f - 0.7f, 0.6f - 0.1f, 0.75f - 0.25f));
        }

        [Fact]
        public void OperatorMultiplication_ColorByScalar()
        {
            var left = new Color(0.2f, 0.3f, 0.4f);
            var right = 2.0f;

            var actual = left * right;

            actual.Should().Be(new Color(0.2f * 2.0f, 0.3f * 2.0f, 0.4f * 2.0f));
        }

        [Fact]
        public void OperatorMultiplication_ScalarByColor()
        {
            var left = 2.0f;
            var right = new Color(0.2f, 0.3f, 0.4f);

            var actual = left * right;

            actual.Should().Be(new Color(0.2f * 2.0f, 0.3f * 2.0f, 0.4f * 2.0f));
        }

        [Fact]
        public void OperatorMultiplication_ColorByColor()
        {
            var left = new Color(1.0f, 0.2f, 0.4f);
            var right = new Color(0.9f, 1.0f, 0.1f);

            var actual = left * right;

            actual.Should().Be(new Color(1.0f * 0.9f, 0.2f * 1.0f, 0.4f * 0.1f));
        }

        public static IEnumerable<object[]> EqualsData => new List<object[]>
        {
            new object[]
            {
                new float[] { 1.0f, 2.0f, 3.0f },
                new float[] { 1.0f, 2.0f, 3.0f },
                true
            },
            new object[]
            {
                new float[] { 1.0f, 2.0f, 3.0f },
                new float[] { 0.0f, 2.0f, 3.0f },
                false
            },
            new object[]
            {
                new float[] { 1.0f, 2.0f, 3.0f },
                new float[] { 1.0f, 0.0f, 3.0f },
                false
            },
            new object[]
            {
                new float[] { 1.0f, 2.0f, 3.0f },
                new float[] { 1.0f, 2.0f, 0.0f },
                false
            },
        };
    }
}

using System.Collections.Generic;
using Xunit;
using System;
using EightPuzzleLogic.Extensions;

namespace EightPuzzleLogic.Tests.ExtensionsTests
{
    public class DataStructureExtensionsTests
    {
        public static IEnumerable<object[]> Generate2DimensionArrayAndConvertionTo1Dimension()
        {
            var result = new List<object[]>() {
                new object[] { 
                    new[] { new [] {1, 2}, new [] {4, 6} }, 
                    new[] { 1, 2, 4, 6 } 
                },
                new object[] { 
                    new[] { new [] {9, 11, 28}, new [] {333, 43543, 543223, 222} },
                    new[] { 9, 11, 28, 333, 43543, 543223, 222 }
                },
                new object[] {
                    Array.Empty<int[]>(),
                    Array.Empty<int>()
                }
            };

            return result;
        }

        [Theory]
        [MemberData(nameof(Generate2DimensionArrayAndConvertionTo1Dimension))]
        public void To1Dimension_Converts2DimensionArrayCorrectly(int[][] array2Dimension, int[] expected)
        {
            var actual = array2Dimension.To1Dimension();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void To1Dimension_ThrowsArgumentNullException()
        {
            int[][] array2Dimension = null;

            Assert.Throws<ArgumentNullException>(() => array2Dimension.To1Dimension());
        }
    }
}
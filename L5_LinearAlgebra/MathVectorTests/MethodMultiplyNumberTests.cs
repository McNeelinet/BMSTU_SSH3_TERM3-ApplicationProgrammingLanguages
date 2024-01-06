using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class MethodMultiplyNumberTests
{
    [TestMethod]
    public void Test_Number_ReturnsCorrectVector()
    {
        // arrange
        const int expectedDimensions = 2;
        const double i0Initial = 3;
        const double i1Initial = 4;
        const double num = 2;
        const double i0Expected = 6;
        const double i1Expected = 8;
        
        var vector = new MathVector(expectedDimensions)
        {
            [0] = i0Initial,
            [1] = i1Initial
        };

        // act;
        var vector2 = vector.MultiplyNumber(num);
        
        // assert
        Assert.AreEqual(vector2.Dimensions, expectedDimensions);
        Assert.AreEqual(vector2[0], i0Expected);
        Assert.AreEqual(vector2[1], i1Expected);
    }
}
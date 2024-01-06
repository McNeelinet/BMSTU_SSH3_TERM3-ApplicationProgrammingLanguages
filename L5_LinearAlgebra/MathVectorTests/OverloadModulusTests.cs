using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class OverloadModulusTests
{
    [TestMethod]
    public void Test_Vector_ReturnsCorrectVector()
    {
        // arrange
        const int expectedDimensions = 2;
        const double v1i0Initial = 3;
        const double v1i1Initial = 4;
        const double v2i0Initial = 5;
        const double v2i1Initial = 6;
        const double expectedValue = 39;
        
        var vector1 = new MathVector(expectedDimensions)
        {
            [0] = v1i0Initial,
            [1] = v1i1Initial
        };
        
        var vector2 = new MathVector(expectedDimensions)
        {
            [0] = v2i0Initial,
            [1] = v2i1Initial
        };

        // act;
        var actualValue = vector1 % vector2;
        
        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }
}
using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class MethodSumTests
{
    [TestMethod]
    public void Test_CorrectVectorDimensions_ReturnsCorrectVector()
    {
        // arrange
        const int expectedDimensions = 2;
        const double v1i0Initial = 3;
        const double v1i1Initial = 4;
        const double v2i0Initial = 5;
        const double v2i1Initial = 6;
        const double i0Expected = 8;
        const double i1Expected = 10;
        
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
        var vector3 = vector1.Sum(vector2);
        
        // assert
        Assert.AreEqual(vector3.Dimensions, expectedDimensions);
        Assert.AreEqual(vector3[0], i0Expected);
        Assert.AreEqual(vector3[1], i1Expected);
    }
    
    [TestMethod]
    public void Test_IncorrectVectorDimensions_ReturnsCorrectVector()
    {
        // arrange
        ArgumentException? expectedException = null;
        const int dimensions1 = 2;
        const int dimensions2 = 4;

        var vector1 = new MathVector(dimensions1);
        var vector2 = new MathVector(dimensions2);

        // act;
        try
        {
            var unused = vector1.Sum(vector2);
        }
        catch (ArgumentException ex)
        {
            expectedException = ex;
        }
        
        // assert
        Assert.IsNotNull(expectedException);
    }
}
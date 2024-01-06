using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class MethodDivideTests
{
    [TestMethod]
    public void Test_CorrectVectorDimensions_ReturnsCorrectVector()
    {
        // arrange
        const int expectedDimensions = 2;
        const double v1i0Initial = 9;
        const double v1i1Initial = 4;
        const double v2i0Initial = 3;
        const double v2i1Initial = 2;
        const double i0Expected = 3;
        const double i1Expected = 2;
        
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
        var vector3 = vector1.Divide(vector2);
        
        // assert
        Assert.AreEqual(vector3.Dimensions, expectedDimensions);
        Assert.AreEqual(vector3[0], i0Expected);
        Assert.AreEqual(vector3[1], i1Expected);
    }
    
    [TestMethod]
    public void Test_CorrectVectorDimensionsContainsZero_ReturnsCorrectVector()
    {
        // arrange
        DivideByZeroException? expectedException = null;
        const int expectedDimensions = 2;
        const double v1i0Initial = 9;
        const double v1i1Initial = 4;
        const double v2i0Initial = 3;
        const double v2i1Initial = 0;
        
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
        try
        {
            var unused = vector1.Divide(vector2);
        }
        catch (DivideByZeroException ex)
        {
            expectedException = ex;
        }
        
        // assert
        Assert.IsNotNull(expectedException);
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
            var unused = vector1.Divide(vector2);
        }
        catch (ArgumentException ex)
        {
            expectedException = ex;
        }
        
        // assert
        Assert.IsNotNull(expectedException);
    }
}
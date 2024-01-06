using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class MethodDivideNumberTests
{
    [TestMethod]
    public void Test_NonZeroNumber_ReturnsCorrectVector()
    {
        // arrange
        const int expectedDimensions = 2;
        const double i0Initial = 6;
        const double i1Initial = 4;
        const double num = 2;
        const double i0Expected = 3;
        const double i1Expected = 2;
        
        var vector = new MathVector(expectedDimensions)
        {
            [0] = i0Initial,
            [1] = i1Initial
        };

        // act;
        var vector2 = vector.DivideNumber(num);
        
        // assert
        Assert.AreEqual(vector2.Dimensions, expectedDimensions);
        Assert.AreEqual(vector2[0], i0Expected);
        Assert.AreEqual(vector2[1], i1Expected);
    }
    
    [TestMethod]
    public void Test_ZeroNumber_ReturnsCorrectVector()
    {
        // arrange
        DivideByZeroException? expectedException = null;
        const int expectedDimensions = 2;
        const double num = 0;

        var vector = new MathVector(expectedDimensions);

        // act;
        try
        {
            var unused = vector.DivideNumber(num);
        }
        catch (DivideByZeroException ex)
        {
            expectedException = ex;
        }
        
        // assert
        Assert.IsNotNull(expectedDimensions);
    }
}
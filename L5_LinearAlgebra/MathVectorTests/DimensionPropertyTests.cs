using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class DimensionPropertyTests
{
    [TestMethod]
    public void Test_Get_ReturnsCorrectDimensions()
    {
        // arrange
        const int expectedDimensions = 5;
        IMathVector vector = new MathVector(expectedDimensions);
        
        // act
        int actualDimensions = vector.Dimensions;
        
        // assert
        Assert.AreEqual(expectedDimensions, actualDimensions);
    }
}
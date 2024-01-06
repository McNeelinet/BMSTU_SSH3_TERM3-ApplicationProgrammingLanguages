using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class MethodToStringTests
{
    [TestMethod]
    public void Test_Number_ReturnsCorrectVector()
    {
        // arrange
        const int expectedDimensions = 2;
        const double i0Initial = 3;
        const double i1Initial = 4;
        const string expectedValue = "3, 4";
        
        var vector = new MathVector(expectedDimensions)
        {
            [0] = i0Initial,
            [1] = i1Initial
        };

        // act;
        var actualValue = vector.ToString();
        
        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }
}
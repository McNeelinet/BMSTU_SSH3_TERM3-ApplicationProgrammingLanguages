using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class LengthPropertyTests
{
    [TestMethod]
    public void Test_Get_ReturnsCorrectLength()
    {
        // arrange
        const int dimensions = 3;
        const double i0 = 3;
        const double i1 = 4;
        const double i2 = 5;
        double expectedValue = Math.Pow( Math.Pow(i0, 2) + Math.Pow(i1, 2) + Math.Pow(i2, 2), 0.5 );
        
        IMathVector vector = new MathVector(dimensions);
        vector[0] = i0;
        vector[1] = i1;
        vector[2] = i2;
        
        // act;
        double actualValue = vector.Length;
        
        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }
}
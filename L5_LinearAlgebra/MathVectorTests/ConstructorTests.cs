using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class ConstructorTests
{
    [TestMethod]
    public void Test_ValidArguments_CreatesVector()
    {
        // Arrange
        const int correctDimensions = 10;
        
        // Act
        MathVector vector = new MathVector(correctDimensions);
    }
    
    [TestMethod]
    public void Test_InvalidArguments_ThrowsException()
    {
        // Arrange
        ArgumentException? expectedException = null;
        const int incorrectDimensions = -1;
        
        // Act
        try
        {
            MathVector vector = new MathVector(incorrectDimensions);
        }
        catch (ArgumentException ex)
        {
            expectedException = ex;
        }
        
        // Assert
        Assert.IsNotNull(expectedException);
    }
}
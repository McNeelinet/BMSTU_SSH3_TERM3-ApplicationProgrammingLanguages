using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MathVectorTests;

[TestClass]
public class IndexerTests
{
    [TestMethod]
    public void Test_SetGetByCorrectIndex_SetsGetsCorrectValue()
    {
        // arrange
        const int dimensions = 5;
        const double expectedValue = 8;
        IMathVector vector = new MathVector(dimensions);
        
        // act
        vector[3] = expectedValue;
        double actualValue = vector[3];
        
        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }
    
    [TestMethod]
    public void Test_GetByIncorrectIndexLower_ThrowsException()
    {
        // arrange
        const int dimensions = 10;
        IndexOutOfRangeException? expectedException = null;
        IMathVector vector = new MathVector(dimensions);
        
        // act
        try
        {
            double someValue = vector[-1];
        }
        catch (IndexOutOfRangeException ex)
        {
            expectedException = ex;
        }
        
        // Assert
        Assert.IsNotNull(expectedException);
    }
    
    [TestMethod]
    public void Test_GetByIncorrectIndexHigher_ThrowsException()
    {
        // arrange
        const int dimensions = 10;
        IndexOutOfRangeException? expectedException = null;
        IMathVector vector = new MathVector(dimensions);
        
        // act
        try
        {
            double someValue = vector[150];
        }
        catch (IndexOutOfRangeException ex)
        {
            expectedException = ex;
        }
        
        // Assert
        Assert.IsNotNull(expectedException);
    }
    
    [TestMethod]
    public void Test_SetByIncorrectIndexLower_ThrowsException()
    {
        // arrange
        const int dimensions = 10;
        double someValue = 5;
        IndexOutOfRangeException? expectedException = null;
        IMathVector vector = new MathVector(dimensions);
        
        // act
        try
        {
            vector[-1] = someValue;
        }
        catch (IndexOutOfRangeException ex)
        {
            expectedException = ex;
        }
        
        // Assert
        Assert.IsNotNull(expectedException);;
    }
    
    [TestMethod]
    public void Test_SetByIncorrectIndexHigher_ThrowsException()
    {
        // arrange
        const int dimensions = 10;
        double someValue = 5;
        IndexOutOfRangeException? expectedException = null;
        IMathVector vector = new MathVector(dimensions);
        
        // act
        try
        {
            vector[150] = someValue;
        }
        catch (IndexOutOfRangeException ex)
        {
            expectedException = ex;
        }
        
        // Assert
        Assert.IsNotNull(expectedException);;
    }
}

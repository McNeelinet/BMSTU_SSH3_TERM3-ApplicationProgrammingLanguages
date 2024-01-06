using LinearAlgebra;


namespace VectorDemo
{
    internal abstract class Program
    {
        private const int VectorDimensions = 5;

        private static void Main()
        { 
            TestLength();
            TestSumNumber();
            TestSubtractNumber();
            TestMultiplyNumber();
            TestDivideNumber();
            TestSum();
            TestSubtract();
            TestMultiply();
            TestDivide();
            TestScalarMultiply();
            TestCalcDistance();
        }

        private static IMathVector CreateSampleVector(double multiplier)
        {
            IMathVector sampleVector = new MathVector(VectorDimensions);
            for (int i = 0; i < sampleVector.Dimensions; ++i)
                sampleVector[i] = (i + 1) * multiplier;

            return sampleVector;
        }

        private static void TestLength()
        {
            IMathVector vector = CreateSampleVector(1.0);
            
            Console.WriteLine("Testing IMathVector.Dimensions:");
            Console.WriteLine("Expected output: 7.416198");
            Console.WriteLine("Output: {0}\n", vector.Length);
        }
        
        private static void TestSumNumber()
        {
            IMathVector vector = CreateSampleVector(1.0);
            
            Console.WriteLine("Testing IMathVector.SumNumber:");
            Console.WriteLine("Expected output: 6, 7, 8, 9, 10");
            Console.WriteLine("Output: {0}", vector.SumNumber(5.0));
            Console.WriteLine("Overload: {0}\n", (MathVector)vector + 5.0);
        }
        
        private static void TestSubtractNumber()
        {
            IMathVector vector = CreateSampleVector(1.0);
            
            Console.WriteLine("Testing IMathVector.SubtractNumber:");
            Console.WriteLine("Expected output: -5, -4, -3, -2, -1");
            Console.WriteLine("Output: {0}", vector.SubtractNumber(6.0));
            Console.WriteLine("Overload: {0}\n", (MathVector)vector - 6.0);
        }
        
        private static void TestMultiplyNumber()
        {
            IMathVector vector = CreateSampleVector(1.0);
            
            Console.WriteLine("Testing IMathVector.MultiplyNumber:");
            Console.WriteLine("Expected output: 10, 20, 30, 40, 50");
            Console.WriteLine("Output: {0}", vector.MultiplyNumber(10.0));
            Console.WriteLine("Overload: {0}\n", (MathVector)vector * 10.0);
        }
        
        private static void TestDivideNumber()
        {
            IMathVector vector = CreateSampleVector(1.0);
            
            Console.WriteLine("Testing IMathVector.DivideNumber:");
            Console.WriteLine("Expected output: 0.1, 0.2, 0.3, 0.4, 0.5");
            Console.WriteLine("Output: {0}", vector.DivideNumber(10.0));
            Console.WriteLine("Overload: {0}\n", (MathVector)vector / 10.0);
        }
        
        private static void TestSum()
        {
            IMathVector vector1 = CreateSampleVector(1.0);
            
            IMathVector vector2 = CreateSampleVector(2.0);
            
            Console.WriteLine("Testing IMathVector.Sum:");
            Console.WriteLine("Expected output: 3, 6, 9, 12, 15");
            Console.WriteLine("Output: {0}", vector1.Sum(vector2));
            Console.WriteLine("Overload: {0}\n", (MathVector)vector1 + vector2);
        }
        
        private static void TestSubtract()
        {
            IMathVector vector1 = CreateSampleVector(1.0);
            
            IMathVector vector2 = CreateSampleVector(2.0);
            
            Console.WriteLine("Testing IMathVector.Subtract:");
            Console.WriteLine("Expected output: -1, -2, -3, -4, -5");
            Console.WriteLine("Output: {0}", vector1.Subtract(vector2));
            Console.WriteLine("Overload: {0}\n", (MathVector)vector1 - vector2);
        }
        
        private static void TestMultiply()
        {
            IMathVector vector1 = CreateSampleVector(1.0);
            
            IMathVector vector2 = CreateSampleVector(2.0);
            
            Console.WriteLine("Testing IMathVector.Multiply:");
            Console.WriteLine("Expected output: 2, 8, 18, 32, 50");
            Console.WriteLine("Output: {0}", vector1.Multiply(vector2));
            Console.WriteLine("Overload: {0}\n", (MathVector)vector1 * vector2);
        }
        
        private static void TestDivide()
        {
            IMathVector vector1 = CreateSampleVector(1.0);
            
            IMathVector vector2 = CreateSampleVector(2.0);
            
            Console.WriteLine("Testing IMathVector.Divide");
            Console.WriteLine("Expected output: 0.5, 0.5, 0.5, 0.5, 0.5");
            Console.WriteLine("Output: {0}", vector1.Divide(vector2));
            Console.WriteLine("Overload: {0}\n", (MathVector)vector1 / vector2);
        }
        
        private static void TestScalarMultiply()
        {
            IMathVector vector1 = CreateSampleVector(1.0);
            
            IMathVector vector2 = CreateSampleVector(2.0);
            
            Console.WriteLine("Testing IMathVector.ScalarMultiply");
            Console.WriteLine("Expected output: 110");
            Console.WriteLine("Output: {0}", vector1.ScalarMultiply(vector2));
            Console.WriteLine("Overload: {0}\n", (MathVector)vector1 % vector2);
        }
        
        private static void TestCalcDistance()
        {
            IMathVector vector1 = CreateSampleVector(1.0);
            
            IMathVector vector2 = CreateSampleVector(2.0);
            
            Console.WriteLine("Testing IMathVector.CalcDistance");
            Console.WriteLine("Expected output: 7.416198");
            Console.WriteLine("Output: {0}", vector1.CalcDistance(vector2));
        }
    }
}

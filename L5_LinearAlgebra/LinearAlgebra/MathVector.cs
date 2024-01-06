using System.Collections;
using System.Net.Mime;

namespace LinearAlgebra;

public class MathVector : IMathVector
{
    private readonly double[] _items;

    private void CheckDimensions(IMathVector vector1, IMathVector vector2)
    {
        if (vector1.Dimensions != vector2.Dimensions)
            throw new ArgumentException("Dimensions of vectors are not equal.");
    }
    
    public MathVector(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentException("Dimensions cannot be less than 1.");
        
        _items = new double[dimensions];
    }

    public int Dimensions
    {
        get
        {
            return _items.Length;
        }
    }

    public double this[int i]
    {
        get
        {
            if (i < 0 || i >= Dimensions)
                throw new IndexOutOfRangeException("Index was out of range. " +
                                                   "Must be non-negative and less than the size of the vector.");
            
            return _items[i];
        }

        set
        {
            if (i < 0 || i >= _items.Length)
                throw new IndexOutOfRangeException("Index was out of range. " +
                                                   "Must be non-negative and less than the size of the vector.");

            _items[i] = value;
        }
    }

    public double Length
    {
        get
        {
            double length = 0;
            
            foreach (double element in _items)
            {
                length += Math.Pow(element, 2.0);
            }

            return Math.Pow(length, 0.5);
        }
    }

    public IMathVector SumNumber(double number)
    {
        IMathVector resVector = new MathVector(Dimensions);
        for (int i = 0; i < Dimensions; ++i)
            resVector[i] = this[i] + number;

        return resVector;
    }

    public IMathVector SubtractNumber(double number)
    {
        IMathVector resVector = new MathVector(Dimensions);
        for (int i = 0; i < Dimensions; ++i)
            resVector[i] = this[i] - number;

        return resVector;
    }

    public IMathVector MultiplyNumber(double number)
    {
        IMathVector resVector = new MathVector(Dimensions);
        for (int i = 0; i < Dimensions; ++i)
            resVector[i] = this[i] * number;

        return resVector;
    }

    public IMathVector DivideNumber(double number)
    {
        if (number == 0)
            throw new DivideByZeroException("Number cannot be 0. Division by 0 is prohibited.");
        
        IMathVector resVector = new MathVector(Dimensions);
        for (int i = 0; i < Dimensions; ++i)
            resVector[i] = this[i] / number;

        return resVector;
    }

    public IMathVector Sum(IMathVector vector)
    {
        CheckDimensions(this, vector);
        
        IMathVector resVector = new MathVector(Dimensions);
        for (int i = 0; i < Dimensions; ++i)
            resVector[i] = this[i] + vector[i];

        return resVector;
    }

    public IMathVector Subtract(IMathVector vector)
    {
        CheckDimensions(this, vector);
        
        IMathVector resVector = new MathVector(Dimensions);
        for (int i = 0; i < Dimensions; ++i)
            resVector[i] = this[i] - vector[i];

        return resVector;
    }

    public IMathVector Multiply(IMathVector vector)
    {
        CheckDimensions(this, vector);
        
        IMathVector resVector = new MathVector(Dimensions);
        for (int i = 0; i < Dimensions; ++i)
            resVector[i] = this[i] * vector[i];

        return resVector;
    }

    public IMathVector Divide(IMathVector vector)
    {
        CheckDimensions(this, vector);
        
        foreach (double element in vector)
            if (element == 0)
                throw new DivideByZeroException("Division by 0 is prohibited. Second vector contains 0.");
        
        IMathVector resVector = new MathVector(Dimensions);
        for (int i = 0; i < Dimensions; ++i)
            resVector[i] = this[i] / vector[i];

        return resVector;
    }

    public double ScalarMultiply(IMathVector vector)
    {
        CheckDimensions(this, vector);

        double result = 0;
        
        for (int i = 0; i < Dimensions; ++i)
            result += this[i] * vector[i];

        return result;
    }
    
    public double CalcDistance(IMathVector vector)
    {
        CheckDimensions(this, vector);

        double result = 0;

        for (int i = 0; i < Dimensions; ++i)
            result += Math.Pow(this[i] - vector[i], 2.0);
        result = Math.Pow(result, 0.5);

        return result;
    }

    public static IMathVector operator +(MathVector vector, double number)
    {
        return vector.SumNumber(number);
    }

    public static IMathVector operator +(MathVector vector1, IMathVector vector2)
    {
        return vector1.Sum(vector2);
    }

    public static IMathVector operator -(MathVector vector, double number)
    {
        return vector.SubtractNumber(number);
    }
    
    public static IMathVector operator -(MathVector vector1, IMathVector vector2)
    {
        return vector1.Subtract(vector2);
    }
    
    public static IMathVector operator *(MathVector vector, double number)
    {
        return vector.MultiplyNumber(number);
    }
    
    public static IMathVector operator *(MathVector vector1, IMathVector vector2)
    {
        return vector1.Multiply(vector2);
    }

    public static IMathVector operator /(MathVector vector, double number)
    {
        return vector.DivideNumber(number);
    }
    
    public static IMathVector operator /(MathVector vector1, IMathVector vector2)
    {
        return vector1.Divide(vector2);
    }
    
    public static double operator %(MathVector vector1, IMathVector vector2)
    {
        return vector1.ScalarMultiply(vector2);
    }

    public override string ToString()
    {
        return string.Join(", ", _items);
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}

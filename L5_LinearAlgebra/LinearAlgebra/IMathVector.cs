using System.Collections;

namespace LinearAlgebra;

public interface IMathVector : IEnumerable
{
    /// <summary>
    /// Получить размерность вектора (количество координат).
    /// </summary>
    int Dimensions { get; }

    /// <summary>
    /// Индексатор для доступа к элементам вектора. Нумерация с нуля.
    /// </summary>
    double this[int i] { get; set; }

    /// <summary>
    /// Рассчитать длину (модуль) вектора.
    /// </summary>
    double Length { get; }

    /// <summary>
    /// Покомпонентное сложение с числом.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    IMathVector SumNumber(double number);

    /// <summary>
    /// Покомпонентное вычитание числа.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    IMathVector SubtractNumber(double number);

    /// <summary>
    /// Покомпонентное умножение на число.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    IMathVector MultiplyNumber(double number);

    /// <summary>
    /// Покомпонентное деление на число.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    IMathVector DivideNumber(double number);

    /// <summary>
    /// Покомпонентное сложение с другим вектором.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    IMathVector Sum(IMathVector vector);

    /// <summary>
    /// Покомпонентное вычитание другого вектора.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    IMathVector Subtract(IMathVector vector);

    /// <summary>
    /// Покомпонентное умножение на другой вектор.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    IMathVector Multiply(IMathVector vector);

    /// <summary>
    /// Покомпонентное деление на другой вектор.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    IMathVector Divide(IMathVector vector);

    /// <summary>
    /// Скалярное умножение на другой вектор.
    /// </summary>
    double ScalarMultiply(IMathVector vector);

    /// <summary>
    /// Вычислить Евклидово расстояние до другого вектора.
    /// </summary>
    double CalcDistance(IMathVector vector);
}
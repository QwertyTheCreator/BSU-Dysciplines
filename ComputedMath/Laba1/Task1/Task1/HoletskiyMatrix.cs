namespace Task1;

public class HoletskiyMatrix
{
    private readonly decimal[] _diag;
    private readonly decimal[][] _sMatrix;
    private readonly List<List<decimal>> _baseMatrix;
    public decimal Determinator { get; } = 1m;

    public HoletskiyMatrix(List<List<decimal>> baseMatrix)
    {
        _baseMatrix = baseMatrix;
        _diag = new decimal[baseMatrix.Count]; //Диагональная матрица D
        _sMatrix = new decimal[baseMatrix.Count][]; //Верхнетреугольная матрица S

        for (int i = 0; i < baseMatrix.Count; i++)
        {
            _sMatrix[i] = new decimal[baseMatrix.Count - i];

            for (int j = 0; j < baseMatrix.Count - i; j++)
            {
                if (j == 0)
                {
                    var sdSum = SdSum(i);

                    _diag[i] = D(i, sdSum);
                    _sMatrix[i][j] = SDiag(i, sdSum);

                    Determinator *= _sMatrix[i][j] * _sMatrix[i][j] * _diag[i]; //Вычисление определителя

                    continue;
                }

                _sMatrix[i][j] = S(i, j);
            }
        }

        Console.WriteLine("Опеределитель A: " + Determinator);

        for (int i = 0; i < baseMatrix.Count; i++)
        {
            Console.Write(string.Concat(Enumerable.Repeat("0 ", i)));
            for (int j = 0; j < baseMatrix.Count - i; j++)
            {
                Console.Write(decimal.Round(_sMatrix[i][j], 2) + " ");
            }

            Console.WriteLine();
        }
    }

    public decimal[] SolveSoLE(decimal[] rightVector) // Решение СЛАУ
    {
        decimal[] yVector = new decimal[_baseMatrix.Count];

        for (int i = 0; i < _baseMatrix.Count; i++)
        {
            yVector[i] = Y(i, rightVector, yVector);
        }

        Console.WriteLine("Y vector: " + string.Concat(yVector.Select(y => y.ToString() + " ")));

        decimal[] xVector = new decimal[_baseMatrix.Count];

        for (int i = _baseMatrix.Count - 1; i >= 0; i--)
        {
            xVector[i] = X(i, yVector, xVector);
        }

        Console.WriteLine("X vector: " + string.Concat(xVector.Select(x => decimal.Round(x, 2).ToString() + " ")));

        return xVector;
    }

    public decimal ResidualNorm(decimal[] answerVector, decimal[] rightVector) //Метод для расчёта нормы(Кубической) невязки
    {
        decimal maxAbs = 0m;

        for (int i = 0; i < _baseMatrix.Count; i++)
        {
            var residualAbs = decimal.Abs(GetEquationResidual(_baseMatrix[i].ToArray(), answerVector, rightVector[i]));

            maxAbs = residualAbs > maxAbs
                ? residualAbs
                : maxAbs;
        }

        return maxAbs;
    }

    private decimal GetEquationResidual(decimal[] equation, decimal[] answers, decimal rightPart)
    {
        var sum = 0m;

        for (int i = 0; i < _baseMatrix.Count; i++)
        {
            sum += equation[i] * answers[i];
        }

        return rightPart - sum;
    }

    private decimal X(int i, decimal[] yVector, decimal[] xVector) // Поиск Xi
    {
        var sum = 0m;

        var iFor = i;
        for (int k = i + 1; k < _baseMatrix.Count; k++)
        {
            sum += _diag[k] * _sMatrix[i][k - i] * xVector[k];
            iFor--;
        }

        return (yVector[i] - sum) / _sMatrix[i][0];
    }

    private decimal Y(int i, decimal[] rightVector, decimal[] yVector) //Поиск Yi
    {
        var sum = 0m;

        var iFor = i;
        for (int k = 0; k < i; k++)
        {
            sum += _sMatrix[k][iFor] * yVector[k];
            iFor--;
        }

        return (rightVector[i] - sum) / _sMatrix[i][0];
    }

    private decimal D(int i, decimal sdSum) =>
         decimal.Sign(_baseMatrix[i][i] - sdSum);

    private decimal SDiag(int i, decimal sdSum) => // Функция для получения Sii элемента
                                                   //Матрицы S
        SqrtFromAbs(_baseMatrix[i][i] - sdSum);

    private decimal S(int i, int j) // Функция для получения Sij, j != i элемента
                                    // из матрицы S
    {
        var iFor = i;
        var jFor = j + i;

        var sum = 0m;

        for (int k = 0; k < i; k++)
        {
            sum += _sMatrix[k][iFor] * _diag[k] * _sMatrix[k][jFor];
            iFor--;
            jFor--;
        }

        return (_baseMatrix[i][j + i] - sum) / (_sMatrix[i][0] * _diag[i]);
    }

    private decimal SdSum(int i) // Функция для подсчёта суммы 
                                 // Из формулы расчёта диагональных элементов матрицы S
    {
        var sum = 0m;

        var iFor = i;
        for (int k = 0; k < i; k++)
        {
            sum += _sMatrix[k][iFor] * _sMatrix[k][iFor] * _diag[k];
            iFor--;
        }

        return sum;
    }

    private decimal SqrtFromAbs(decimal value) => //Урощения операции взятия корня из модуля
        (decimal)Math.Sqrt((double)decimal.Abs(value));
}

namespace Task1;

public class Program
{
    static void Main(string[] args)
    {
        List<List<decimal>> matrixTest = [
            [1m, 2m, 4m],
            [2m, 13m, 23m],
            [4m, 23m, 77m]
        ];

        var holets = new HoletskiyMatrix(matrixTest);

        decimal[] b = [10, 50, 150];
        var answer = holets.SolveSoLE(b);

        var residual = holets.ResidualNorm(answer, b);

        Console.WriteLine("Невязка: " + residual);
    }
}

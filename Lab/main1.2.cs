using System;

class Program
{
    static void Main()
    {
        int n = 10; // Размерность матрицы

        // Создание и заполнение двумерного массива случайными вещественными числами
        double[,] matrix = new double[n, n];
        Random rand = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = rand.NextDouble() * 100; // Заполняем случайными вещественными числами от 0 до 100
            }
        }

        // Выполнение операции сглаживания матрицы
        double[,] smoothedMatrix = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                double sum = 0;
                int count = 0;
                for (int x = Math.Max(0, i - 1); x <= Math.Min(n - 1, i + 1); x++)
                {
                    for (int y = Math.Max(0, j - 1); y <= Math.Min(n - 1, j + 1); y++)
                    {
                        sum += matrix[x, y];
                        count++;
                    }
                }
                smoothedMatrix[i, j] = sum / count;
            }
        }

        // Нахождение суммы модулей элементов, расположенных ниже главной диагонали сглаженной матрицы
        double sumBelowDiagonal = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i > j) // Проверка, что элемент расположен ниже главной диагонали
                {
                    sumBelowDiagonal += Math.Abs(smoothedMatrix[i, j]);
                }
            }
        }

        // Вывод результата
        Console.WriteLine("Pезультат сглаживания  вещественной матрицы размером 10 х 10:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(smoothedMatrix[i, j] + " "); // Вывод каждого элемента smoothedMatrix
            }
            Console.WriteLine(); // Переход к следующей строке после того, как будут выведены все элементы в строке
        }
        Console.WriteLine("Сумма модулей элементов, расположенных ниже главной диагонали: " + sumBelowDiagonal);
    }
}

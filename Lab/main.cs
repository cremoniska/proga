using System;

class Program
{
    static void Main()
    {
        int N = 10; // Размер массива

        // Создание и заполнение массива случайными вещественными числами
        double[] arr = new double[N];
        Random rand = new Random();
        for (int i = 0; i < N; i++)
        {
            arr[i] = rand.NextDouble() * 100 - 50; // Заполняем случайными вещественными числами от -50 до 50
        }

        // Вычисление максимального по модулю элемента массива
        double maxAbs = 0;
        for (int i = 0; i < N; i++)
        {
            if (Math.Abs(arr[i]) > maxAbs)
                maxAbs = Math.Abs(arr[i]);
        }
        Console.WriteLine("Максимальный элемент по модулю: " + maxAbs);

        // Вычисление суммы элементов массива, расположенных между первым и вторым положительными элементами
        double sumBetweenPositives = 0;
        int firstPositiveIndex = -1;
        for (int i = 0; i < N; i++)
        {
            if (arr[i] > 0)
            {
                if (firstPositiveIndex == -1)
                    firstPositiveIndex = i;
                else
                {
                    for (int j = firstPositiveIndex + 1; j < i; j++)
                    {
                        sumBetweenPositives += arr[j];
                    }
                    break;
                }
            }
        }
        Console.WriteLine("Сумма элементов между первым и вторым положительными элементами: " + sumBetweenPositives);

        // Преобразование массива, чтобы элементы, равные нулю, располагались после всех остальных
        Array.Sort(arr, (a, b) => (a == 0 ? 1 : b == 0 ? -1 : 0)); // Сортировка массива

        // Вывод итогового массива
        Console.WriteLine("Преобразованный массив:");
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
    }
}

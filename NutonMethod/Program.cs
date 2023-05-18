using System;
class Newton
{
    static void Main()
    {
        Console.WriteLine("Введите коэфициент перед x(1)^2:");
        double a = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите коэфициент перед x(1)*x(2):");
        double b = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите коэфициент перед x(2)^2:");
        double c = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите значение Epsilen(1):");
        double Accuracy1 = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите значение Epsilen(2):");
        double Accuracy2 = double.Parse(Console.ReadLine());
        double[] leftk = new double[50];
        double[] rightk = new double[50];
        Console.WriteLine("Введите значение x1 x(0):");
        double left = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите значение x2 x(0):");
        double right = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите значение предельного числа итераций M:");
        int M = int.Parse(Console.ReadLine());

        leftk[0] = left;
        rightk[0] = right;
        double a2 = 2 * a;
        double c2 = 2 * c;
        double func1;
        double func;
        int k;
        int flag = 0;
        double[] xk = new double[50]; //xk - gradient
        double[] tk = new double[50];
        tk[0] = 1;
        double[] dkleft = new double[50];
        double[] dkright = new double[50];
        double[] gradleft = new double[50];
        double[] gradright = new double[50];


        for (k = 0; k <= M; k++, tk[k] = tk[k - 1])
        {
            Console.WriteLine($"Итерация {k}");
            xk[k] = (Math.Sqrt(Math.Pow((a2 * leftk[k] + b * rightk[k]), 2) + Math.Pow((b * leftk[k] + c2 * rightk[k]), 2)));
            gradleft[k] = a2 * leftk[k] + b * rightk[k];
            gradright[k] = c2 * rightk[k] + b * leftk[k];

            if (Math.Abs(xk[k]) <= Accuracy1)
            {
                Console.WriteLine($"Gradient f(x({k}) = {xk[k]} < {Accuracy1}");
                func = (a * Math.Pow(leftk[k], 2) + b * leftk[k] * rightk[k] + c * Math.Pow(rightk[k], 2));
                Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({leftk[k]}; {rightk[k]}), f(x) = ({Math.Round(func, 4)})");
                return;
            }
            Console.WriteLine($"Gradient f(x({k}) = {xk[k]} > {Accuracy1}");
            if (k >= M)
            {
                func = (a * Math.Pow(leftk[k], 2) + b * leftk[k] * rightk[k] + c * Math.Pow(rightk[k], 2));
                Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({leftk[k]}; {rightk[k]}), f(x) = ({Math.Round(func, 4)})");
                return;
            }

            double notH = (1 / (a2 * c2 - b * b)) * (a2) * (1 / (a2 * c2 - b * b)) * (c2) - (1 / (a2 * c2 - b * b)) * (-b) * (1 / (a2 * c2 - b * b)) * (-b); //определитель обратной матрицы Гёссе
            Console.WriteLine($"определитель обратной матрицы Гёссе - notH({k}) = {notH}");

            if (notH > 0)
            {
                dkleft[k] = -gradleft[k] * (((1 / (a2 * c2 - (b * b))) * c2) + ((1 / (a2 * c2 - (b * b))) * (-b)));
                Console.WriteLine(dkleft[k]);
                dkright[k] = -gradright[k] * (((1 / (a2 * c2 - (b * b))) * a2) + ((1 / (a2 * c2 - (b * b))) * (-b)));
                Console.WriteLine(dkright[k]);
                tk[k] = 1;

                leftk[k + 1] = leftk[k] + tk[k] * dkleft[k];
                rightk[k + 1] = rightk[k] + tk[k] * dkright[k];
                func = (a * Math.Pow(leftk[k], 2) + b * leftk[k] * rightk[k] + c * Math.Pow(rightk[k], 2));
                func1 = (a * Math.Pow(leftk[k + 1], 2) + b * leftk[k + 1] * rightk[k + 1] + c * Math.Pow(rightk[k + 1], 2));

                double check = (Math.Abs(Math.Sqrt(Math.Pow((leftk[k + 1] - leftk[k]), 2) + Math.Pow((rightk[k + 1] - rightk[k]), 2))));
                double check1 = (Math.Abs((func1 - func)));

                Console.WriteLine($"t({k}) = {tk[k]}");
                Console.WriteLine($"dk({k}) = [{dkleft[k]};{dkright[k]}]");


                if ((check < Accuracy2) && (Math.Abs((func1 - func)) < Accuracy2))
                {
                    Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)} < {Accuracy2} и |x({k + 1})-x({k})| = {check} < {Accuracy2}");
                    if (flag == 1)
                    {
                        Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                        return;
                    }
                    flag++;
                }
                if (flag < 1)
                {
                    Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)}; |x({k + 1})-x({k})| = {check}");
                }
                Console.WriteLine($"X*({k}) - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                Console.WriteLine("\n\n");
            }
            else
            {
                dkleft[k] = -gradleft[k];
                dkright[k] = -gradright[k];
            stepp7:

                leftk[k + 1] = leftk[k] + tk[k] * dkleft[k];
                rightk[k + 1] = rightk[k] + tk[k] * dkright[k];

                func = (a * Math.Pow(leftk[k], 2) + b * leftk[k] * rightk[k] + c * Math.Pow(rightk[k], 2));
                func1 = (a * Math.Pow(leftk[k + 1], 2) + b * leftk[k + 1] * rightk[k + 1] + c * Math.Pow(rightk[k + 1], 2));


                if (func1 < func)
                {
                    Console.WriteLine($"f({k + 1}) - f({k}) = {func1 - func} > 0");
                    Console.WriteLine($"t({k}) = {tk[k]}");
                    tk[k] = tk[k] / 2;
                    goto stepp7;
                }

                double check = (Math.Abs(Math.Sqrt(Math.Pow((leftk[k + 1] - leftk[k]), 2) + Math.Pow((rightk[k + 1] - rightk[k]), 2))));
                double check1 = (Math.Abs((func1 - func)));
                Console.WriteLine($"t({k}) = {tk[k]}");
                Console.WriteLine($"dk({k}) = [{dkleft[k]};{dkright[k]}]");
                //Console.WriteLine($"x({k}) = {Math.Pow(leftk[k], 2) - Math.Pow(rightk[k], 2)}");

                if ((check < Accuracy2) && (Math.Abs((func1 - func)) < Accuracy2))
                {
                    Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)} < {Accuracy2} и |x({k + 1})-x({k})| = {check} < {Accuracy2}");
                    if (flag == 1)
                    {
                        Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                        return;
                    }
                    flag++;
                }
                if (flag < 1)
                {
                    Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)}; |x({k + 1})-x({k})| = {check}");
                }
                Console.WriteLine($"X*({k}) - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                Console.WriteLine("\n\n");
            }

        }
    }
}
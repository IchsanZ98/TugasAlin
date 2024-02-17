using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Kalkulator Matriks dan Sistem Persamaan Linier");

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Penjumlahan Matriks");
            Console.WriteLine("2. Pengurangan Matriks");
            Console.WriteLine("3. Determinan Matriks");
            Console.WriteLine("4. Sistem Persamaan Linier (Non-homogen)");
            Console.WriteLine("5. Kombinasi Vektor Dimensi");
            Console.WriteLine("6. Basis Vektor Dimensi");
            Console.WriteLine("0. Keluar");

            Console.Write("Pilih operasi (0-6): ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddMatrices();
                    break;

                case 2:
                    SubtractMatrices();
                    break;

                case 3:
                    CalculateDeterminant();
                    break;

                case 4:
                    SolveLinearSystem();
                    break;

                case 5:
                    VectorCombination();
                    break;

                case 6:
                    VectorBasis();
                    break;

                case 0:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                    break;
            }
        }
    }

    static void AddMatrices()
    {
        Console.WriteLine("Masukkan dimensi matriks:");
        Console.Write("Baris: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("Kolom: ");
        int columns = int.Parse(Console.ReadLine());

        double[,] matrix1 = InputMatrix("Masukkan matriks pertama:", rows, columns);
        double[,] matrix2 = InputMatrix("Masukkan matriks kedua:", rows, columns);

        double[,] result = new double[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }

        Console.WriteLine("Hasil Penjumlahan Matriks:");
        PrintMatrix(result);
    }

    static void SubtractMatrices()
    {
        Console.WriteLine("Masukkan dimensi matriks:");
        Console.Write("Baris: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("Kolom: ");
        int columns = int.Parse(Console.ReadLine());

        double[,] matrix1 = InputMatrix("Masukkan matriks pertama:", rows, columns);
        double[,] matrix2 = InputMatrix("Masukkan matriks kedua:", rows, columns);

        double[,] result = new double[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = matrix1[i, j] - matrix2[i, j];
            }
        }

        Console.WriteLine("Hasil Pengurangan Matriks:");
        PrintMatrix(result);
    }

    static void CalculateDeterminant()
    {
        Console.WriteLine("Masukkan dimensi matriks persegi:");
        Console.Write("Ordinat: ");
        int order = int.Parse(Console.ReadLine());

        double[,] matrix = InputMatrix("Masukkan matriks:", order, order);

        double determinant = Determinant(matrix, order);
        Console.WriteLine($"Determinan Matriks: {determinant}");
    }

    static void SolveLinearSystem()
    {
        Console.WriteLine("Masukkan dimensi matriks koefisien:");
        Console.Write("Baris: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("Kolom: ");
        int columns = int.Parse(Console.ReadLine());

        double[,] coefficients = InputMatrix("Masukkan matriks koefisien:", rows, columns);
        double[] constants = new double[rows];

        Console.WriteLine("Masukkan matriks konstanta:");

        for (int i = 0; i < rows; i++)
        {
            Console.Write($"Konstanta untuk baris {i + 1}: ");
            constants[i] = double.Parse(Console.ReadLine());
        }

        double[] solution = SolveLinearSystem(coefficients, constants);

        Console.WriteLine("Solusi Sistem Persamaan Linier:");

        for (int i = 0; i < solution.Length; i++)
        {
            Console.WriteLine($"x{i + 1} = {solution[i]}");
        }
    }

    static void VectorCombination()
    {
        Console.WriteLine("Masukkan dimensi vektor:");
        Console.Write("Dimensi: ");
        int dimensions = int.Parse(Console.ReadLine());

        Console.WriteLine("Masukkan koefisien vektor:");

        double[] vector = new double[dimensions];

        for (int i = 0; i < dimensions; i++)
        {
            Console.Write($"Koefisien untuk dimensi {i + 1}: ");
            vector[i] = double.Parse(Console.ReadLine());
        }

        Console.WriteLine("Kombinasi Vektor:");
        Console.WriteLine($"V = {string.Join(", ", vector)}");
    }

    static void VectorBasis()
    {
        Console.WriteLine("Masukkan dimensi vektor:");
        Console.Write("Dimensi: ");
        int dimensions = int.Parse(Console.ReadLine());

        double[,] vectors = InputMatrix("Masukkan vektor-vektor basis:", dimensions, dimensions);

        if (IsLinearlyIndependent(vectors))
        {
            Console.WriteLine("Vektor-vektor basis tersebut membentuk basis.");
        }
        else
        {
            Console.WriteLine("Vektor-vektor basis tersebut tidak membentuk basis.");
        }
    }

    static double[,] InputMatrix(string prompt, int rows, int columns)
    {
        Console.WriteLine(prompt);

        double[,] matrix = new double[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write($"Elemen [{i + 1}, {j + 1}]: ");
                matrix[i, j] = double.Parse(Console.ReadLine());
            }
        }

        return matrix;
    }

    static void PrintMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }

    static double Determinant(double[,] matrix, int order)
    {
        if (order == 1)
        {
            return matrix[0, 0];
        }
        else if (order == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        else
        {
            double det = 0;

            for (int j = 0; j < order; j++)
            {
                det += matrix[0, j] * Cofactor(matrix, 0, j, order - 1) * Math.Pow(-1, j);
            }

            return det;
        }
    }

    static double Cofactor(double[,] matrix, int row, int col, int order)
    {
        double[,] submatrix = new double[order, order];
        int subrow = 0;
        int subcol = 0;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (i != row && j != col)
                {
                    submatrix[subrow, subcol++] = matrix[i, j];
                    if (subcol == order)
                    {
                        subrow++;
                        subcol = 0;
                    }
                }
            }
        }

        return Determinant(submatrix, order);
    }

    static double[] SolveLinearSystem(double[,] coefficients, double[] constants)
    {
        int n = coefficients.GetLength(0);
        double[] solution = new double[n];

        for (int i = n - 1; i >= 0; i--)
        {
            double sum = constants[i];

            for (int j = i + 1; j < n; j++)
            {
                sum -= coefficients[i, j] * solution[j];
            }

            solution[i] = sum / coefficients[i, i];
        }

        return solution;
    }

    static bool IsLinearlyIndependent(double[,] vectors)
    {
        int dimensions = vectors.GetLength(0);

        // Convert the 2D array to jagged array for easier indexing
        double[][] vectorArray = new double[dimensions][];

        for (int i = 0; i < dimensions; i++)
        {
            vectorArray[i] = new double[dimensions];

            for (int j = 0; j < dimensions; j++)
            {
                vectorArray[i][j] = vectors[i, j];
            }
        }

        // Perform Gaussian elimination
        for (int i = 0; i < dimensions; i++)
        {
            // Find the pivot
            int pivotRow = i;

            for (int j = i + 1; j < dimensions; j++)
            {
                if (Math.Abs(vectorArray[j][i]) > Math.Abs(vectorArray[pivotRow][i]))
                {
                    pivotRow = j;
                }
            }

            if (vectorArray[pivotRow][i] == 0)
            {
                return false; // The vectors are linearly dependent
            }

            // Swap rows
            double[] temp = vectorArray[i];
            vectorArray[i] = vectorArray[pivotRow];
            vectorArray[pivotRow] = temp;

            // Eliminate below the pivot
            for (int j = i + 1; j < dimensions; j++)
            {
                double factor = vectorArray[j][i] / vectorArray[i][i];

                for (int k = i; k < dimensions; k++)
                {
                    vectorArray[j][k] -= factor * vectorArray[i][k];
                }
            }
        }

        return true; // The vectors are linearly independent
    }
}
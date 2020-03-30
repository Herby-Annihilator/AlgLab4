using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryForAlgLab;
using System.IO;

namespace AlgLab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] menu = new string[] { "Задать точки вручную",
            "Задать точки рандомно",
            "Сформировать остовный подграф",
            "Восстановить из файла"};
            char[] menuSymbols = new char[] { 'q', 'w', 'e', 'r' };
            bool goOut = false;
            List<Component> components = new List<Component>();

            do
            {

                switch (Subroutines.PrintMenu(menu, menuSymbols))
                {
                    //
                    // Задать точки вручную
                    //
                    case 'q':
                        {
                            if (File.Exists("input.dat"))
                            {
                                File.Delete("input.dat");
                            }
                            components.Clear();
                            int count;
                            do
                            {
                                do
                                {
                                    Console.Write("Сколько точек вы зададите ");
                                } while (!Int32.TryParse(Console.ReadLine(), out count));
                            } while (count < 1 || count > 15);

                            StreamWriter writer = new StreamWriter("input.dat");
                            for (int i = 0; i < count; i++)
                            {
                                Console.WriteLine("Точка № " + (i + 1));
                                int x, y;
                                do
                                {
                                    Console.Write("Х = ");
                                } while (!Int32.TryParse(Console.ReadLine(), out x));
                                do
                                {
                                    Console.Write("Y = ");
                                } while (!Int32.TryParse(Console.ReadLine(), out y));

                                writer.WriteLine(x + " " + y);

                                Vertex vertex = new Vertex(x, y, i, i);
                                List<Vertex> vertices = new List<Vertex>();
                                vertices.Add(vertex);
                                components.Add(new Component(i, vertices));
                            }
                            writer.Close();
                            Console.Write("Точки успешно заданы. Нажмите что-нибудь...");
                            Console.ReadKey(true);
                            break;
                        }
                    //
                    // Задать точки рандомно
                    //
                    case 'w':
                        {
                            if (File.Exists("input.dat"))
                            {
                                File.Delete("input.dat");
                            }
                            components.Clear();
                            Random random = new Random();

                            int count;
                            do
                            {
                                do
                                {
                                    Console.Write("Сколько точек вы зададите ");
                                } while (!Int32.TryParse(Console.ReadLine(), out count));
                            } while (count < 1 || count > 150);

                            int x, y;

                            StreamWriter writer = new StreamWriter("input.dat");
                            for (int i = 0; i < count; i++)
                            {
                                x = random.Next(-500, 500);
                                y = random.Next(-500, 500);
                                writer.WriteLine(x + " " + y);
                                Vertex vertex = new Vertex(x, y, i, i);
                                List<Vertex> vertices = new List<Vertex>();
                                vertices.Add(vertex);
                                components.Add(new Component(i, vertices));
                            }
                            writer.Close();

                            Console.Write("Точки успешно заданы. Нажмите что-нибудь...");
                            Console.ReadKey(true);
                            break;
                        }
                    //
                    // Сформировать остовный подграф
                    //
                    case 'e':
                        {
                            if (components.Count == 0)
                            {
                                Console.Write("Список пуст! Нажмите что-нибудь...");
                                Console.ReadKey(true);
                            }
                            else
                            {
                                List<Component> workingList = new List<Component>();

                                for (int i = 0; i < components.Count; i++)
                                {
                                    workingList.Add(components[i].Clone());
                                }

                                int componentsCount = workingList.Count;
                                int currentComponentNumber = 0;
                                double smallestPathBetweenComponents;
                                while (componentsCount > 1)
                                {
                                    smallestPathBetweenComponents = 2000000000;
                                    int firstSmallestPathComponent = 0;
                                    int secondSmallestPathComponent = 1;
                                    // находим пару компонент с кратчайшим расстоянием между ними
                                    for (int i = 0; i < componentsCount; i++)
                                    {
                                        if (i == currentComponentNumber)
                                        {
                                            continue;
                                        }

                                        double path = Component.SmallestPathBetweenComponents(workingList[currentComponentNumber], workingList[i]);
                                        if (path <= smallestPathBetweenComponents)
                                        {
                                            // опасный участок
                                            if (path == smallestPathBetweenComponents)
                                            {
                                                if (workingList[i].ID < workingList[secondSmallestPathComponent].ID)
                                                {
                                                    secondSmallestPathComponent = i;
                                                }
                                                continue;
                                            }
                                            //************************
                                            firstSmallestPathComponent = currentComponentNumber;
                                            secondSmallestPathComponent = i;
                                            smallestPathBetweenComponents = path;
                                        }
                                    }
                                    //сливаем найденную пару компонент
                                    workingList[currentComponentNumber] = Component.MergeComponents(workingList[currentComponentNumber], workingList[secondSmallestPathComponent]);
                                    workingList.RemoveAt(secondSmallestPathComponent);
                                    //число компонент автоматически уменьшилось на 1
                                    componentsCount--;
                                    // нужно перейти к следующей компоненте в списке
                                    currentComponentNumber++;
                                    // если данные величины совпали, значит этап закончен и можно обходить список сначала
                                    if (componentsCount <= currentComponentNumber)
                                    {
                                        currentComponentNumber = 0;
                                    }
                                }

                                // проверка на правильность, если все хорошо, то сюда никогда не попадем
                                if (workingList.Count != 1)
                                {
                                    Console.WriteLine("Error, error, error! WorkingList.Count more then 1");
                                    Console.ReadKey(true);
                                    break;
                                }

                                // формируем матрицу
                                int[][] matrix = new int[workingList[0].VertexCount][];
                                for (int i = 0; i < workingList[0].VertexCount; i++)
                                {
                                    matrix[i] = new int[workingList[0].VertexCount];
                                }
                                double sum = 0;
                                for (int i = 0; i < workingList[0].EdgeCount; i++)
                                {
                                    int row = workingList[0].Edges[i].FirstVertex.ID;
                                    int col = workingList[0].Edges[i].SecondVertex.ID;
                                    matrix[row][col] = 1;
                                    matrix[col][row] = 1;
                                    sum += workingList[0].Edges[i].FirstVertex.LenghtFromVertex(workingList[0].Edges[i].SecondVertex);
                                }

                                // выводим данные
                                StreamWriter writer = new StreamWriter("output.dat");
                                Console.WriteLine("\n");
                                for (int i = 0; i < workingList[0].VertexCount; i++)
                                {
                                    for (int j = 0; j < workingList[0].VertexCount; j++)
                                    {
                                        Console.Write(matrix[i][j] + " ");
                                        writer.Write(matrix[i][j] + ", ");
                                    }
                                    Console.WriteLine();
                                    writer.WriteLine();
                                }
                                writer.Close();
                                Console.WriteLine("\n\nОбщая величина дерева (по длинам ребер) = " + sum);

                                Console.WriteLine("\nНажмите что-нибудь...");
                                Console.ReadKey(true);
                            }
                            break;
                        }
                    //
                    // восстановить из файла
                    //
                    case 'r':
                        {
                            if (!File.Exists("input.dat"))
                            {
                                Console.WriteLine("Файл не существует. Нажмите что-нибудь...");
                                Console.ReadKey(true);
                                break;
                            }
                            components.Clear();
                            StreamReader reader = new StreamReader("input.dat");
                            string[] coords = reader.ReadToEnd().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < coords.Length; i++)
                            {
                                string[] specificCoords = coords[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                Int32.TryParse(specificCoords[0], out int x);
                                Int32.TryParse(specificCoords[1], out int y);
                                Vertex vertex = new Vertex(x, y, i, i);
                                List<Vertex> vertices = new List<Vertex>();
                                vertices.Add(vertex);
                                components.Add(new Component(i, vertices));
                            }
                            reader.Close();
                            Console.WriteLine("\nВосстановлено");
                            Console.WriteLine("\nНажмите что-нибудь...");
                            Console.ReadKey(true);
                            break;
                        }
                    //
                    // Exit
                    //
                    case (char)27:
                        {
                            goOut = true;
                            break;
                        }
                }

            } while (!goOut);
        }
    }
}

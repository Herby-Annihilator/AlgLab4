using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryForAlgLab;

namespace AlgLab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] menu = new string[] { "Задать точки вручную",
            "Задать точки рандомно",
            "Сформировать остовный подграф"};
            char[] menuSymbols = new char[] { 'q', 'w', 'e' };
            bool goOut = false;

            do
            {
                List<Component> components = new List<Component>();
                switch (Subroutines.PrintMenu(menu, menuSymbols))
                {
                    // Задать точки вручную
                    case 'q':
                        {
                            components.Clear();
                            int count;
                            do
                            {
                                do
                                {
                                    Console.Write("Сколько точек вы зададите ");
                                } while (!Int32.TryParse(Console.ReadLine(), out count));
                            } while (count < 1 || count > 15);

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

                                Vertex vertex = new Vertex(x, y, i, i);
                                List<Vertex> vertices = new List<Vertex>();
                                vertices.Add(vertex);
                                components.Add(new Component(i, vertices));
                            }
                            Console.Write("Точки успешно заданы. Нажмите что-нибудь...");
                            Console.ReadKey(true);
                            break;
                        }
                    // Задать точки рандомно
                    case 'w':
                        {
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

                            for (int i = 0; i < count; i++)
                            {
                                x = random.Next(-500, 500);
                                y = random.Next(-500, 500);
                                Vertex vertex = new Vertex(x, y, i, i);
                                List<Vertex> vertices = new List<Vertex>();
                                vertices.Add(vertex);
                                components.Add(new Component(i, vertices));
                            }

                            Console.Write("Точки успешно заданы. Нажмите что-нибудь...");
                            Console.ReadKey(true);
                            break;
                        }
                    // Сформировать остовный подграф
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
                                while (componentsCount > 1)
                                {
                                    // находим пару компонент с кратчайшим расстоянием между ними

                                }
                            }
                            break;
                        }
                    // Exit
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

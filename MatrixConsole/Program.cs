using System;
using System.Linq;
using System.Threading;

namespace MatrixConsole
{
    internal class Program
    {
        static string katakana = "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨラリルレロヮワヰヱヲンヴヵヶヷヸヹヺ";
        static Column[] cols;

        static Random random = new Random();
        static string getrandomstring(int len)
        {
            return new string(Enumerable.Repeat(katakana, len).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static void setup(int size)
        {
            cols = new Column[size];

            for (int i = 0; i < size; i++)
            {
                cols[i] = new Column();
                cols[i].n = Math.Clamp(random.Next(-30, 10), 0, 10);
                cols[i].content = getrandomstring(random.Next(Console.WindowHeight / 4, Console.WindowHeight - 1 - cols[i].n));
            }
        }

        static int getmaxlength()
        {
            int x = 0;

            foreach (Column column in cols)
            {
                if (column.content.Length > x) x = column.content.Length;
            }

            return x;
        }

        static void draw()
        {
            /*int x = 0;

            while (true)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    Column col = cols[i];

                    char c = x >= col.content.Length ? ' ' : col.content[x];

                    Console.Write(c);
                }
                Console.WriteLine();

                break;
            }*/

            int i = 0;
            int x = 0;
            int max = getmaxlength();
            while (true)
            {
                if (x > max) { Console.Clear(); break; }
                if (i >= cols.Length) { i = 0; Console.WriteLine(); x++; Thread.Sleep(500); } 
                Column col = cols[i];

                int n = x - col.n;
                char c = (n < 0 || n >= col.content.Length) ? ' ' : col.content[n];
                Console.Write(c);
                if (c == ' ') Console.Write(c);

                i++;
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Matrix";
            //Console.WindowWidth = 80;
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            while (true)
            {
                setup(Console.WindowWidth / 2);
                draw();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WorldUtils;

namespace StationeersTools
{
    public static class PrintUtils
    {
        public static string[] BuildRow (string c1 = "", string c2 = "", string c3 = "", string c4 = "")
        {
            return new string[] { c1, c2, c3, c4 };
        }

        public static void PrepareConsole (bool visibleCursor = false, ConsoleColor color = ConsoleColor.White)
        {
            Console.CursorVisible = visibleCursor;
            Console.ForegroundColor = color;
        }

        public static void PrintLine (string message, ConsoleColor color)
        {
            ConsoleColor c = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = c;
        }

        public static string RequestInputLine (string message)
        {
            PrepareConsole(color: ConsoleColor.DarkYellow);
            Console.Write(message + ": ");
            PrepareConsole(true, ConsoleColor.White);
            string input = Console.ReadLine();
            PrepareConsole();
            return input;
        }

        public static void PrintColumns (string[][] content, int fromtop)
        {
            int[] maxlengths = { 0, 0, 0, 0 };
            int[] indexes = { 0, 0, 0, 0 };
            for (int i = 0; i < content.GetLength(0); i++)
            {
                for (int c = 0; c < 4; c++)
                {
                    maxlengths[c] = (content[i][c].Length > maxlengths[c]) ? content[i][c].Length : maxlengths[c];
                }
            }

            for (int i = 1; i < indexes.Length; i++)
            {
                indexes[i] = maxlengths[i - 1] + 4 + indexes[i - 1];
            }

            foreach (string[] line in content)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    Console.SetCursorPosition(indexes[i], fromtop);
                    Console.Write(line[i]);
                }
                fromtop++;
            }
        }

        public static Vector3 RequestVector(string message)
        {
            PrintUtils.PrintLine(message, ConsoleColor.DarkYellow);

            Vector3 point = new Vector3();
            bool fx = false, fy = false, fz = false;

            while (!fx)
            {
                string input = PrintUtils.RequestInputLine("X");
                try
                {
                    point.x = float.Parse(input);
                    fx = true;
                }
                catch (Exception ex)
                {
                    PrintUtils.PrintLine("Incorrect input!", ConsoleColor.Red);
                }
            }
            while (!fy)
            {
                string input = PrintUtils.RequestInputLine("Y");
                try
                {
                    point.y = float.Parse(input);
                    fy = true;
                }
                catch (Exception ex)
                {
                    PrintUtils.PrintLine("Incorrect input!", ConsoleColor.Red);
                }
            }
            while (!fz)
            {
                string input = PrintUtils.RequestInputLine("Z");
                try
                {
                    point.z = float.Parse(input);
                    fz = true;
                }
                catch (Exception ex)
                {
                    PrintUtils.PrintLine("Incorrect input!", ConsoleColor.Red);
                }
            }

            return point;
        }

        public static float RequestFloat(string message)
        {
            float value = 0;
            bool fr = false;

            PrintUtils.PrintLine(message, ConsoleColor.DarkYellow);

            while (!fr)
            {
                string input = PrintUtils.RequestInputLine("Value");
                try
                {
                    value = float.Parse(input);
                    fr = true;
                }
                catch (Exception ex)
                {
                    PrintUtils.PrintLine("Incorrect input!", ConsoleColor.Red);
                }
            }

            return value;
        }
    }
}

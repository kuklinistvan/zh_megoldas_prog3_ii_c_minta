using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_c
{
    class ColorStatusWriter
    {
        private object consoleLock = new object();

        public ColorStatusWriter(object consoleLock = null)
        {
            if(consoleLock != null)
            {
                this.consoleLock = consoleLock;
            }
        }

        public void Write(string linecontent, int lineNumberStartingWithOne, ConsoleColor color)
        {
            lock(consoleLock)
            {
                ConsoleCursorState state = ConsoleCursorState.Save();

                Console.CursorTop = lineNumberStartingWithOne - 1;
                Console.CursorLeft = 0;

                ClearCurrentLine();

                Console.ForegroundColor = color;
                Console.Write(linecontent);

                state.Load();
            }
        }

        private void ClearCurrentLine()
        {
            int characters = Console.BufferWidth;
            
            Console.CursorLeft = 0;

            for (int i = 0; i < characters; i++)
            {
                Console.Write(' ');
            }

            Console.CursorTop--;
        }
    }

    struct ConsoleCursorState
    {
        int left;
        int top;
        ConsoleColor color;

        public static ConsoleCursorState Save()
        {
            return new ConsoleCursorState(Console.CursorLeft, Console.CursorTop, Console.ForegroundColor);
        }

        public ConsoleCursorState(int left, int top, ConsoleColor color)
        {
            this.left = left;
            this.top = top;
            this.color = color;
        }

        public void Load()
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
            Console.ForegroundColor = color;
        }
    }
}

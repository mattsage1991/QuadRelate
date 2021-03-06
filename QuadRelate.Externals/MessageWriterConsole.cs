﻿using QuadRelate.Contracts;
using System;

namespace QuadRelate.Externals
{
    public class MessageWriterConsole : IMessageWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }
    }
}

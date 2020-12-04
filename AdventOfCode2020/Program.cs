using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AdventOfCode2020.Framework;
using static System.Console;
using static System.ConsoleColor;

namespace AdventOfCode2020
{
    public class Program
    {
        static void Main(string[] args)
        {
            WindowsClipboard.SetText("");
            var totalTime = Stopwatch.StartNew();
            RunAll();
            WriteLine();
            WriteLine($"Total: {totalTime.Elapsed}");
        }

        private static void RunAll()
        {
            foreach (var answerMethod in PuzzleRefection.GetAnswerMethods())
            {
                PrintAnswer(answerMethod);
            }
        }

        public static void PrintAnswer(MethodInfo methodInfo)
        {
            Write($"{methodInfo.DeclaringType.Name}.{methodInfo.Name}() => ");

            var result = GetAnswer(methodInfo);

            // Keep the last result on the clipboard
            WindowsClipboard.SetText(result.ToString());

            var expectedResult = methodInfo.GetCustomAttribute<ResultAttribute>()?.Result;

            if (expectedResult != null)
            {
                if (expectedResult.Equals(result))
                {
                    BackgroundColor = Green;
                    ForegroundColor = White;
                    WriteLine(result);
                }
                else
                {
                    BackgroundColor = Red;
                    ForegroundColor = White;
                    Write(result);

                    BackgroundColor = Black;
                    ForegroundColor = DarkGray;
                    WriteLine(" Expected is " + expectedResult);
                }
            }
            else
            {
                BackgroundColor = DarkGray;
                ForegroundColor = White;
                WriteLine(result);
                WindowsClipboard.SetText(result.ToString());

            }
            BackgroundColor = Black;
            ForegroundColor = Gray;
        }

        private static object GetAnswer(MethodInfo methodInfo)
        {
            var instance = methodInfo.IsStatic ? null : Activator.CreateInstance(methodInfo.DeclaringType);
            var input = InputDataManager.GetInputArgs(methodInfo);

            return methodInfo.Invoke(instance, input);
        }
    }
}
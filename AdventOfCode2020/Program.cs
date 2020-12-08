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
            WriteLine("Total:                 {0,12:##,###.000} ms", totalTime.Elapsed.TotalMilliseconds); ;
            WriteLine();
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
            Write($"{methodInfo.DeclaringType.Name[^2..].TrimStart('0'),2}.{methodInfo.Name[^1..]} => ");

            var stopwatch = Stopwatch.StartNew();
            var result = GetAnswer(methodInfo);
            stopwatch.Stop();

            // Keep the last result on the clipboard
            WindowsClipboard.SetText(result.ToString());

            var expectedResult = methodInfo.GetCustomAttribute<ResultAttribute>()?.Result;

            if (expectedResult != null)
            {
                if (expectedResult.Equals(result))
                {
                    BackgroundColor = DarkGreen;
                    ForegroundColor = White;
                    Write("{0,15}", result);
                }
                else
                {
                    BackgroundColor = Red;
                    ForegroundColor = White;
                    Write("{0,15}", result);

                    BackgroundColor = Black;
                    ForegroundColor = DarkGray;
                    Write(" Expected is " + expectedResult);
                }
            }
            else
            {
                BackgroundColor = DarkGray;
                ForegroundColor = White;
                Write("{0,15}", result);
                WindowsClipboard.SetText(result.ToString());

            }
            BackgroundColor = Black;
            ForegroundColor = Gray;
            WriteLine("{0,12:##,###.000} ms", stopwatch.Elapsed.TotalMilliseconds );
        }

        private static object GetAnswer(MethodInfo methodInfo)
        {
            var instance = methodInfo.IsStatic ? null : Activator.CreateInstance(methodInfo.DeclaringType);
            var input = InputDataManager.GetInputArgs(methodInfo);

            return methodInfo.Invoke(instance, input);
        }
    }
}
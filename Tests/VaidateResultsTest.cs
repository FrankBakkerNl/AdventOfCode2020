using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AdventOfCode2020;
using FluentAssertions;
using Xunit;

namespace Tests
{
    class VerifyResultData : TheoryData<string, MethodInfo, object>
    {
        public VerifyResultData()
        {
            var answerMethods = AdventOfCode2020.Framework.PuzzleRefection.GetAnswerMethods(ignoreFocus:true);
            foreach (var method in answerMethods)
            {
                var expectedResult = method?.GetCustomAttributes<ResultAttribute>().FirstOrDefault()?.Result;
                if (expectedResult != null)
                {
                    Add($"{method.DeclaringType.Name}.{method.Name}", method, expectedResult);
                }
            }
        }
    }
    public class ValidateResultsTest
    {
        [Theory]
        [ClassData(typeof(VerifyResultData))]
        public void VerifyResult(string name, MethodInfo methodInfo, object expectedResult)
        {
            var input = InputDataManager.GetInputArgs(methodInfo);
            var instance = methodInfo.IsStatic ? null : Activator.CreateInstance(methodInfo.DeclaringType);

            var result = methodInfo.Invoke(instance, input);
            result.Should().Be(expectedResult);
        }
    }
}

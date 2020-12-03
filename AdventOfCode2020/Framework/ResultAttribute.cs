using System;

namespace AdventOfCode2020
{
    public class ResultAttribute : Attribute
    {
        public object Result { get; }

        public ResultAttribute(object result)
        {
            Result = result;
        }
    }
}
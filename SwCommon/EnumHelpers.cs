using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwCommon
{
    public static class EnumHelpers
    {
        /// <summary>
        /// Gets all of the flags that are present in the argument enum value.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetFlags(Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }
    }
}

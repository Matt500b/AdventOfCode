namespace AdventOfCode.AoC2018
{
	using System.Collections.Generic;
    using System.IO;
    using System.Linq;

	public class Day1
		: AdventOfCodeBase
	{
		public override void Main()
		{
            /* Part 1 */
            UnitTest<int>(SolvePart1<int>("Files/2018/Day1/P1_UT1.txt"), 3);
            UnitTest<int>(SolvePart1<int>("Files/2018/Day1/P1_UT2.txt"), 0);
            UnitTest<int>(SolvePart1<int>("Files/2018/Day1/P1_UT3.txt"), -6);

            SolveProblem<int>(SolvePart1<int>("Files/2018/Day1/Input.txt"));

            /* Part 2 */
            UnitTest<int>(SolvePart2<int>("Files/2018/Day1/P2_UT1.txt"), 0);
            UnitTest<int>(SolvePart2<int>("Files/2018/Day1/P2_UT2.txt"), 10);
            UnitTest<int>(SolvePart2<int>("Files/2018/Day1/P2_UT3.txt"), 5);
            UnitTest<int>(SolvePart2<int>("Files/2018/Day1/P2_UT4.txt"), 14);

            SolveProblem<int>(SolvePart2<int>("Files/2018/Day1/Input.txt"));
        }

        /// <summary>Code for solving Part 1.</summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>The answer for solving Part 1</returns>
        public override T SolvePart1<T>(string input)
		{
			return (T)(object)File.ReadAllLines(input).Sum(record => int.Parse(record.Replace("+", "")));
		}

        /// <summary>Code for solving Part 2.</summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>The answer for solving Part 2</returns>
        public override T SolvePart2<T>(string input)
		{
            bool duplicateFound   = false;
            int currentValue      = 0;
            int index             = 0;
            List<int> valuesfound = new List<int>();
            List<int> fileValues  = File.ReadAllLines(input).Select(record => int.Parse(record.Replace("+", ""))).ToList();
            int maxIndex          = fileValues.Count - 1;

            while (!duplicateFound)
            {
                if (!valuesfound.Exists(record => record == currentValue))
                    valuesfound.Add(currentValue);
                else
                    break;

                currentValue += fileValues[index++];

                if (index > maxIndex)
                    index = 0;
            }

            return (T)(object)currentValue;
        }
	}
}

namespace AdventOfCode.AoC2017
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Day2
		: AdventOfCodeBase
	{
		public override void Main()
		{
			/* Part 1 */
			UnitTest<int>(SolvePart1<int>("Files/AoC2017_Day2_P1_UT1.csv"), 18);

			SolveProblem<int>(SolvePart1<int>("Files/AoC2017_Day2_P1.csv"));

			/* Part 2 */
			UnitTest<int>(SolvePart2<int>("Files/AoC2017_Day2_P1_UT2.csv"), 9);

			SolveProblem<int>(SolvePart2<int>("Files/AoC2017_Day2_P1.csv"));
		}

		/// <summary>
		/// Solves the part1.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart1<T>(string input)
		{
			List<string> fileLines = File.ReadLines(input).ToList();
			int sum                = 0;

			foreach (string line in fileLines)
				sum += GetMinMaxDifferenceInLine(line);

			return (T)(object)sum;
		}

		/// <summary>
		/// Solves the part2.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart2<T>(string input)
		{
			List<string> fileLines = File.ReadLines(input).ToList();
			int sum                = 0;

			foreach (string line in fileLines)
				sum += GetEvenlyDivisibleValue(line);

			return (T)(object)sum;
		}

		/// <summary>
		/// Gets the minimum maximum difference in line.
		/// </summary>
		/// <param name="line">The line.</param>
		/// <returns>A integer value.</returns>
		private int GetMinMaxDifferenceInLine(string line)
		{
			List<int> listOfInts = line.Split('\t').ToList().Select(int.Parse).ToList();

			return listOfInts.Max() - listOfInts.Min();
		}

		/// <summary>
		/// Gets the evenly divisible value.
		/// </summary>
		/// <param name="line">The line.</param>
		/// <returns></returns>
		private int GetEvenlyDivisibleValue(string line)
		{
			List<int> listOfInts = line.Split('\t').ToList().Select(int.Parse).ToList();
			listOfInts.Sort();

			for (int i = listOfInts.Count - 1; i >= 0; i--)
			{
				if (listOfInts[i] == 0)
					continue;

				for (int j = 0; j < listOfInts.Count; j++)
				{
					if (listOfInts[j] == 0 || j == i)
						continue;
					else if (listOfInts[i] % listOfInts[j] == 0)
						return listOfInts[i] / listOfInts[j];
				}
			}

			return 0;
		}
	}
}

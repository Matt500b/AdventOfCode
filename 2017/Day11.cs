namespace AdventOfCode.AoC2017
{
    using System;
    using System.IO;

    public class Day11
		: AdventOfCodeBase
	{
		private int _maxValueFound;

		public override void Main()
		{
			/* Part 1 */
			UnitTest<int>(SolvePart1<int>("Files/AoC2017_Day11_P1_UT1.csv"), 3);
			UnitTest<int>(SolvePart1<int>("Files/AoC2017_Day11_P1_UT2.csv"), 0);
			UnitTest<int>(SolvePart1<int>("Files/AoC2017_Day11_P1_UT3.csv"), 2);
			UnitTest<int>(SolvePart1<int>("Files/AoC2017_Day11_P1_UT4.csv"), 3);

			SolveProblem<int>(SolvePart1<int>("Files/AoC2017_Day11_P1.csv"));

			/* Part 2 */
			SolveProblem<int>(SolvePart2<int>("Files/AoC2017_Day11_P1.csv"));
		}

		/// <summary>
		/// Solves the part1.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart1<T>(string input)
		{
			string rawInstructions     = File.ReadAllText(input);
			string[] splitInstructions = rawInstructions.Split(',');
			int x                      = 0;
			int y                      = 0;
			int z                      = 0;

			foreach (string instruction in splitInstructions)
				(x, y, z) = Move(x, y, z, instruction);

			return (T)(object)Math.Max(-x, Math.Max(-y, -z));
		}

		/// <summary>
		/// Solves the part2.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart2<T>(string input)
		{
			_maxValueFound = 0;
			SolvePart1<T>(input);

			return (T)(object)_maxValueFound;
		}

		/// <summary>
		/// Moves the specified x.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <param name="z">The z.</param>
		/// <param name="direction">The direction.</param>
		/// <returns>The current x, y and z coordinates</returns>
		/// <exception cref="NotImplementedException">UH OH</exception>
		private (int x, int y, int z) Move(int x, int y, int z, string direction)
		{
			switch (direction.ToLowerInvariant())
			{
				case "n":
					x++;
					z--;
					break;
				case "ne":
					x++;
					y--;
					break;
				case "se":
					y--;
					z++;
					break;
				case "s":
					x--;
					z++;
					break;
				case "sw":
					x--;
					y++;
					break;
				case "nw":
					y++;
					z--;
					break;
				default:
					throw new NotImplementedException($"UH OH didn't expect a direction of '{direction}'");
			}

			// Added for Part2
			int currentMax = Math.Max(-x, Math.Max(-y, -z));
			_maxValueFound = currentMax > _maxValueFound ? currentMax : _maxValueFound;

			return (x, y, z);
		}
	}
}

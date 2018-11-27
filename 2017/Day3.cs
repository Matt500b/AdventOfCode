namespace AdventOfCode.AoC2017
{
    using System;

    public class Day3
		: AdventOfCodeBase
	{
		public override void Main()
		{
			/* Part 1 */
			UnitTest<int>(SolvePart1<int>("12"), 3);
			UnitTest<int>(SolvePart1<int>("23"), 2);
			UnitTest<int>(SolvePart1<int>("1024"), 31);

			SolveProblem<int>(SolvePart1<int>("347991"));

			/* Part 2 */
			SolveProblem<int>(SolvePart2<int>("347991"));
		}

		/// <summary>
		/// Solves the part1.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart1<T>(string input)
		{
			int inputAsInt             = int.Parse(input);
			int roundedUpSquareRoot    = (int)Math.Ceiling(Math.Sqrt(inputAsInt));
			int UpperSquare            = (int)Math.Pow(roundedUpSquareRoot, 2);
			bool isEvenSquare          = roundedUpSquareRoot % 2 == 0;
			int numberOfLayers         = isEvenSquare ? roundedUpSquareRoot / 2 : ((roundedUpSquareRoot - 1) / 2);
			int offset                 = UpperSquare - inputAsInt;
			int midPoint               = isEvenSquare ? (UpperSquare + 1) - numberOfLayers : UpperSquare - numberOfLayers;

			if (inputAsInt < midPoint)
				midPoint -= (numberOfLayers * 2);

			return (T)(object)(numberOfLayers + Math.Abs(inputAsInt - midPoint));
		}

		/// <summary>
		/// Solves the part2.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart2<T>(string input)
		{
			int value = 0;

			while (value < int.Parse(input))
				value = GenerateSprialNextValue();

			return (T)(object)value;
		}

		private int GenerateSprialNextValue()
		{
			return 0;
		}
	}
}

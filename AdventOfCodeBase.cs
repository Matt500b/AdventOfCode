namespace AdventOfCode
{
	using System;
	using System.Collections.Generic;

	public abstract class AdventOfCodeBase
    {
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		public abstract void Main();

        /// <summary>Code for solving Part 1.</summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>The answer for solving Part 1</returns>
        public abstract T SolvePart1<T>(string input);

        /// <summary>Code for solving Part 2.</summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>The answer for solving Part 2</returns>
        public abstract T SolvePart2<T>(string input);

        /// <summary>Units the test.</summary>
        /// <typeparam name="T">The type of answer to compare</typeparam>
        /// <param name="input">The input.</param>
        /// <param name="expectedResult">The expected result.</param>
        public void UnitTest<T>(T input, T expectedResult)
		{
			bool isMatch = EqualityComparer<T>.Default.Equals(input, expectedResult);

			Console.Write($"Performing Unit Test...\t Expecting: {expectedResult.ToString()}\t Found: {input.ToString()}\t");

			if (isMatch)
			{
				Console.BackgroundColor = ConsoleColor.DarkGreen;
				Console.Write("SUCCESS");
			}
			else
			{
				Console.BackgroundColor = ConsoleColor.DarkRed;
				Console.Write("FAILED");
			}

            Console.WriteLine();
			Console.ResetColor();
		}

        /// <summary> Writes out the solved answer. </summary>
        /// <typeparam name="T">The type of answer</typeparam>
        /// <param name="result">The result.</param>
        public void SolveProblem<T>(T result)
		{
			Console.WriteLine($"Performing Problem Solving...\t{result.ToString()}");
			Console.WriteLine();
		}
	}
}

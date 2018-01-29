namespace AoC2017
{
	using System;
	using System.Collections.Generic;

	public abstract class AdventOfCodeBase
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		public abstract void Main();

		/// <summary>
		/// Solves the part1.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public abstract T SolvePart1<T>(string input);

		/// <summary>
		/// Solves the part2.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public abstract T SolvePart2<T>(string input);

		/// <summary>
		/// Units the test.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input">The input.</param>
		/// <param name="result">The result.</param>
		public void UnitTest<T>(T input, T result)
		{
			bool isMatch = EqualityComparer<T>.Default.Equals(input, result);

			Console.Write($"Performing Unit Test...\t\t");

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

		/// <summary>
		/// Solves the problem.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="result">The result.</param>
		public void SolveProblem<T>(T result)
		{
			Console.WriteLine($"Performing Problem Solving...\t{result.ToString()}");
			Console.WriteLine();
		}
	}
}

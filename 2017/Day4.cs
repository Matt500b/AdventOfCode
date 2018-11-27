namespace AdventOfCode.AoC2017
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Day4
		: AdventOfCodeBase
	{
		public override void Main()
		{
			/* Part 1 */
			UnitTest<int>(SolvePart1<int>("Files/AoC2017_Day4_P1_UT1.csv"), 2);

			SolveProblem<int>(SolvePart1<int>("Files/AoC2017_Day4_P1.csv"));

			/* Part 2 */
			UnitTest<int>(SolvePart2<int>("Files/AoC2017_Day4_P1_UT2.csv"), 3);

			SolveProblem<int>(SolvePart2<int>("Files/AoC2017_Day4_P1.csv"));
		}

		/// <summary>
		/// Solves the part1.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart1<T>(string input)
		{
			int sum = 0;
			List<string> fileLines = File.ReadLines(input).ToList();

			foreach (string line in fileLines)
				sum += line.Split(' ').ToList().GroupBy(x => x).Count(x => x.Count() > 1) > 0 ? 0 : 1;

			return (T)(object)sum;
		}

		/// <summary>
		/// Solves the part2.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart2<T>(string input)
		{
			int sum = 0;
			List<string> fileLines = File.ReadLines(input).ToList();

			foreach (string line in fileLines)
				sum += ContainsAnagrams(line) ? 0 : 1;

			return (T)(object)sum;
		}

		/// <summary>
		/// Determines whether the specified line contains anagrams.
		/// </summary>
		/// <param name="line">The line.</param>
		/// <returns>
		///   <c>true</c> if the specified line contains anagrams; otherwise, <c>false</c>.
		/// </returns>
		private bool ContainsAnagrams(string line)
		{
			List<string> words = line.Split(' ').ToList();

			// Compare until the penultimate word
			for (int i = 0; i < words.Count - 1; i++)
			{
				string startWord = GetSortedLettersWord(words[i]);

				// Only compare values after the starting word as previous words will have been compared already
				for (int j = i + 1; j < words.Count; j++)
				{
					string compareWord = GetSortedLettersWord(words[j]);

					if (startWord.ToLowerInvariant() == compareWord.ToLowerInvariant())
						return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Gets the sorted letters word.
		/// </summary>
		/// <param name="jumbledWord">The jumbled word.</param>
		/// <returns></returns>
		private string GetSortedLettersWord(string jumbledWord)
		{
			char[] startWordLetters = jumbledWord.ToCharArray();
			Array.Sort(startWordLetters);

			return new string(startWordLetters);
		}
	}
}

namespace AdventOfCode.AoC2018
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
            UnitTest<int>(SolvePart1<int>("Files/2018/Day2/P1_UT1.txt"), 12);

            SolveProblem<int>(SolvePart1<int>("Files/2018/Day2/Input.txt"));

            /* Part 2 */
            UnitTest<string>(SolvePart2<string>("Files/2018/Day2/P2_UT1.txt"), "fgij");

            SolveProblem<string>(SolvePart2<string>("Files/2018/Day2/Input.txt"));
        }

        /// <summary>Code for solving Part 1.</summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>The answer for solving Part 1</returns>
        public override T SolvePart1<T>(string input)
		{
            IEnumerable<IEnumerable<IGrouping<char, char>>> splitAndGroupedChars =  File.ReadAllLines(input).Select(record => record.ToCharArray().GroupBy(characters => characters));

            IEnumerable<IEnumerable<IGrouping<char, char>>> twoTimes   = splitAndGroupedChars.Select(record => record.Where(r2 => r2.Count() == 2));
            IEnumerable<IEnumerable<IGrouping<char, char>>> threeTimes = splitAndGroupedChars.Select(record => record.Where(r2 => r2.Count() == 3));

            return (T)(object)(twoTimes.Count(record => record.Any()) * threeTimes.Count(record => record.Any())) ;
        }

        /// <summary>Code for solving Part 2.</summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>The answer for solving Part 2</returns>
        public override T SolvePart2<T>(string input)
		{
            List<char[]> splitCharacters = File.ReadAllLines(input).Select(record => record.ToCharArray()).ToList();
            int numberOfCharsDifferent   = 0;
            int missingIndex             = 0;
            string matchingValues        = string.Empty;

            for (int i = 0; i < splitCharacters.Count(); i++)
            {
                for (int j = i + 1; j < splitCharacters.Count(); j++)
                {
                    numberOfCharsDifferent = 0;

                    if (splitCharacters[i].Length != splitCharacters[j].Length)
                        continue;

                    for (int k = 0; k < splitCharacters[i].Length; k++)
                    {
                        if (splitCharacters[i][k] != splitCharacters[j][k])
                        {
                            numberOfCharsDifferent++;
                            missingIndex = k;
                        }
                    }

                    if (numberOfCharsDifferent == 1)
                    {
                        List<char> foundCharacters = splitCharacters[i].ToList();
                        foundCharacters.RemoveAt(missingIndex);

                        matchingValues = string.Join("", foundCharacters);
                        break;
                    }
                }
            }

            return (T)(object)matchingValues;
        }
    }
}

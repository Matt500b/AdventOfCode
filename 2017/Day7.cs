namespace AdventOfCode.AoC2017
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Day7
		: AdventOfCodeBase
	{
		public override void Main()
		{
			/* Part 1 */
			UnitTest<string>(SolvePart1<string>("Files/2017/Day7/UT1.csv"), "tknk");

			SolveProblem<string>(SolvePart1<string>("Files/2017/Day7/Input.csv"));

			/* Part 2 */
			UnitTest<int>(SolvePart2<int>("Files/2017/Day7/UT1.csv"), 60);

			SolveProblem<int>(SolvePart2<int>("Files/2017/Day7/Input.csv"));
		}

		/// <summary>
		/// Solves the part1.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart1<T>(string input)
		{
			List<string> names = File.ReadLines(input).ToList();
			List<Tower> towers = ParseTowers(names);

			for (int i = 0; i < towers.Count; i++)
			{
				int counter = 0;

				for (int j = 0; j < towers.Count; j++)
				{
					if (towers[j].TowersAbove.Any(record => record.Name.ToLowerInvariant() == towers[i].Name.ToLowerInvariant()))
						counter++;
				}

				if (counter == 0)
					return (T)(object)towers[i].Name;
			}

			return default(T);
		}

		/// <summary>
		/// Solves the part2.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart2<T>(string input)
		{
			List<string> names    = File.ReadLines(input).ToList();
			List<Tower> towers    = ParseTowers(names);
			Tower baseTower       = towers.Find(record => record.Name.ToLowerInvariant() == SolvePart1<string>(input).ToLowerInvariant());
			int? weightDifference = null;

			while(true)
			{
				List<int> towerAboveWeight = new List<int>();

				foreach(Tower tower in baseTower.TowersAbove)
					towerAboveWeight.Add(CalculateAboveTowerWeight(towers, tower));

				int difference = towerAboveWeight.Max() - towerAboveWeight.Min();

				if (difference > 0)
					weightDifference = towerAboveWeight.Max() - towerAboveWeight.Min();

				if (difference == 0 && weightDifference.HasValue)
					return (T)(object)(baseTower.Weight - weightDifference);


				int index  = towerAboveWeight.FindIndex(record => record == towerAboveWeight.GroupBy(gRecord => gRecord).Where(gRecord => gRecord.Count() == 1).Select(gRecord => gRecord.Key).First());
				baseTower  = towers.Find(record => record.Name.ToLowerInvariant() == baseTower.TowersAbove[index].Name.ToLowerInvariant());
			}
		}

		/// <summary>
		/// Parses the towers.
		/// </summary>
		/// <param name="stringTowers">The string towers.</param>
		/// <returns></returns>
		private List<Tower> ParseTowers(List<string> stringTowers)
		{
			List<Tower> towers = new List<Tower>();

			foreach (string stringTower in stringTowers)
			{
				int weightBeginLocation = stringTower.IndexOf("(");
				int weightEndLocation   = stringTower.IndexOf(")");
				int weightLength        = weightEndLocation - weightBeginLocation - 1;

				Tower tower = new Tower
				{
					Name   = stringTower.Substring(0, weightBeginLocation).Trim(),
					Weight = int.Parse(stringTower.Substring(weightBeginLocation + 1, weightLength))
				};

				if (stringTower.Contains("->"))
				{
					int aboveTowersIndex = stringTower.IndexOf(">");
					string aboveTowers   = stringTower.Substring(aboveTowersIndex + 1).Trim();
					aboveTowers.Split(',').ToList().ForEach(record => tower.TowersAbove.Add(new Tower { Name = record.Trim() }));
				}

				towers.Add(tower);
			}

			return towers;
		}

		/// <summary>
		/// Calculates the above tower weight.
		/// </summary>
		/// <param name="towers">The towers.</param>
		/// <param name="tower">The tower.</param>
		/// <returns></returns>
		private int CalculateAboveTowerWeight(List<Tower> towers, Tower tower)
		{
			Tower refTower = towers.Find(record => record.Name.ToLowerInvariant() == tower.Name.ToLowerInvariant());
			int sum        = refTower.Weight;

			foreach (Tower uppertowers in refTower.TowersAbove)
				sum +=  CalculateAboveTowerWeight(towers, uppertowers);

			return sum;
		}

		/// <summary>
		/// class holding Tower information
		/// </summary>
		public class Tower
		{
			public string Name { get; set; }
			public int Weight { get; set; }
			public List<Tower> TowersAbove { get; set; }

			public Tower()
			{
				TowersAbove = new List<Tower>();
			}
		}
	}
}

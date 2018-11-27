namespace AdventOfCode.AoC2017
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Day8
		: AdventOfCodeBase
	{
		private int _maxValueFound;

		public override void Main()
		{
			/* Part 1 */
			UnitTest<int>(SolvePart1<int>("Files/AoC2017_Day8_P1_UT1.csv"), 1);

			SolveProblem<int>(SolvePart1<int>("Files/AoC2017_Day8_P1.csv"));

			/* Part 2 */
			UnitTest<int>(SolvePart2<int>("Files/AoC2017_Day8_P1_UT1.csv"), 10);

			SolveProblem<int>(SolvePart2<int>("Files/AoC2017_Day8_P1.csv"));
		}

		/// <summary>
		/// Solves the part1.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart1<T>(string input)
		{
			List<Instruction> instructions = GetInstructions(input);
			List<Register> registers       = GetListOfRegisters(instructions);

			foreach (Instruction instruction in instructions)
				ProcessInstructions(instruction, ref registers);

			return (T)(object)registers.Max(record => record.CurrentValue);
		}

		/// <summary>
		/// Solves the part2.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public override T SolvePart2<T>(string input)
		{
			_maxValueFound = 0;

			SolvePart1<int>(input);

			return (T)(object)_maxValueFound;
		}

		/// <summary>
		/// Gets the instructions.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		private List<Instruction> GetInstructions(string input)
		{
			List<Instruction> instructions  = new List<Instruction>();
			List<string> stringInstructions = File.ReadLines(input).ToList();

			foreach (string stringInstruction in stringInstructions)
				instructions.Add(new Instruction(stringInstruction));

			return instructions;
		}

		/// <summary>
		/// Gets the list of registers.
		/// </summary>
		/// <param name="instructions">The instructions.</param>
		/// <returns></returns>
		private List<Register> GetListOfRegisters(List<Instruction> instructions)
		{
			List<Register> registers     = new List<Register>();
			List<string> stringRegisters = instructions.Select(record => record.RegisterToProcess)
														.Concat(instructions.Select(record => record.RegisterToCheck))
														.Distinct()
														.ToList();

			stringRegisters.ForEach(record => registers.Add(new Register(record)));

			return registers;
		}

		/// <summary>
		/// Processes the instructions.
		/// </summary>
		/// <param name="instruction">The instruction.</param>
		/// <param name="registers">The registers.</param>
		private void ProcessInstructions(Instruction instruction, ref List<Register> registers)
		{
			Register registerToCheck   = registers.Find(record => record.Name == instruction.RegisterToCheck);
			int registerToProcessIndex = registers.FindIndex(record => record.Name == instruction.RegisterToProcess);

			if (Processcomparison(registerToCheck.CurrentValue, instruction.Comparision, instruction.ComparisionAmount))
				registers[registerToProcessIndex].CurrentValue += (int)instruction.Sign * instruction.OperationAmount;

			// Added for Part 2
			if (registers[registerToProcessIndex].CurrentValue > _maxValueFound)
				_maxValueFound = registers[registerToProcessIndex].CurrentValue;
		}

		/// <summary>
		/// Processes the comparison.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="operation">The operation.</param>
		/// <param name="right">The right.</param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		private bool Processcomparison(int left, string operation, int right)
		{
			switch (operation)
			{
				case ">":
					return left > right;
				case ">=":
					return left >= right;
				case "<":
					return left < right;
				case "<=":
					return left <= right;
				case "==":
					return left == right;
				case "!=":
					return left != right;
				default:
					throw new InvalidOperationException($"Unable to process operator: {operation}");
			}
		}

		/// <summary>
		/// Class to hold Register information
		/// </summary>
		private class Register
		{
			public string Name { get; set; }
			public int CurrentValue { get; set; }

			public Register(string name)
			{
				Name         = name;
				CurrentValue = 0;
			}
		}

		/// <summary>
		/// Class to hold Instruction information
		/// </summary>
		private class Instruction
		{
			public string RegisterToProcess { get; set; }
			public Sign Sign { get; set; }
			public int OperationAmount { get; set; }
			public string RegisterToCheck { get; set; }
			public string Comparision { get; set; }
			public int ComparisionAmount { get; set; }

			public Instruction(string instruction)
			{
				string[] splitInstruction = instruction.Split(' ');

				RegisterToProcess = splitInstruction[0];
				Sign              = splitInstruction[1].ToLowerInvariant() == "inc" ? Sign.Increment : Sign.Decrement;
				OperationAmount   = Int32.Parse(splitInstruction[2]);
				RegisterToCheck   = splitInstruction[4];
				Comparision       = splitInstruction[5];
				ComparisionAmount = Int32.Parse(splitInstruction[6]);
			}
		}

		/// <summary>
		/// Sign to increment or decrement
		/// </summary>
		private enum Sign
		{
			Decrement = -1,
			Increment = 1
		}
	}
}

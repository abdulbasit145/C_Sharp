using System;

namespace Assignement_01
{
	/// <summary>
	/// This program find All Pairs Shortest Paths in an n-vertex graph using the following techniques.
	/// a) Serial (single threaded) application
	/// b) Multithreaded application (using Thread objects)
	/// c) Multithreaded application (using a ThreadPool)
	/// </summary>
	internal class Program
	{
		static Random random;
		static int postive_infinity = int.MaxValue;
		static int negative_infinity = int.MinValue;

		/// <summary>
		/// Main entry point of the program
		/// </summary>
		/// <param name="args">Command line args</param>
		static void Main(string[] args)
		{
			int size = 5;
			int[,] graph = new int[size, size];

			GenerateRandomGraph(graph, size);
			PrintGraph(graph, size);

	
		}

		/// <summary>
		/// Method <c>GenerateRandomGraph</c>  create a 2-D graph filled with random Integers.
		/// </summary>
		/// <param name="graph">It is a 2-D Array represnting graph.</param>
		/// <param name="size">It is representing the row and column size of the Geraph.</param>
		/// <returns>A 2-D graph filled with random integers.</returns>
		static int[,] GenerateRandomGraph(int[,] graph, int size)
		{
			random = new Random();

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					// Generate a graph with random number between 1 and 20
					graph[i, j] = random.Next(1, 21);
				}
			}
			return graph;
		}

		/// <summary>
		/// Method <c>PrintGraph</c> print the 2-D graph on console.
		/// </summary>
		/// <param name="graph">It is a 2-D Array represnting graph.</param>
		/// <param name="size">It is representing the row and column size of the Geraph.</param>
		static void PrintGraph(int[,] graph, int size)
		{
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					Console.Write(graph[i, j] + "\t");
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
		
		
	}
}

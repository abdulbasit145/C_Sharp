using System;
using System.Diagnostics;

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
		static readonly Stopwatch stopwatch = new Stopwatch();
		const int INF = int.MaxValue;

		/// <summary>
		/// Main entry point of the program
		/// </summary>
		/// <param name="args">Command line args</param>
		static void Main(string[] args)
		{
			int size = 4;
			int[,] graph = new int[size, size];

			GenerateRandomGraph(graph, size);

			Console.WriteLine("\t----- Initial Graph -----\n");
			PrintGraph(graph, size);

			
			Console.WriteLine("\t----- All Pair Shortest Path Graph -----\n");

			stopwatch.Start();
			SerialApplicationForAllPairShortestPath(graph, size);
			stopwatch.Stop();

			TimeSpan elapsed_time = stopwatch.Elapsed;
			Console.WriteLine($"Time taken by Serial Application : {elapsed_time.TotalMilliseconds} milliseconds");

			Console.ReadKey();
		}

		/// <summary>
		/// Method <c>GenerateRandomGraph</c>  create a 2-D graph filled with random Integers.
		/// </summary>
		/// <param name="graph">It is a 2-D Array represnting graph.</param>
		/// <param name="size">It is representing the row and column size of the Geraph.</param>
		static void GenerateRandomGraph(int[,] graph, int size)
		{
			random = new Random();

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					// Generate a graph with random number between 1 and 100 with 10% probability to add INF in between.
					int random_value = random.Next(1, 21);

					if (random_value <= 2)
						graph[i, j] = INF;
					else
						graph[i, j] = random_value;
				}
			}
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
					if (graph[i, j] == INF)
						Console.Write("INF".PadRight(10));
					else
						Console.Write(graph[i, j].ToString().PadRight(10));
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Method <c>SerialApplicationForAllPairShortestPath</c> calculate the all pair shortest path using Flyod Warshall Algorithm.
		/// </summary>
		/// <param name="graph">It is a 2-D Array represnting graph.</param>
		/// <param name="size">It is representing the row and column size of the Geraph.</param>
		static void SerialApplicationForAllPairShortestPath(int[,] graph, int size)
		{
			int[,] shortest_path_graph = new int[size, size];

			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
					shortest_path_graph[i, j] = graph[i, j];

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					for (int k = 0; k < size; k++)
					{
						if (shortest_path_graph[j, i] < INF && shortest_path_graph[i, k] < INF)
						{
							if (shortest_path_graph[j, i] + shortest_path_graph[i, k] < shortest_path_graph[j, k])
								shortest_path_graph[j, k] = shortest_path_graph[j, i] + shortest_path_graph[i, k];
						}
					}
				}
			}

			PrintGraph(shortest_path_graph, size);
		}
	}
}

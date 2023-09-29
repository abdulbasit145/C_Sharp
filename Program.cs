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
		const int INF = 99999;
		/*const int INF = int.MaxValue;*/

		/// <summary>
		/// Main entry point of the program
		/// </summary>
		/// <param name="args">Command line args</param>
		static void Main(string[] args)
		{
			/*int size = 5;
			int[,] graph = new int[size, size];

			GenerateRandomGraph(graph, size);

			Console.WriteLine("\t----- Initial Graph -----\n");
			PrintGraph(graph, size);*/

			int size = 5;
			int[,] graph = {
				{ 0,   5,  INF, 6, INF },
				{ INF, 0,   1, INF, 7 },
				{ 3, INF, 0, 4, INF},
				{ INF, INF, 2, 0,3 },
				{ 2, INF, INF, 5,0 }
			};
			PrintGraph(graph, size);


			Console.WriteLine("\t----- All Pair Shortest Path Graph -----\n");
			SerialApplicationForAllPairShortestPath(graph, size);


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
					// Generate a graph with random number between 1 and 20
					graph[i, j] = random.Next(1, 21);
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
						if (shortest_path_graph[j, i] + shortest_path_graph[i, k] < shortest_path_graph[j, k])
							shortest_path_graph[j, k] = shortest_path_graph[j, i] + shortest_path_graph[i, k];
					}
				}
			}

			PrintGraph(shortest_path_graph, size);
		}
	}
}

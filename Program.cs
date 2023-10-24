using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment_01
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
		static readonly int vertices = 4;
		static Thread[] threads;

		/// <summary>
		/// Main entry point of the program.
		/// </summary>
		/// <param name="args">Command line args</param>
		static void Main(string[] args)
		{
			int[,] graph = {
				{0,3,INF,5},
				{2,0,INF,4},
				{INF,1,0,INF},
				{INF,INF,2,0}
			};

			Console.WriteLine("\t----- Initial Graph -----\n");
			PrintGraph(graph);


			Console.WriteLine("\t----- All Pair Shortest Path Graphs -----\n");

			stopwatch.Start();
			SerializedAllPairShortestPath(graph);
			stopwatch.Stop();

			TimeSpan elapsed_time = stopwatch.Elapsed;
			Console.WriteLine($"Time taken by Serial Application : {elapsed_time.TotalMilliseconds} milliseconds");

			stopwatch.Reset();

			stopwatch.Start();
			ThreadedAllPairsShortestPath(graph);
			stopwatch.Stop();

			TimeSpan elapsed_time2 = stopwatch.Elapsed;
			Console.WriteLine($"Time taken by Threaded Application : {elapsed_time2.TotalMilliseconds} milliseconds");

			stopwatch.Reset();

			stopwatch.Start();
			ThreadPoolAllPairShortestPath(graph);
			stopwatch.Stop();

			TimeSpan elapsed_time3 = stopwatch.Elapsed;
			Console.WriteLine($"Time taken by Thread Pool Application : {elapsed_time3.TotalMilliseconds} milliseconds");

			Console.ReadKey();
		}

		/// <summary>
		/// Method <c>GenerateRandomGraph</c>  create a 2-D graph filled with random Integers.
		/// </summary>
		/// <param name="graph">It is a 2-D Array represnting graph.</param>
		static void GenerateRandomGraph(int[,] graph)
		{
			random = new Random();

			for (int i = 0; i < vertices; i++)
			{
				for (int j = 0; j < vertices; j++)
				{
					// Generate a graph with random number between 1 and 20 with 10% probability to add INF in between.
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
		static void PrintGraph(int[,] graph)
		{
			for (int i = 0; i < vertices; i++)
			{
				for (int j = 0; j < vertices; j++)
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
		/// Method <c>SerializedAllPairShortestPath</c> calculate the all pair shortest path using Flyod Warshall Algorithm.
		/// </summary>
		/// <param name="graph">It is a 2-D Array represnting graph.</param>
		static void SerializedAllPairShortestPath(int[,] graph)
		{
			int[,] shortest_path_graph = new int[vertices, vertices];

			for (int i = 0; i < vertices; i++)
				for (int j = 0; j < vertices; j++)
					shortest_path_graph[i, j] = graph[i, j];

			for (int k = 0; k < vertices; k++)
			{
				UpdateRow(shortest_path_graph, k);
			}

			PrintGraph(shortest_path_graph);
		}

		/// <summary>
		/// Method <c>ThreadedAllPairsShortestPath</c> calculate the all pair shortest path using Parrallel Flyod Warshall Algorithm using thread objects.
		/// </summary>
		/// <param name="graph">It is a 2-D Array represnting graph.</param>
		static void ThreadedAllPairsShortestPath(int[,] graph)
		{
			threads = new Thread[vertices];
			int[,] shortest_path_graph = new int[vertices, vertices];

			for (int i = 0; i < vertices; i++)
				for (int j = 0; j < vertices; j++)
					shortest_path_graph[i, j] = graph[i, j];

			for (int k = 0; k < vertices; k++)
			{
				int rowIndex = k;
				threads[k] = new Thread(() => UpdateRow(shortest_path_graph, rowIndex));
				threads[k].Start();
			}

			foreach (Thread thread in threads)
			{
				thread.Join();
			}

			PrintGraph(shortest_path_graph);
		}

		/// <summary>
		/// Method <c>ThreadPoolAllPairShortestPath</c> calculate the all pair shortest path using Parrallel Flyod Warshall Algorithm using thread pool.
		/// </summary>
		/// <param name="graph">It is a 2-D Array represnting graph.</param>
		static void ThreadPoolAllPairShortestPath(int[,] graph)
		{
			int[,] shortest_path_graph = new int[vertices, vertices];

			for (int i = 0; i < vertices; i++)
				for (int j = 0; j < vertices; j++)
					shortest_path_graph[i, j] = graph[i, j];

			Parallel.For(0, vertices, k => UpdateRow(shortest_path_graph, k));

			PrintGraph(shortest_path_graph);
		}

		/// <summary>
		/// Method <c>UpdateRow</c> calcullate the value for the Floyd  shortest distance matrix. 
		/// </summary>
		/// <param name="shortest_path_graph">It is a 2-D Array represnting Floyd  shortest distance graph.</param>
		/// <param name="k">It is the outer loop of Floyd Algorithm that handle the update o each row value.</param>
		static void UpdateRow(int[,] shortest_path_graph, int k)
		{
			for (int i = 0; i < vertices; i++)
			{
				for (int j = 0; j < vertices; j++)
				{
					if (shortest_path_graph[i, k] < INF && shortest_path_graph[k, j] < INF)
					{
						if (shortest_path_graph[i, k] + shortest_path_graph[k, j] < shortest_path_graph[i, j])
							shortest_path_graph[i, j] = shortest_path_graph[i, k] + shortest_path_graph[k, j];
					}
				}
			}
		}

	}
}

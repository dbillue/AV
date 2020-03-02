using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApiClient.Utils;

namespace WebApiClient
{
	class Program
	{
		// Create HttpClient ::)).
		private static readonly HttpClient client = new HttpClient();
		private static readonly File_IO file_IO = new File_IO()
		{
			FileName = "RepoListing.json",
			FilePath = Environment.CurrentDirectory
		};

		static async Task Main(string[] args)
		{
			// Full path to output file.
			file_IO.FullFilePath = file_IO.FilePath + @"\" + file_IO.FileName;
			var repoInfo = string.Empty;
			var data = string.Empty;

			// await ProcessRepositoriesNoRepoListReturn(file_IO.FullFilePath);
			
			#region // Async repository processing listing with return List<T>.
			var repos = await ProcessRepositories();

			foreach (var repo in repos)
			{
				repoInfo = "Repo Name: " + repo.Name
					+ Environment.NewLine 
					+ "\tURL: "	+ repo.Html_Url
					+ Environment.NewLine
					+ "\tDescription: " + repo.Description
					+ Environment.NewLine
					+ "\tForks: " + repo.Forks
					+ Environment.NewLine
					+ "\tForks: " + repo.Open_Issues
					+ Environment.NewLine
					+ "\tOwner.Login: " + repo.owner.Login
					+ Environment.NewLine
					+ "\tOwner.Login: " + repo.owner.Id
					+ Environment.NewLine
					+ "\tOwner.Login: " + repo.owner.Followers_Url
					+ Environment.NewLine
					+ "\tOwner.Permissions: " + repo.permissions.Admin
					+ Environment.NewLine
					+ "\tOwner.Permissions: " + repo.permissions.Pull
					+ Environment.NewLine
					+ "\tOwner.Permissions: " + repo.permissions.Push;

				Console.WriteLine(repoInfo);

				data += repoInfo + Environment.NewLine;
				file_IO.WriteFile(file_IO.FullFilePath, data);
			}
			Console.ReadLine();
			#endregion
		}

		private static async Task ProcessRepositoriesNoRepoListReturn(string fullFilePath)
		{
			var useString = bool.Parse(ConfigurationManager.AppSettings["UseString"]);
			var useStream = bool.Parse(ConfigurationManager.AppSettings["UseStream"]);
			var reposName = string.Empty;

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
			client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

			#region Use method GetStringAsync().
			if(useString)
			{
				var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
				var data = await stringTask;

				// Write to file.
				var dataWritten = file_IO.WriteFile(fullFilePath, data);

				// Write to console.
				Console.Write(data);
				Console.ReadLine();
			}
			#endregion

			#region Use method GetStreamAsync().
			if(useStream)
			{
				var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
				var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

				// Write to console.
				foreach (var repo in repositories)
				{
					Console.WriteLine("Repo Name: {0}", repo.Name);
					reposName += "Repo Name: " + repo.Name + Environment.NewLine;
				}

				// Write to file.
				var dataWritten = file_IO.WriteFile(fullFilePath, reposName);

				Console.ReadLine();
			}
			#endregion
		}

		private static async Task<List<Repository>> ProcessRepositories()
		{
			// Create client headers.
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
			client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

			// Use method GetStreamAsync().
			var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
			var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
			return repositories;
		}
	}

	internal class Repository
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("html_url")]
		public string Html_Url { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("forks")]
		public int Forks { get; set; }

		[JsonPropertyName("open_issues")]
		public int Open_Issues { get; set; }

		[JsonPropertyName("owner")]
		public Owner owner { get; set; }

		[JsonPropertyName("permissions")]
		public Permissions permissions { get; set; }
	}

	internal class Owner
	{
		[JsonPropertyName("login")]
		public string Login { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("followers_url")]
		public string Followers_Url { get; set; }
	}

	internal class Permissions
	{
		[JsonPropertyName("admin")]
		public bool Admin { get; set; }

		[JsonPropertyName("push")]
		public bool Push { get; set; }

		[JsonPropertyName("pull")]
		public bool Pull { get; set; }
	}
}

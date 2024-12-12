using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExpenseTracker.Utilities
{
	public static class JsonHelper
	{
		private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "expenses.json");

		public static async Task<List<ExpenseModel>> ReadExpenses()
		{
			try
			{
				if (!File.Exists(filePath))
				{
					return new List<ExpenseModel>();
				}
				var expenses = await File.ReadAllTextAsync(filePath);
				return JsonSerializer.Deserialize<List<ExpenseModel>>(expenses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while reading the expenses: {ex.Message}");
				return new List<ExpenseModel>();
			}
		}
		public static async Task WriteExpenses(List<ExpenseModel> expenses)
		{
			try
			{
				if (expenses == null) throw new ArgumentNullException(nameof(expenses));


				var options = new JsonSerializerOptions { WriteIndented = true };
				var json = JsonSerializer.Serialize(expenses, options);
				Directory.CreateDirectory(Path.GetDirectoryName(filePath));
				await File.WriteAllTextAsync(filePath, json);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while writing the expenses: {ex.Message}");
			}
		}
	}
}

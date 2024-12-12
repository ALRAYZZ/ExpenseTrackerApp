using ExpenseTracker.Models;
using ExpenseTracker.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess;

public static class DataController
{
	public static async Task AddExpense(string description, double amount)
	{
		List<ExpenseModel> listExpenses = await JsonHelper.ReadExpenses();

		ExpenseModel expense = new ExpenseModel();

		if (listExpenses.Count == 0)
		{
			expense.Id = 1;
		}
		else
		{
			expense.Id = listExpenses.Last().Id + 1;
		}
		expense.Description = description;
		expense.Amount = amount;

		listExpenses.Add(expense);
		await JsonHelper.WriteExpenses(listExpenses);
	}
	public static async Task UpdateExpense(int id, string description, double amount)
	{
		List<ExpenseModel> listExpenses = await JsonHelper.ReadExpenses();

		ExpenseModel? expense = listExpenses.FirstOrDefault(e => e.Id == id);
		
		if (expense != null)
		{
			expense.Description = description;
			expense.Amount = amount;
			await JsonHelper.WriteExpenses(listExpenses);
		}
		else
		{
			Console.WriteLine($"No expense with the ID: {id} found");
		}
		
		
	}
	public static async Task DeleteExpense(int id)
	{
		List<ExpenseModel> listExpenses = await JsonHelper.ReadExpenses();

		ExpenseModel? expense = listExpenses.FirstOrDefault(e => e.Id == id);

		if (expense != null)
		{
			listExpenses.Remove(expense);
			await JsonHelper.WriteExpenses(listExpenses);
		}
		else
		{
			Console.WriteLine($"No expense with the ID: {id} found");
		}
		
	}
	public static async Task ListExpenses()
	{
		List<ExpenseModel> listExpenses = await JsonHelper.ReadExpenses();

		if (listExpenses.Count == 0)
		{
			Console.WriteLine("No expenses found");
		}
		else
		{
			foreach (ExpenseModel expense in listExpenses)
			{
				Console.WriteLine($"ID: {expense.Id}, Date: {expense.Date}, Description: {expense.Description}, Amount: {expense.MoneyAmount}");
			}
		}
	}
	public static async Task GetTotalExpenses(int month)
	{
		List<ExpenseModel> listExpenses = await JsonHelper.ReadExpenses();
		double totalExpenses = listExpenses.Sum(e => e.Amount);

		if (month == 0)
		{
			Console.WriteLine($"Total Expenses are: ${totalExpenses}");
		}
		else
		{
			double totalExpensesForMonth = listExpenses.Where(e => e.Date.Month == month).Sum(e => e.Amount);
			Console.WriteLine($"Total expenses for {CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(month).ToUpper()}: ${totalExpensesForMonth}");
		}

	}
}

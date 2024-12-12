using ExpenseTracker.DataAccess;
using ExpenseTracker.Models;
using ExpenseTracker.Utilities;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers;

public static class CommandsController
{
	public static Command GetAddCommand()
	{
		var addCommand = new Command("add", "Command to add expense")
		{
			new Option<string>("--description", "The expense origin name"),
			new Option<double>("--amount", "The cost of the expense")
		};

		addCommand.SetHandler(async (string description, double amount) =>
		{
			Console.WriteLine($"Adding expense: {description}, Amount: {amount}");
			// Add logic to add expense here
			await DataController.AddExpense(description, amount);
			
		}, (Option<string>)addCommand.Options[0], (Option<double>)addCommand.Options[1]);

		return addCommand;
	}

	public static Command GetUpdateCommand()
	{
		var updateCommand = new Command("update", "Command to update expense")
		{
			new Option<int>("--id", "The expense ID to update"),
			new Option<string>("--description", "The expense origin name"),
			new Option<double>("--amount", "The cost of the expense")
		};

		updateCommand.SetHandler(async (int id, string description, double amount) =>
		{
			Console.WriteLine($"Updating expense ID: {id} - With: {description}, Amount: {amount}");
			// Add logic to update expense here
			await DataController.UpdateExpense(id, description, amount);
		}, (Option<int>)updateCommand.Options[0], (Option<string>)updateCommand.Options[1], (Option<double>)updateCommand.Options[2]);

		return updateCommand;
	}
	public static Command GetDeleteCommand()
	{
		var deleteCommand = new Command("delete", "Command to delete expense")
		{
			new Option<int>("--id", "The expense ID to delete"),
		};

		deleteCommand.SetHandler(async (int id) =>
		{
			Console.WriteLine($"Deleting expense ID: {id}");
			await DataController.DeleteExpense(id);
		}, (Option<int>)deleteCommand.Options[0]);

		return deleteCommand;
	}

	public static Command GetListCommand()
	{
		var listCommand = new Command("list", "Command to list all expenses");

		listCommand.SetHandler(async () =>
		{
			Console.WriteLine("Listing all expenses");
			await DataController.ListExpenses();
		});

		return listCommand;
	}

	public static Command GetSummaryCommand()
	{
		var summaryCommand = new Command("summary", "Command to show summary of expenses") 
		{
			new Option<int>("--month", "The month to show the summary for")
		};

		summaryCommand.SetHandler(async (int month) =>
		{
			Console.WriteLine("Showing summary of expenses");
			// Add logic to show summary here
			await DataController.GetTotalExpenses(month);

		}, (Option<int>)summaryCommand.Options[0]);

		return summaryCommand;
	}
}

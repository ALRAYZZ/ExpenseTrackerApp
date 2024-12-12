using ExpenseTracker.Controllers;
using System.CommandLine;
using System.CommandLine.Invocation;


// Define the root command and add subcommands
var rootCommand = new RootCommand
{
	CommandsController.GetAddCommand(),
	CommandsController.GetUpdateCommand(),
	CommandsController.GetDeleteCommand(),
	CommandsController.GetListCommand(),
	CommandsController.GetSummaryCommand()
};

rootCommand.Description = "A simple expense tracker app";

await rootCommand.InvokeAsync(args);




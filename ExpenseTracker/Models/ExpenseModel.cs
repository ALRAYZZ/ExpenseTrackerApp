using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpenseTracker.Models;

public class ExpenseModel
{
	
	public int Id { get; set; }
	public DateTime Date { get; set; } = DateTime.Now;
	public string Description { get; set; }
	public double Amount { get; set; }

	[JsonIgnore]
	public string MoneyAmount => $"${Amount}";
}

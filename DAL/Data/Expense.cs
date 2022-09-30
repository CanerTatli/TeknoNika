using System;
using System.Collections.Generic;

namespace NIKA.DAL
{
    public partial class Expense
    {
        public int Id { get; set; }
        public decimal? TechnicalInfrastructure { get; set; }
        public decimal? ElectricBills { get; set; }
        public decimal? WaterBills { get; set; }
        public decimal? EmployeeSalary { get; set; }
        public DateTime? ExpenseDate { get; set; }
    }
}

namespace NIKA.APII.Model
{
    public record ExpensesReportRow
    {
       

        public decimal? TechnicalInfrastructure { get; set; }
        public decimal? ElectricBills { get; set; }
        public decimal? WaterBills { get; set; }
        public decimal? EmployeeSalary { get; set; }
        public DateTime? ExpenseDate { get; set; }
    }
}

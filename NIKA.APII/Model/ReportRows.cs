namespace NIKA.APII.Model
{

    public record BestSellerReportRow
    {
        public string ProductName { get; set; } = string.Empty;
        public int SellingQuantity { get; set; }
    }
    public record ProductReportRow
    {
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public record SupplierReportRow
    {
        public string CompanyName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public DateTime SupplyDate { get; set; }
        public int SupplyQuantity { get; set; }
    }

    public record TotalSupplyReportRow
    {
        public string CompanyName { get; set; } = string.Empty;
        public int TotalSupply { get; set; }
    }

    public record CustomerProfileRow
    {
        public string ProductName { get; set; } = string.Empty;
        public int SellingCount { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string GenderMajority { get; set; } = string.Empty;
        public short AvarageAge { get; set; }
    }

    public record ExpensesReportRow
    {
        public decimal? TechnicalInfrastructure { get; set; }
        public decimal? ElectricBills { get; set; }
        public decimal? WaterBills { get; set; }
        public decimal? EmployeeSalary { get; set; }
        public DateTime? ExpenseDate { get; set; }
    }

    public record StockReportRow
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public short? UnitsInStock { get; set; }
        public bool IsCritic { get => UnitsInStock <= 5; }
        // bu şekilde set değil sadece get bir değer ve sadece okuma yaparken ufak bir if dönüyor içinde
    }

    public record EmployeeSalesReportRow
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int SaleCount { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public bool IsQuotaSuccessful
        {
            get
            {
                return TotalRevenue > 10;
            }
        }
        public decimal Bonus
        {
            get
            {
                if (IsQuotaSuccessful)
                {
                    return (TotalRevenue - 10) * 10 / 100;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}

using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIKA.APII.Model;
using NIKA.DAL;
using NIKA.DAL.Data;
using NIKA.DAL.Databases;
using System.Linq;


namespace NIKA.APII.Controllers
{
     [ApiController]
     [Route("Api/[controller]")]
    public class ReportsConroller : Controller
    {
        private readonly TeknoNIKAContext dbContext;
        private readonly NIKADapperContext _dapperContext;


        public ReportsConroller(TeknoNIKAContext dbContext, NIKADapperContext dapperContext)
        {
            _dapperContext = dapperContext;
            this.dbContext = dbContext;
        }


        [HttpGet("get-stockreport")]
       public IActionResult GetStockReport()
        {
           
            var products = dbContext.Products.Select(p => new StockReportRow() { 
                UnitsInStock = p.UnitsInStock, 
                ProductName = p.ProductName,  
                ProductID = p.Id
            }).ToList();

            //some additionas about reports
            return Ok(products);
        }

        [HttpGet("get-expensereport")]   
        public IActionResult GetExpenseReport()
        {
            var expenses = dbContext.Expenses.Select(p => new ExpensesReportRow()
            {
                WaterBills= p.WaterBills,
                ElectricBills= p.ElectricBills,
                TechnicalInfrastructure = p.TechnicalInfrastructure,
                EmployeeSalary= p.EmployeeSalary,
                ExpenseDate=p.ExpenseDate  
            }).ToList();

            return Ok(expenses);
        }


        [HttpGet("get-bestseller-report")]
        public async Task<IActionResult> GetBestSeller()
        {
            var query = @"
select ProductName,(count(ProductID)*Quantity) as 'SellingQuantity'
from ProductSales PS
inner join Products p on PS.ProductID=p.ID 
group by ProductName,Quantity 
order by SellingQuantity desc
            ";

            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QueryAsync<BestSellerReportRow>(query);

            return Ok(result);
        }


        [HttpGet("get-supplier-report")]
        public async Task<IActionResult> GetSupplierReport()
        {
            var query = @"
Declare @ThisYear int = YEAR(GETDATE());
Declare @ThisMonth int = MONTH(GETDATE());
Declare @StartOfThisMonth datetime = DATEFROMPARTS(@ThisYear, @ThisMonth, 1);
Declare @StartOfNextMonth datetime = DATEFROMPARTS(@ThisYear, @ThisMonth + 1, 1);

select 
	p.ID as ProductId,
	S.ID as CompanyId,
	CompanyName, 
	ProductName,
	SupplyQuantity,
	S.SupplyDate
from Products  p
right join Supplier s on p.SupplierID=s.ID
where s.SupplyDate between @StartOfThisMonth and @StartOfNextMonth
            ";

            var totalSupplyQuery = @"
select 
	CompanyName,
	sum(SupplyQuantity) as TotalSupply 
from Supplier 
group by CompanyName
            ";

            using var connection = _dapperContext.CreateConnection();
            var monthlySupplies = await connection.QueryAsync<SupplierReportRow>(query);
            var totalSupplyCounts = await connection.QueryAsync<TotalSupplyReportRow>(totalSupplyQuery);

            return Ok(new
            {
                monthlySupplies,
                totalSupplyCounts
            });
        }

        public record BestSellerReportRow
        {
            public string ProductName { get; set; } = string.Empty;
            public int SellingQuantity { get; set; }
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



    }
}

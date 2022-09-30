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
    [Route("reports")]
    public class ReportsConroller : Controller
    {
        private readonly TeknoNIKAContext dbContext;
        private readonly NIKADapperContext _dapperContext;


        public ReportsConroller(TeknoNIKAContext dbContext, NIKADapperContext dapperContext)
        {
            _dapperContext = dapperContext;
            this.dbContext = dbContext;
        }


        [HttpGet("get-stocks")]
        public IActionResult GetStockReport()
        {

            var products = dbContext.Products.Select(p => new StockReportRow()
            {
                UnitsInStock = p.UnitsInStock,
                ProductName = p.ProductName,
                ProductID = p.Id
            }).ToList();

            //some additionas about reports
            return Ok(products);
        }

        [HttpGet("get-expenses")]
        public IActionResult GetExpenseReport()
        {
            var expenses = dbContext.Expenses.Select(p => new ExpensesReportRow()
            {
                WaterBills = p.WaterBills,
                ElectricBills = p.ElectricBills,
                TechnicalInfrastructure = p.TechnicalInfrastructure,
                EmployeeSalary = p.EmployeeSalary,
                ExpenseDate = p.ExpenseDate
            }).ToList();

            return Ok(expenses);
        }


        [HttpGet("get-bestsellers")]
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


        [HttpGet("get-suppliers")]
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


        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            var query = @"
select 
	CategoryName,
	ProductName,
	UnitPrice,
	UnitsInStock 
from Products 
inner join Categories c on Products.CategoryID=c.ID
            ";

            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QueryAsync<ProductReportRow>(query);

            return Ok(result);
        }

        [HttpGet("get-customer-profile")]
        public async Task<IActionResult> GetCustomerProfile()
        {
            var query = @"
select TOP 10
	p.ProductName,
	(count(PS.ProductID)*PS.Quantity) as 'SellingCount',
	AVG(C.Age) as 'AverageAge',
	S.CompanyName,
	CASE WHEN C.Gender = 0 THEN 'Female' ELSE 'Male' END as 'GenderMajority'
from ProductSales PS
inner join Products p on PS.ProductID=p.ID 
inner join Supplier S on S.ID = P.SupplierID
inner join Customers C on C.ID = PS.CustomerID
group by ProductName,Quantity, S.CompanyName, C.Age, C.Gender
order by SellingCount desc
            ";

            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QueryAsync<CustomerProfileRow>(query);

            return Ok(result);
        }

        [HttpGet("get-employee-sales")]
        public async Task<IActionResult> GetEmployeeSales()
        {
            var countQuery = @"
SELECT 
	E.Id as EmployeeId,
	E.FirstName,
	E.Lastname,
	COUNT(PS.Id) as SaleCount,
	SUM(PS.Quantity) as TotalSales
FROM Employees E
LEFT JOIN ProductSales PS ON PS.EmployeeID = E.ID
LEFT JOIN Products P ON PS.ProductID = P.ID
GROUP BY E.ID, E.FirstName, E.Lastname
            ";

            var revenueQuery = @"
select
subq.EmployeeID,
SUM(subq.TotalSales) as TotalRevenue
from
(
select 
	PS.EmployeeID,
	PS.Quantity * P.UnitPrice as TotalSales
from ProductSales PS
inner join Products P on PS.ProductID = P.ID
group by PS.EmployeeID, PS.Quantity, P.UnitPrice
) subq
group by subq.EmployeeID

";

            using var connection = _dapperContext.CreateConnection();
            var countResult = await connection.QueryAsync<EmployeeSalesReportRow>(countQuery);
            var revenueResult = await connection.QueryAsync<EmployeeSalesReportRow>(revenueQuery);

            foreach(var revenueRow in revenueResult)
            {
                var countRow = countResult.Where(c => c.EmployeeId == revenueRow.EmployeeId).FirstOrDefault();
                if (countRow == null) continue;
                countRow.TotalRevenue = revenueRow.TotalRevenue;
            }

            return Ok(countResult);
        }

    }
}

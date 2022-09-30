
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIKA.DAL.Databases
{
    public class NIKADapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _conneectionString;

        public NIKADapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _conneectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_conneectionString);
    }
}

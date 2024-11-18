using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Data
{
    public static class DatabaseHelper
    {
        private static UchPr_Basharova_321Entities _connection = new UchPr_Basharova_321Entities();

        public static List<Employee> GetEmployees()
        {
            return _connection.Employee.ToList();
        }
    }
}

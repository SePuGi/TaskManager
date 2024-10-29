using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSqlLite
{
    public class SQLiteDBContext : DbContext
    {
        public static string DB_FILENAME { get { return $"{Directory.GetCurrentDirectory()}\\..\\TaskManager.db"; } }
        public static string script { get { return $"{Directory.GetCurrentDirectory()}\\..\\TaskManager.sql"; } }
        public static string DB_PATH { get { return "db"; } }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Data Source=c:\mydb.db;Version=3;
            optionsBuilder.UseSqlite($"Data Source={DB_FILENAME}");
        }

        public DbConnection GetConnection()
        {
            return Database.GetDbConnection();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbContext;

    public class MySqlDbContextOption:IMySqlDbContextOptionsFactory
    {
        public DbContextOptionsBuilder OptionsBuilder { get; set; }

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        public MySqlDbContextOption(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseLoggerFactory(loggerFactory);
            builder.EnableSensitiveDataLogging();
            builder.ConfigureWarnings(t => t.Default(WarningBehavior.Log));
            builder.UseMySQL(configuration.GetConnectionString("DefaultConnection")!);
            OptionsBuilder = builder;
        }
        #endregion
}


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbContext
{
    public interface IMySqlDbContextOptionsFactory
    {
        DbContextOptionsBuilder OptionsBuilder { get; }
    }
}

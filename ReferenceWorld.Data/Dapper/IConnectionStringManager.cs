using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceWorld.Data.Dapper
{
    public interface IConnectionStringManager
    {
        string ConnectionString { get; }
    }
}

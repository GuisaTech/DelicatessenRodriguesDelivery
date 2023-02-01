using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRD.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}

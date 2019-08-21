using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class AppViewVM
    {
        public VMBase CurrentVM { get; set; }
        public AppViewVM()
        {
            CurrentVM = new EmployeeConfiguratorVM(new OracleDB());
        }
    }
}

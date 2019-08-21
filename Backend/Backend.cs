using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public static class Backend
    {
        private static ICostumerDatabase _costumerDb = new CostumerDatabase() as ICostumerDatabase;
        public static ICostumerDatabase GetCostumerDatabase()
        {
            return _costumerDb;
        }
    }
}

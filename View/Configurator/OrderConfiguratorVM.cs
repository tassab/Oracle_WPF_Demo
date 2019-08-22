using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public class OrderConfiguratorVM : BaseConfiguratorVM
    {
        private enum State
        {
            SELECT_ORDER_TO_CONFIGURE
        };
        private State _state;

        public OrderConfiguratorVM(OracleDB db)
        {
            _db = db;
            ChangeState(State.SELECT_ORDER_TO_CONFIGURE);
        }

        protected override void OnForwardButton(object param)
        {
            Console.WriteLine("Forward!");
            switch (_state)
            {
                default:
                    break;
            }
        }
        protected override void OnBackButton(object param)
        {
            Console.WriteLine("Retreat...");
            switch (_state)
            {
                default:
                    break;
            }
        }

        private void ChangeState(State newState)
        {
            switch (newState)
            {
                default:
                    break;
            }
        }
    }
}

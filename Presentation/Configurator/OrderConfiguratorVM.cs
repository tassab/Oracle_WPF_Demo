using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TestApp
{
    public interface IOrderConfiguratorVMParent
    {
        void OnExitOrderConfigurator(object param);
    }

    public class OrderConfiguratorVM : BaseConfiguratorVM, IOrderSelectorAdderVMParent
    {
        private enum State
        {
            ORDER_LIST
        };
        private State _state;
        private IOrderConfiguratorVMParent _parent;

        public OrderConfiguratorVM(IOrderConfiguratorVMParent parent, OracleDB db, IMessageDialogService messageDialogService)
        {
            _db = db;
            _parent = parent;
            _messageDialogService = messageDialogService;
            ChangeState(State.ORDER_LIST);
        }

        protected override void OnForwardButton(object param)
        {
            switch (_state)
            {
                default:
                    break;
            }
        }
        protected override void OnBackButton(object param)
        {
            switch (_state)
            {
                case State.ORDER_LIST:
                    _parent.OnExitOrderConfigurator(this);
                    break;
                default:
                    break;
            }
        }

        private void ChangeState(State newState)
        {
            switch (newState)
            {
                case State.ORDER_LIST:
                    Content = new OrderSelectorAdderVM(this, _db);
                    ForwardButtonText = "Configure Order";
                    BackButtonText = "Back";
                    Title = "Select Order to Configure";
                    _state = State.ORDER_LIST;
                    break;
                default:
                    break;
            }
        }

        public void OnAddNewOrder(object param)
        {
            throw new NotImplementedException();
        }

        public void OnRemoveOrder(object param)
        {
            var vm = Content as OrderSelectorVM;
            if (vm == null) { Console.WriteLine("This shouldnt happen"); return; }
            if (vm.SelectedOrder == null) { return; }
            if (_messageDialogService.ShowYesNoDialog(
                $"Do you want to remove the order with ID={vm.SelectedOrder.Order_Id} from the database?",
                "Confirm removal")
                == MessageDialogResult.No)
            {
                return;
            }
            _db.Orders.Remove(vm.SelectedOrder);
            _db.SaveChanges();
        }
    }
}

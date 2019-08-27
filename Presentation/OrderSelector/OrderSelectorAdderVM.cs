using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public interface IOrderSelectorAdderVMParent
    {
        void OnAddNewOrder(object param);
        void OnRemoveOrder(object param);
    }
    public class OrderSelectorAdderVM : OrderSelectorVM
    {
        private ICommand _addNewOrder;
        private ICommand _removeOrder;
        private IOrderSelectorAdderVMParent _parent;

        public ICommand AddNewOrder { get { return _addNewOrder ?? new RelayCommand(OnAddNewOrder); } set { _addNewOrder = value; } }
        public ICommand RemoveOrder { get { return _removeOrder ?? new RelayCommand(OnRemoveOrder); } set { _removeOrder = value; } }
        public OrderSelectorAdderVM(IOrderSelectorAdderVMParent parent, OracleDB db): base(db)
        {
            _parent = parent;
        }
        private void OnAddNewOrder(object param)
        {
            _parent.OnAddNewOrder(param);
        }
        private void OnRemoveOrder(object param)
        {
            _parent.OnRemoveOrder(param);
            OnPropertyChanged("SearchQuery");
        }
    }
}

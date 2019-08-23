using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TestApp
{
    public class OrderSelectorVM : VMBase
    {
        private OracleDB _db;
        private string _search_Query;
        private Order _selectedOrder;
        private string _selectedOrderInfo;
        private List<Order> _orderList;


        public string SearchQuery { get { return _search_Query; } set { SetProperty(ref _search_Query, value); } }
        public Order SelectedOrder { get { return _selectedOrder; } set { SetProperty(ref _selectedOrder, value); } }
        public string SelectedOrderInfo { get { return _selectedOrderInfo; } set { SetProperty(ref _selectedOrderInfo, value); } }
        public List<Order> OrderList { get { return _orderList; } set { SetProperty(ref _orderList, value); } }

        public OrderSelectorVM(OracleDB db)
        {
            _db = db;
            OrderList = _db.Orders.ToList();
            PropertyChanged += RespondToPropertyChanged;
        }

        private void RespondToPropertyChanged(object sender, PropertyChangedEventArgs a)
        {
            if (a.PropertyName == "SelectedOrder")
            {
                SelectedOrderInfo = SelectedOrder == null ? "" :
                    $"Order ID: {SelectedOrder.Order_Id}\n" +
                    $"Customer: {SelectedOrder.Customer.Name}\n" +
                    $"{SelectedOrder.Items.Count} products\n" +
                    $"Date: {SelectedOrder.Date.ToString("dd-MM-yyyy")}\n" +
                    $"Status: {SelectedOrder.Status}";
            }
            else if (a.PropertyName == "SearchQuery")
            {
                if (SearchQuery != null && SearchQuery != "")
                {
                    OrderList = _db.Orders.ToList() // does not work on entity level, have to get the whole list first...
                        .Where(o =>
                            o.Order_Id.ToString() == SearchQuery
                            || o.Customer.Name.ToLower().Contains(SearchQuery.ToLower())
                            || o.Items.Any(i => i.Product.Name.ToLower().Contains(SearchQuery.ToLower())))
                        .ToList();
                }
                else
                {
                    OrderList = _db.Orders.ToList();
                }
            }
        }
    }
}

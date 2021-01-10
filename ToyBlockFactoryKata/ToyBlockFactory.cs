namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private Order _customerOrder;
        private OrderManagementSystem _orderManagementSystem = new OrderManagementSystem();
        public Order CreateOrder(string customerName, string customerAddress)
        {
            _customerOrder = new Order {Name = customerName, Address = customerAddress};
            return _customerOrder;
        }

        public void SubmitOrder(Order customerOrder)
        {
            _orderManagementSystem.SetOrder(customerOrder);
        }

        public Order GetOrder(string orderId)
        {
            _orderManagementSystem.GetOrder(orderId); //Is calling GetOrder() twice bad?
            throw new System.NotImplementedException();
        }
    }
}
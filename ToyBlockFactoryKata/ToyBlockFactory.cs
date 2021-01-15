namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private Order _customerOrder;
        private readonly OrderManagementSystem _orderManagementSystem = new OrderManagementSystem();

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
            //should this also return the reports? should we ask customer what report they want?
            
            return _orderManagementSystem.GetOrder(orderId); //Is calling GetOrder() twice bad?
        }
        
        //once user selected what report they want, call that particular method
            // this method will call private GetOrder() ???
    }
}
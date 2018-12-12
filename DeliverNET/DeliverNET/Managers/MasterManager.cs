using DeliverNET.Managers.Interfaces;

namespace DeliverNET.Managers
{
    /// <summary>
    /// Contains methods to return every manager.
    /// </summary>
    public class MasterManager : IMasterManager
    {
        private readonly IOrderManager _orderManager;
        private readonly IDelivererManager _delivererManager;
        private readonly IBusinessManager _businessManager;
        private readonly IBusinessCashierManager _businessCashierManager;

        public MasterManager(
            IOrderManager orderManager,
            IDelivererManager delivererManager,
            IBusinessManager businessManager,
            IBusinessCashierManager businessCashierManager)
        {
            _orderManager = orderManager;
            _delivererManager = delivererManager;
            _businessManager = businessManager;
            _businessCashierManager = businessCashierManager;
        }

        public IBusinessCashierManager GetBusinessCashierManager() => _businessCashierManager;
        public IBusinessManager GetBusinessManager() =>_businessManager;
        public IDelivererManager GetDelivererManager() => _delivererManager;
        public IOrderManager GetOrderManager() => _orderManager;
    }
}

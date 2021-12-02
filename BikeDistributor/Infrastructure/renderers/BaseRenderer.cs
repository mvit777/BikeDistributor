using BikeDistributor.Domain.Models;


namespace BikeDistributor.renderers
{
    public class BaseRenderer
    {
        protected Order _order = null;

        public BaseRenderer(Order order)
        {
            _order = order;
        }
    }
}

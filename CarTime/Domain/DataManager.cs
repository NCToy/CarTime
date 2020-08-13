using CarTime.Domain.Repositories.Abstract;

namespace CarTime.Domain
{
    public class DataManager
    {
        public ICarItemsRepository CarItems { get; set; }
        public IBrandItemsRepository BrandItems { get; set; }
        public IOrderRepository Orders { get; set; }

        public DataManager(ICarItemsRepository carItems, IBrandItemsRepository brandItems, IOrderRepository orders)
        {
            CarItems = carItems;
            BrandItems = brandItems;
            Orders = orders;
        }
    }
}

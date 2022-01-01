namespace CarShop.Service.Common.Base
{
    using CarShop.Data;

    public abstract class BaseService
    {
        protected readonly CarShopDbContext db;

        public BaseService(CarShopDbContext db)
        {
            this.db = db;
        }
    }
}
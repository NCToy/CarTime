using System;
using System.Linq;
using CarTime.Domain.Entities;

namespace CarTime.Domain.Repositories.Abstract
{
    public interface IBrandItemsRepository
    {
        IQueryable<BrandItem> GetBrandItems();
        BrandItem GetBrandItemByTitle(string Title);
        BrandItem GetBrandItemById(Guid Id);
        void SaveBrandItem(BrandItem entity);
        void DeleteBrandItem(Guid id);
    }
}

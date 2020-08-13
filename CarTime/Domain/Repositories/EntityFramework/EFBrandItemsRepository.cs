using System;
using System.Linq;
using CarTime.Domain.Entities;
using CarTime.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CarTime.Domain.Repositories.EntityFramework
{
    public class EFBrandItemsRepository : IBrandItemsRepository
    {
        private readonly AppDbContext context;

        public EFBrandItemsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<BrandItem> GetBrandItems()
        {
            return context.BrandItems;
        }

        public BrandItem GetBrandItemByTitle(string Title)
        {
            return context.BrandItems.FirstOrDefault(x => x.Title == Title);
        }

        public BrandItem GetBrandItemById(Guid id)
        {
            return context.BrandItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveBrandItem(BrandItem entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public void DeleteBrandItem(Guid id)
        {
            context.BrandItems.Remove(new BrandItem() { Id = id });
            context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarTime.Domain.Entities;
using CarTime.Domain.Repositories.Abstract;

namespace CarTime.Domain.Repositories.EntityFramework
{
    public class EFCarItemsRepository : ICarItemsRepository
    {
        private readonly AppDbContext context;

        public EFCarItemsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<CarItem> GetAvailableCars()
        {
            List<CarItem> avCars = context.CarItems.Where(c => c.Available == true).ToList();
            return avCars;
        }

        public IQueryable<CarItem> GetCarsByBrand(string title)
        {
            return context.CarItems.Where(x=>x.Brand.Title == title);
        }

        public CarItem GetCarById(Guid id)
        {
            return context.CarItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveCar(CarItem entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteCar(Guid id)
        {
            context.CarItems.Remove(new CarItem() {Id = id});
            context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using CarTime.Domain.Entities;

namespace CarTime.Domain.Repositories.Abstract
{
    public interface ICarItemsRepository
    {
        List<CarItem> GetAvailableCars();
        IQueryable<CarItem> GetCarsByBrand(string title);
        CarItem GetCarById(Guid Id);
        void SaveCar(CarItem entity);
        void DeleteCar(Guid id);
    }
}

using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in filter == null ? context.Cars : context.Cars.Where(filter)
                             join co in context.Colors on c.ColorId equals co.ColorId
                             join b in context.Brands on c.BrandId equals b.BrandId
                             select new CarDetailDto { 
                                 CarId = c.CarId, 
                                 BrandName = b.BrandName, 
                                 ColorName = co.ColorName, 
                                 ModelName = c.ModelName, 
                                 ModelYear = c.ModelYear, 
                                 DailyPrice = c.DailyPrice, 
                                 Description = c.Description,
                                 MinFindeksScore = c.MinFindeksScore
                             };
                return result.ToList();

            }
        }
    }
}

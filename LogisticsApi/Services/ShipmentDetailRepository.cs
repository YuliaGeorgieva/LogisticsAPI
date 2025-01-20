using LogisticsApi.Enums;
using LogisticsApi.Model;
using LogisticsApi.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApi.Services
{
    public class ShipmentDetailRepository : GenericRepository<ShipmentDetail>, IShipmentDetailRepository
    {
        public ShipmentDetailRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<ShipmentDetail> AddShipmentDetail(ShipmentDetail shipmentDetail)
        {
            return await Add(shipmentDetail);
        }

        public long[] GetShipmentStatusCount()
        {

            long openCount= GetAll(x=> !x.IsDeleted && x.ShipmentStatusId== (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus),"0")).LongCount();
            long readyCount= GetAll(x=> !x.IsDeleted && x.ShipmentStatusId== (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus),"1")).LongCount();
            long inTransitCount= GetAll(x=> !x.IsDeleted && x.ShipmentStatusId== (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus),"2")).LongCount();
            long reachedCount= GetAll(x=> !x.IsDeleted && x.ShipmentStatusId== (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus),"3")).LongCount();
            long deliveredCount= GetAll(x=> !x.IsDeleted && x.ShipmentStatusId== (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus),"4")).LongCount();
            long completedCount= GetAll(x=> !x.IsDeleted && x.ShipmentStatusId== (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus),"5")).LongCount();

            return new long[6] { openCount, readyCount , inTransitCount, reachedCount, deliveredCount, completedCount };
        }
    }
}

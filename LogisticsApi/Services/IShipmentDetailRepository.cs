using LogisticsApi.Model;

namespace LogisticsApi.Services
{
    public interface IShipmentDetailRepository
    {
        Task<ShipmentDetail> AddShipmentDetail(ShipmentDetail shipment);
        long[] GetShipmentStatusCount();
    }
}
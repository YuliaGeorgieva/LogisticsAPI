using LogisticsApi.Model;

namespace LogisticsApi.Services
{
    public interface IShipmentRepository
    {
        Task<Shipment> AddShipment(Shipment shipment);
        Task<Shipment> UpdateShipment(Shipment shipment);
        Task<Shipment?> GetShipmentById(int Id);
        Task<bool> DeleteShipmentById(int Id);
        Task<List<Shipment>> GetAllShipments();
        Task<List<Shipment>> GetLatestShipments();
        Task<Shipment?> GetShipmentByTrackingNumber(string trackingNumber);

        Task<long> GetAllShipmentsCount();
    }
}

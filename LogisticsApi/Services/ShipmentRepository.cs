using LogisticsApi.Model;
using LogisticsApi.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApi.Services
{
    public class ShipmentRepository : GenericRepository<Shipment>, IShipmentRepository
    {
        public ShipmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Shipment> AddShipment(Shipment shipment)
        {
            return await Add(shipment);
        }

        public async Task<bool> DeleteShipmentById(int Id)
        {
            try
            {
                var result = await GetById(Id);
                result.IsDeleted = true;
                await Update(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
            return true;
        }

        public async Task<List<Shipment>> GetAllShipments()
        {
            return await GetAll(x => !x.IsDeleted).Include(x => x.ShipmentDetails).ToListAsync();
        }
        public async Task<List<Shipment>> GetLatestShipments()
        {
            return await GetAll(x => !x.IsDeleted).OrderByDescending(x=>x.CreatedAt).Take(10).ToListAsync();
        }
        public async Task<long> GetAllShipmentsCount()
        {
            var allShipment= await GetAll(x => !x.IsDeleted).ToListAsync();
            return allShipment.LongCount();
        }
        public async Task<Shipment?> GetShipmentById(int Id)
        {
            return await GetAll(x => !x.IsDeleted && x.Id == Id).Include(x=>x.ShipmentDetails).SingleOrDefaultAsync();
        }
        public async Task<Shipment?> GetShipmentByTrackingNumber(string trackingNumber)
        {
            return await GetAll(x => !x.IsDeleted && x.TrackingNumber == trackingNumber).Include(x => x.ShipmentDetails).SingleOrDefaultAsync();
        }

        public async Task<Shipment> UpdateShipment(Shipment shipment)
        {
            return await Update(shipment);
        }
    }
}

using LogisticsApi.Dtos;
using LogisticsApi.Helpers;
using LogisticsApi.Model;
using LogisticsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/*
    // Get the course where currently DepartmentID = 2.
    Course course = context.Courses.First(c => c.DepartmentID == 2);

    // Use DepartmentID foreign key property
    // to change the association.
    course.DepartmentID = 3;

    // Load the related Department where DepartmentID = 3
    context.Entry(course).Reference(c => c.Department).Load();
 */
namespace LogisticsApi.Controllers
{
    //    [Authorize(Roles = "superadmin,employee")]

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IShipmentDetailRepository _shipmentDetailRepository;
        public ShipmentController(IShipmentRepository shipmentRepository, IShipmentDetailRepository shipmentDetailRepository)
        {
            _shipmentRepository = shipmentRepository;
            _shipmentDetailRepository = shipmentDetailRepository;
        }
        [HttpGet]
        [Route("GetLatestShipments")]
        public async Task<IActionResult> GetLatestShipments()
        {
            return Ok(await _shipmentRepository.GetLatestShipments());
        }

        [HttpGet]
        [Route("GetAllShipments")]
        public async Task<IActionResult> GetAllShipments()
        {
            return Ok(await _shipmentRepository.GetAllShipments());
        }

        [HttpGet]
        [Route("GetShipmentStatusCount")]
        public IActionResult GetShipmentStatusCount()
        {
            return Ok(_shipmentDetailRepository.GetShipmentStatusCount());
        }


        [HttpGet]
        [Route("GetAllShipmentsCount")]
        public async Task<IActionResult> GetAllShipmentsCount()
        {
            return Ok(await _shipmentRepository.GetAllShipmentsCount());
        }

        [HttpGet]
        [Route("GetShipmentById/{id}")]
        public async Task<IActionResult> GetShipment(int id)
        {
            if (id <= 0)
                return BadRequest();

            var shipment = await _shipmentRepository.GetShipmentById(id);
            if (shipment == null)
                return NotFound();

            return Ok(shipment);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("TrackShipment/{trackingNumber}")]
        public async Task<IActionResult> TrackShipment(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
                return BadRequest();

            var shipment = await _shipmentRepository.GetShipmentByTrackingNumber(trackingNumber);
            if (shipment == null)
                return NotFound();

            return Ok(shipment);
        }

        [HttpPost]
        [Route("AddShipment")]
        public async Task<IActionResult> AddShipment(ShipmentDto model)
        {
            Shipment shipment = new Shipment()
            {
                TrackingNumber = Common.GenerateTrackingNumber(),
                BranchId = model.BranchId,
                CourierId = model.CourierId,
                CreatedAt = DateTime.Now,
                CreatedBy = model.CreatedBy,
                DeliveredDate = model.DeliveredDate,
                DeliveryAddress = model.DeliveryAddress,
                DeliveryCost = model.DeliveryCost,
                Description = model.Description,
                Discount = model.Discount,
                EstimatedDeliveryDate = model.EstimatedDeliveryDate,
                FinalPrice = model.FinalPrice,
                IsDeleted = false,
                PaidAmount = model.PaidAmount,
                PaymentStatus = model.PaymentStatus,
                Quantity = model.Quantity,
                ReceiverAddress = model.ReceiverAddress,
                ReceiverCity = model.ReceiverCity,
                ReceiverCountry = model.ReceiverCountry,
                ReceiverEmail = model.ReceiverEmail,
                ReceiverName = model.ReceiverName,
                ReceiverPhoneNumber = model.ReceiverPhoneNumber,
                ReceiverState = model.ReceiverState,
                SenderAddress = model.SenderAddress,
                SenderCity = model.SenderCity,
                SenderCountry = model.SenderCountry,
                SenderEmail = model.SenderEmail,
                SenderName = model.SenderName,
                SenderPhoneNumber = model.SenderPhoneNumber,
                SenderState = model.SenderState,
                ShippingAddress = model.ShippingAddress,
                ShippingDate = model.ShippingDate,
                Weight = model.Weight,
            };

            var createdShipment = await _shipmentRepository.AddShipment(shipment);

            ShipmentDetail shipmentDetail = new ShipmentDetail()
            {
                AddedBy = model.CreatedBy,
                AddedDate = DateTime.Now,
                IsDeleted = false,
                ShipmentId = createdShipment.Id,
                Message = "Shipment Registered",
                ShipmentStatusId = 0,
                TrackingNumber = createdShipment.TrackingNumber,
            };

            var updatedShipment = await _shipmentDetailRepository.AddShipmentDetail(shipmentDetail);

            return Ok(createdShipment);
        }

        [HttpPut]
        [Route("UpdateShipment")]
        public async Task<IActionResult> UpdateShipment(ShipmentDetailsDto model)
        {
            if (model == null || model.ShipmentId == 0)
                return BadRequest();

            var result = await _shipmentRepository.GetShipmentById(model.ShipmentId);
            if (result == null)
                return NotFound();


            ShipmentDetail shipmentDetail = new ShipmentDetail()
            {
                AddedBy = model.AddedBy,
                AddedDate = DateTime.Now,
                IsDeleted = false,
                ShipmentId = model.ShipmentId,
                Message = model.Message,
                ShipmentStatusId = model.ShipmentStatusId,
                TrackingNumber = result.TrackingNumber,
            };

            var updatedShipment = await _shipmentDetailRepository.AddShipmentDetail(shipmentDetail);
            return Ok(updatedShipment);
        }

        [HttpDelete]
        [Route("DeleteShipmentById/{id}")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            if (id < 1)
                return BadRequest();
            var result = await _shipmentRepository.GetShipmentById(id);
            if (result == null)
                return NotFound();
            var isDeleted = await _shipmentRepository.DeleteShipmentById(id);
            return Ok(isDeleted);
        }


    }
}

using BL.Dtos;
using BL.Dtos.Payment;
using BL.Services.Implementation.ShipmentService.ManageState;
using BL.Services.Interfaces;
using BL.Services.Interfaces.IShipment;
using BL.Services.Interfaces.IShipment.IManageStatue;
using DAL.Exceptions;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLiberary.Common;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ShipmentsController : ControllerBase
    {
        IShipmentStateHandlerFactory _shipmentStateHandlerFactory;
        IShipmentCommand _ShipmentCommand;
        IShipmentQuery _ShipmentQuery;
        ILogger<ShipmentsController> _logger;
        public ShipmentsController(IShipmentCommand ShipmentCommand, IShipmentQuery ShipmentQuery, ILogger<ShipmentsController> logger,
            IShipmentStateHandlerFactory shipmentStateHandlerFactory)
        {
            _ShipmentCommand = ShipmentCommand;
            _ShipmentQuery = ShipmentQuery;
            _logger = logger;
            _shipmentStateHandlerFactory = shipmentStateHandlerFactory;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PageResulet<ShipmentDto>>>>List(int page=1)
        {
    
            try
            {
                bool onlyCurrentUser = true;
                ShipmentstatuesEnum? status = null;

                if (User.IsInRole("Admin"))
                {
                    onlyCurrentUser = false;
                    status = null;
                }
                else if (User.IsInRole("Reviwer"))
                {
                    onlyCurrentUser = false;
                    status = ShipmentstatuesEnum.Created;
                }
                else if (User.IsInRole("Operation"))
                {
                    onlyCurrentUser = false;
                    status = ShipmentstatuesEnum.Approved;
                }
                else if (User.IsInRole("OperationManger"))
                {
                    onlyCurrentUser = false;
                    status = ShipmentstatuesEnum.ReadyForShip;
                }
                else
                {
                    onlyCurrentUser = true;
                    status = null;
                }
           

                var data = await _ShipmentQuery.GetShipments(page, 3, onlyCurrentUser, status);

                return Ok(ApiResponse<PageResulet<ShipmentDto>>.SuccessResponse(
                    data, "Shipment retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll Shipment");
                return StatusCode(500,ApiResponse<PageResulet<ShipmentDto>>.FailResponse("An error occurred while retrieving Shipment."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll Shipment");

                return StatusCode(500,ApiResponse<PageResulet<ShipmentDto>>.FailResponse("An error occurred while retrieving Shipment ."));
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ShipmentDto>>> Show(Guid id)
        {
            try
            {
                var data = await _ShipmentQuery.GetShipment(id);
                return Ok(ApiResponse<ShipmentDto>.SuccessResponse(data, "Shipment retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500,ApiResponse<ShipmentDto>.FailResponse("An error occurred while retrieving Shipment ."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500,ApiResponse<ShipmentDto>.FailResponse("An error occurred while retrieving Shipment ."));
            }


        }
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ShipmentDto data)
        {
            if (data == null)
            {
                return BadRequest(ApiResponse<string>.FailResponse("Shipment data is required."));
            }

            try
            {
                Guid shipmentId = await _ShipmentCommand.Create(data);

                if (shipmentId == Guid.Empty)
                {
                    return StatusCode(500,
                        ApiResponse<string>.FailResponse("Failed to create shipment."));
                }

                return Ok(ApiResponse<Guid>.SuccessResponse(shipmentId, "Shipment created successfully."));
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return StatusCode(500,
                    ApiResponse<string>.FailResponse("An error occurred while creating the shipment.", errors));
            }
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromBody] ShipmentDto data)
        {
            if (data == null)
            {
                return BadRequest(ApiResponse<string>.FailResponse(" Faild TO Edit"));
            }

            try
            {
                // انتظر نتيجة الخدمة بدل إرجاع الـ Task نفسه
                var result = await _ShipmentCommand.Edit(data); // result: bool

                if (!result)
                {
                    return BadRequest(ApiResponse<string>.FailResponse(" Faild TO Edit"));
                }

                return Ok(ApiResponse<object>.SuccessResponse(result, "Shipment Updated successfully."));
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return StatusCode(500,
                    ApiResponse<string>.FailResponse("An error occurred while Updated the shipment.", errors));
            }
        }


        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ShipmentDto data)
        {
            try
            {
                ShipmentstatuesEnum targetStatus = (ShipmentstatuesEnum)data.CurrentState;

                var result =  _shipmentStateHandlerFactory.GetHandler(targetStatus);
                await result.HandleState(data);

                return Ok(ApiResponse<object>.SuccessResponse("change status successfully."));
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return StatusCode(500,
                    ApiResponse<string>.FailResponse("An error occurred while updating the shipment to Shipped.", errors));
            }
        }


        //[HttpPost("MarkPaid")]
        //public async Task<IActionResult> MarkPaid([FromBody] ShipmentDto dto)
        //{
        //    if (dto == null || dto.Id == Guid.Empty)
        //        return BadRequest(ApiResponse<string>.FailResponse("ShipmentId is required."));

        //    await _ShipmentCommand.EditFields(dto.Id, s =>
        //    {
        //        s.IsPaid = true;
        //        s.PaymentGateway = dto.PaymentGateway;
        //        s.PaymentReference = dto.PaymentReference;
        //    });

        //    Console.WriteLine($"MarkPaid => Id={dto.Id}, Gateway={dto.PaymentGateway}, Ref={dto.PaymentReference}");

        //    return Ok(ApiResponse<string>.SuccessResponse("Shipment marked as paid successfully."));
        //}



// ... (باقي الكلاس)

[AllowAnonymous] // *** مهم: Webhook لا يتطلب مصادقة ***
    [HttpPost("paymob-webhook-confirm")]
    public async Task<IActionResult> PaymobWebhook([FromForm] IFormCollection data)
    {
        // 1. (موصى به) التحقق من الـ HMAC (التوقيع الرقمي) لضمان مصدر الطلب
        // يجب تطبيق منطق التحقق من HMAC هنا باستخدام Paymob:HMACSecret
        // (سنفترض تخطيها مؤقتاً للتركيز على تحديث الحالة، لكن يجب تنفيذها في الإنتاج)

        // Paymob Webhook يرسل البيانات كـ Form data

        // 2. تحليل البيانات
        var successParam = data.Keys.Contains("success") ? data["success"].ToString() : null;
        var orderIdParam = data.Keys.Contains("order") ? data["order"].ToString() : null;
        var transactionIdParam = data.Keys.Contains("id") ? data["id"].ToString() : null;
        var pendingParam = data.Keys.Contains("pending") ? data["pending"].ToString() : null;

        // حالة نجاح العملية (success='true' AND pending='false')
        var isTransactionSuccessful = successParam == "true" && pendingParam == "false";

        Console.WriteLine($"Paymob Webhook Received: Success={successParam}, Order={orderIdParam}, TxnId={transactionIdParam}");

        if (isTransactionSuccessful &&
            Guid.TryParse(orderIdParam, out var shipmentId) &&
            !string.IsNullOrEmpty(transactionIdParam))
        {
            // 3. تحديث حالة الشحنة
            try
            {
                await _ShipmentCommand.EditFields(shipmentId, s =>
                {
                    s.IsPaid = true;
                    s.PaymentGateway = "Paymob";
                    s.PaymentReference = transactionIdParam;
                });

                Console.WriteLine($"Webhook Success: Shipment {shipmentId} marked as paid.");
            }
            catch (Exception ex)
            {
                // سجل الخطأ إذا فشل تحديث قاعدة البيانات
                Console.Error.WriteLine($"Error marking shipment {shipmentId} paid: {ex.Message}");
                // لا تزال ترد بـ OK حتى لا تعيد Paymob الإرسال
            }

            // 4. الرد بـ 200 OK لتأكيد استلام الإشعار
            return Ok();
        }

        // إذا كانت العملية فاشلة أو معلقة أو غير صالحة، نرد أيضاً بـ 200 OK
        return Ok();
    }
}
}

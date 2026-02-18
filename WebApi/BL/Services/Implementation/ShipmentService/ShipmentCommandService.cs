using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Implementation.ShipmentService.ManageState;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using BL.Services.Interfaces.IShipment;
using DAL.Repositories.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation.ShipmentService;

public class ShipmentCommandService : GenericService<Shipment, ShipmentDto>, IShipmentCommand
{
    IUserReciverService _userReceiver;
    IUserSenderService _userSender;
    ITrackingNumberCreatorService _trackingCreator;
    ICalculateRateService _rateCalculator;
    IUnitOfWork _uow;
    IUserService _userService;
    IGenericRepository<Shipment> _repo;
    IMappingService _mapper;
    IShipmentStatusService _shipmentStatus;
    public ShipmentCommandService(IGenericRepository<Shipment> repo, IMappingService mapper,
           IUserService userService, IUserReciverService userReceiver,
           IUserSenderService userSender, ITrackingNumberCreatorService trackingCreator
          , ICalculateRateService rateCalculator, IShipmentStatusService shipmentStatus, IUnitOfWork uow) : base(uow, mapper, userService)
    {
        _uow = uow;
        _repo = repo;
        _mapper = mapper;
        _userReceiver = userReceiver;
        _userSender = userSender;
        _trackingCreator = trackingCreator;
        _rateCalculator = rateCalculator;
        _userService = userService;
        _shipmentStatus = shipmentStatus;
    }
    // بدلاً من Task<bool>
    public async Task<Guid> Create(ShipmentDto dto)
    {
        try
        {
            await _uow.BeginTransactionAsync();

            dto.TrackingNumber = _trackingCreator.GenerateTrackingNumber(dto);
            dto.ShipingRate = _rateCalculator.CalculateRate(dto);

            var userId = _userService.GetLoggedInUser();

            // Sender
            if (dto.SenderId == Guid.Empty)
            {
                dto.SenderData.UserId = userId;
                var senderResulet = await _userSender.Add(dto.SenderData);
                dto.SenderId = senderResulet.Item2;
            }

            // Receiver
            if (dto.ReceiverId == Guid.Empty)
            {
                dto.ReciverData.UserId = userId;
                var reciverResulet = await _userReceiver.Add(dto.ReciverData);
                dto.ReceiverId = reciverResulet.Item2;
            }

            dto.IsPaid = false;
            dto.PaymentGateway = null;
            dto.PaymentReference = null;

            dto.CurrentState = (int)ShipmentstatuesEnum.Created;

            // Add الشحنة وترجع (success, id)
            var addResult = await Add(dto);        // addResult : (bool, Guid)
            var success = addResult.Item1;
            var shipmentId = addResult.Item2;

            if (!success || shipmentId == Guid.Empty)
            {
                await _uow.RollbackAsync();
                return Guid.Empty;
            }

            await _shipmentStatus.Add(shipmentId, ShipmentstatuesEnum.Created);

            await _uow.CommitAsync();
            return shipmentId;
        }
        catch
        {
            await _uow.RollbackAsync();
            return Guid.Empty;
        }
    }

    public async Task<bool> Edit(ShipmentDto dto)
    {
        try
        {
            await _uow.BeginTransactionAsync();

            dto.ShipingRate = _rateCalculator.CalculateRate(dto);

            // نربط الـ DTO بالـ Ids الموجودة
            dto.SenderData.Id = dto.SenderId;
            dto.ReciverData.Id = dto.ReceiverId;

            // تحديث المرسل والمستقبل عن طريق DTO + Repository
            var senderResult = await _userSender.Update(dto.SenderData);
            var reciverResult = await _userReceiver.Update(dto.ReciverData);

            // تحديث الشحنة
            await Update(dto);

            await _uow.CommitAsync();
            return true;
        }
        catch
        {
            await _uow.RollbackAsync();
            return false;
        }
    }



    public async Task EditFields(Guid id, Action<Shipment> updateAction)
    {
        await _repo.Update(id, updateAction);
    }
}

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

namespace BL.Services;

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
    public async Task<bool> Create(ShipmentDto dto)
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


            dto.CurrentState = (int)ShipmentstatuesEnum.Created;
            Guid gId = Guid.Empty;
            var resulet = await Add(dto);
            await _shipmentStatus.Add(gId, ShipmentstatuesEnum.Created);

            await _uow.CommitAsync();
            return true;
        }
        catch
        {
            await _uow.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> Edit(ShipmentDto dto)
    {
        try
        {

            //create tracking number
            await _uow.BeginTransactionAsync();

            dto.ShipingRate = _rateCalculator.CalculateRate(dto);

            // save sender
            dto.SenderData.Id = dto.SenderId;
            await _userSender.Update(dto.SenderData);

            dto.ReciverData.Id = dto.ReceiverId;

            await _userReceiver.Update(dto.ReciverData);
            // save shipment
            await Update(dto!);

            await _uow.CommitAsync();
            return true;

        }
        catch (Exception)
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

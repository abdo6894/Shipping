using BL.Services.Interfaces.IShipment.IManageStatue;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation.ShipmentService.ManageState
{
    public class ShipmentStateHandlerFactory : IShipmentStateHandlerFactory
    {
     private readonly  IServiceProvider _ServiceProvider;
        public ShipmentStateHandlerFactory(IServiceProvider ServiceProvider)
        {
            _ServiceProvider = ServiceProvider;
        }
        public IShipmentStateHandler GetHandler(ShipmentstatuesEnum status)
        {
            return status switch
            {
                ShipmentstatuesEnum.Approved => _ServiceProvider.GetRequiredService<ApproveShipment>(),
                ShipmentstatuesEnum.ReadyForShip => _ServiceProvider.GetRequiredService<ReadyShipment>(),
                ShipmentstatuesEnum.Shipped => _ServiceProvider.GetRequiredService<ShippedShipment>(),
                ShipmentstatuesEnum.Deliverd => _ServiceProvider.GetRequiredService<DeliverdShipment>(),
                ShipmentstatuesEnum.Returned => _ServiceProvider.GetRequiredService<ReturnedShipment>(),
                _ => throw new NotImplementedException($"No handler implemented for state: {status}")

            };

        }
    }
}

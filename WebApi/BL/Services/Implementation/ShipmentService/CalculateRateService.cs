using BL.Dtos;
using BL.Services.Interfaces.IShipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation.ShipmentService;


    public class CalculateRateService : ICalculateRateService
    {
        
        public decimal CalculateRate(ShipmentDto shipment)
        {
            return 3041;
        }

    }


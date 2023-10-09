using FleetManagement.Entities;
using Shared.DataServiceLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.DataServiceLayer.Contracts
{
    public interface ITripDSL : ICRUDOperationsDSL<TripDTO, TripSearchCriteriaDTO>
    {
    }
}

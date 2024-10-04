using AdventureWorks.Common.Domain.Production;
using AdventureWorks.Common.Domain.Products;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Service.Production
{
    public interface ILocationService
    {
        List<LocationDto> GetAllLocations();
        LocationDto GetLocationById(int id);
    }

    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public List<LocationDto> GetAllLocations()
        {
            var locationEntities = _locationRepository.GetAll().ToList();
            var locationDtos = MapLocations(locationEntities);
            
            return locationDtos;
        }

        public LocationDto GetLocationById(int id)
        {
            var shortId = (short)id;

            var locationEntity = _locationRepository.GetById(shortId);

            if (locationEntity == null)
                throw new NotFoundException($"Location with id '{id}' was not found.");

            var locationDto = MapLocation(locationEntity);

            return locationDto;
        }

        private List<LocationDto> MapLocations(List<Location> locationEntities)
        {
            var result = new List<LocationDto>();

            foreach (var locationEntity in locationEntities)
            {
                result.Add(MapLocation(locationEntity));
            }

            return result;
        }

        private LocationDto MapLocation(Location locationEntity)
        {
            var locationResult = new LocationDto
            {
                LocationId = locationEntity.LocationId,
                Availability = locationEntity.Availability,
                CostRate = locationEntity.CostRate,
                ModifiedDate = locationEntity.ModifiedDate,
                Name = locationEntity.Name
            };

            return locationResult;
        }        
    }
}

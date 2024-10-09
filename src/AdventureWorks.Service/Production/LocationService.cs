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
        LocationDto CreateLocation(LocationDto locationDto);
        Task UpdateLocation(int id, LocationDto locationDto);
        Task DeleteLocation(int id);
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

        public LocationDto CreateLocation(LocationDto locationDto)
        {
            var locationEntity = new Location
            {
                Availability = locationDto.Availability,
                CostRate = locationDto.CostRate,
                Name = locationDto.Name                
            };

            var createdLocation = _locationRepository.Insert(locationEntity);
            var result = MapLocation(createdLocation);
            return result;
        }

        public LocationDto GetLocationById(int id)
        {
            var locationEntity = FindLocation(id);
            var locationDto = MapLocation(locationEntity);

            return locationDto;
        }

        public async Task UpdateLocation(int id, LocationDto locationDto)
        {
            var locationEntityToUpdate = FindLocation(id);

            var changed = false;
            if(locationEntityToUpdate.Name != locationDto.Name)
            {
                locationEntityToUpdate.Name = locationDto.Name;
                changed = true;
            }

            if(locationEntityToUpdate.Availability != locationDto.Availability)
            {
                locationEntityToUpdate.Availability = locationDto.Availability;
                changed = true;
            }

            if(locationEntityToUpdate.CostRate != locationDto.CostRate)
            {
                locationEntityToUpdate.CostRate = locationDto.CostRate;
                changed = true;
            }

            if(changed)
            {
                locationEntityToUpdate.ModifiedDate = DateTime.Now;
                _locationRepository.Update(locationEntityToUpdate);
            }
        }

        public async Task DeleteLocation(int id)
        {
            var locationEntity = FindLocation(id);                     
            _locationRepository.Delete(locationEntity.LocationId);
        }

        private Location FindLocation(int id)
        {
            var shortId = (short)id;

            var locationEntity = _locationRepository.GetById(shortId);

            if (locationEntity == null)
                throw new NotFoundException($"Location with id '{id}' was not found.");

            return locationEntity;
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

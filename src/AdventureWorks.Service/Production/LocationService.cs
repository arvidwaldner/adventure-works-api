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
        Task<List<LocationDto>> GetAllLocations();
        Task<LocationDto> GetLocationById(int id);
        Task<LocationDto> CreateLocation(LocationDto locationDto);
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

        public async Task<List<LocationDto>> GetAllLocations()
        {
            var locationEntities = await _locationRepository.GetAllAsync();
            var locationDtos = MapLocations(locationEntities.ToList());
            
            return locationDtos;
        }

        public async Task<LocationDto> CreateLocation(LocationDto locationDto)
        {
            var locationEntity = new Location
            {
                Availability = locationDto.Availability,
                CostRate = locationDto.CostRate,
                Name = locationDto.Name                
            };

            var createdLocation = await _locationRepository.InsertAsync(locationEntity);
            var result = MapLocation(createdLocation);
            return result;
        }

        public async Task<LocationDto> GetLocationById(int id)
        {
            var locationEntity = await FindLocation(id);
            var locationDto = MapLocation(locationEntity);

            return locationDto;
        }

        public async Task UpdateLocation(int id, LocationDto locationDto)
        {
            var locationEntityToUpdate = await FindLocation(id);

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
                await _locationRepository.UpdateAsync(locationEntityToUpdate);
            }
        }

        public async Task DeleteLocation(int id)
        {
            var locationEntity = await FindLocation(id);                     
            await _locationRepository.DeleteAsync(locationEntity.LocationId);
        }

        private async Task<Location> FindLocation(int id)
        {
            var shortId = (short)id;

            var locationEntity = await _locationRepository.GetByIdAsync(shortId);

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

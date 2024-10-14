using AdventureWorks.Common.Domain.Sales;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Service.Sales
{
    public interface IStoreService
    {
        Task<StoreDto> GetStoreByBusinessEntityId(int businessEntityId);
        Task<List<StoreDto>> GetAllStores();
    }

    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository) 
        {
            _storeRepository = storeRepository;
        }

        public async Task<List<StoreDto>> GetAllStores()
        {
            var stores = _storeRepository.GetAll().ToList();
            var results = MapStores(stores);
            return results;
        }

        public async Task<StoreDto> GetStoreByBusinessEntityId(int businessEntityId)
        {
            var store = _storeRepository.GetById(businessEntityId);

            if (store == null)
                throw new NotFoundException($"A store with BusinessEntityId: '{businessEntityId}' was not found");

            var result = MapStore(store);
            return result;
        }

        private List<StoreDto> MapStores(List<Store> stores)
        {
            var results = new List<StoreDto>();

            foreach (var store in stores) 
            {
                results.Add(MapStore(store));
            }

            return results;
        }

        private StoreDto MapStore(Store store) 
        {
            var result = new StoreDto
            {
                BusinessEntityId = store.BusinessEntityId,
                Demographics = store.Demographics,
                Name = store.Name,
                ModifiedDate = store.ModifiedDate,
                RowGuid = store.Rowguid,
                SalesPersonId = store.SalesPersonId
            };

            return result;
        }
    }
}

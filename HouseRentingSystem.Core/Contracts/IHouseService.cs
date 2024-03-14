using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Enums;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();

        Task<bool> CategoryExistsAsync(int categoryId);

        Task<int> CreateAsync(HouseFormModel model, int agentId);

        Task<HouseQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);

        Task<IEnumerable<string>> AllCategoiesNamesAsync();

        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId);

        Task<IEnumerable<HouseServiceModel>> AllHousesByUserIdAsync(string userId);

        Task<bool> ExistsAsync(int id);

        Task<HouseDetailsServiceModel> HouseDatailsByIdAsync(int id);

        Task EditAsync(int houseId, HouseFormModel model);

        Task<bool> HasAgentWithIdAsync(int houseId, string userId);

        Task<HouseFormModel?> GetHouseFormModelByIdAsync(int id);

        Task Delete(int houseId);

        Task<bool> IsRented(int id);

        Task<bool> IsRentedByUserWithId(int houseId, string userId);

        Task Rent(int houseId, string userId);

        Task Leave(int houseId);
    }
}

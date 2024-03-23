using HouseRentingSystem.Attributes;
using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Extensions;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class HouseController : BaseController
    {
        private readonly IHouseService _houseService;
        private readonly IAgentService _agentService;

        public HouseController(
            IHouseService houseService,
            IAgentService agentService)
        {
            _houseService = houseService;
            _agentService = agentService;

        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel model)
        {
            var houses = await _houseService.AllAsync(
                model.Category,
                model.SearchTerm,
                model.Sorting,
                model.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            model.TotalHousesCount = houses.TotalHousesCount;
            model.Houses = houses.Houses;

            model.Categories = await _houseService.AllCategoiesNamesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            string userId = User.Id();

            IEnumerable<HouseServiceModel> model;

            if (await _agentService.ExistsByIdAsync(userId))
            {
                int agentId = await _agentService.GetAgentIdAsync(userId) ?? 0;
                model = await _houseService.AllHousesByAgentIdAsync(agentId);
            }
            else
            {
                model = await _houseService.AllHousesByUserIdAsync(userId);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
            if (await _houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await _houseService.HouseDatailsByIdAsync(id);

            if (information != model.GetInformation())
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        [MustBeAgent]
        public async Task<IActionResult> Add()
        {
            var model = new HouseFormModel()
            {
                Categories = await _houseService.AllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [MustBeAgent]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (!await _houseService.CategoryExistsAsync(model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await _houseService.AllCategoriesAsync();

                return View(model);
            }

            int? agentId = await _agentService.GetAgentIdAsync(User.Id());

            int newHouseId = await _houseService.CreateAsync(model, agentId ?? 0);

            return RedirectToAction(nameof(Details), new { id = newHouseId, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await _houseService.ExistsAsync(id))
            {
                return BadRequest();
            }

            if (!await _houseService.HasAgentWithIdAsync(id, User.Id()))
            {
                return Unauthorized();
            }

            var model = await _houseService.GetHouseFormModelByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HouseFormModel model, int id)
        {
            if (!await _houseService.ExistsAsync(id))
            {
                return BadRequest();
            }

            if (!await _houseService.HasAgentWithIdAsync(id, User.Id()))
            {
                return Unauthorized();
            }

            if (!await _houseService.CategoryExistsAsync(model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await _houseService.AllCategoriesAsync();

                return View(model);
            }

            await _houseService.EditAsync(id, model);

            return RedirectToAction(nameof(Details), new { id, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _houseService.ExistsAsync(id))
            {
                return BadRequest();
            }

            if (!await _houseService.HasAgentWithIdAsync(id, User.Id()))
            {
                return Unauthorized();
            }

            var house = await _houseService.HouseDatailsByIdAsync(id);

            var model = new HouseDetailsViewModel()
            {
                Id = house.Id,
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            if (!await _houseService.ExistsAsync(model.Id))
            {
                return BadRequest();
            }

            if (!await _houseService.HasAgentWithIdAsync(model.Id, User.Id()))
            {
                return Unauthorized();
            }

            await _houseService.Delete(model.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (!await _houseService.ExistsAsync(id))
            {
                return BadRequest();
            }

            if (await _agentService.ExistsByIdAsync(User.Id()))
            {
                return Unauthorized();
            }

            if (await _houseService.IsRentedAync(id))
            {
                return BadRequest();
            }

            await _houseService.Rent(id, User.Id());

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (!await _houseService.ExistsAsync(id) || !await _houseService.IsRentedAync(id))
            {
                return BadRequest();
            }

            if (!await _houseService.IsRentedByUserWithId(id, User.Id()))
            {
                return Unauthorized();
            }

            await _houseService.Leave(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}

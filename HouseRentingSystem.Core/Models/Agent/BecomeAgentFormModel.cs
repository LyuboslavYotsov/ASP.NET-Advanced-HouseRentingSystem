using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Data.Constants.DataConstants;
using static HouseRentingSystem.Core.Constants.MessageConstants;

namespace HouseRentingSystem.Core.Models.Agent
{
    public class BecomeAgentFormModel
    {
        [Required(ErrorMessage = RequireMessage)]
        [StringLength(AgentPhoneMaxLength,
            MinimumLength = AgentPhoneMinLength,
            ErrorMessage = LengthMessage)]
        [Display(Name = "Phone number")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

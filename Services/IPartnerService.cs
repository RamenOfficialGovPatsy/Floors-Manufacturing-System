using Master_Floor_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master_Floor_Project.Services
{
    public interface IPartnerService
    {
        Task<IEnumerable<Partner>> GetPartnersAsync();
        Task AddPartnerAsync(Partner partner);
        Task UpdatePartnerAsync(Partner partner);
        Task DeletePartnerAsync(int partnerId);
    }
}
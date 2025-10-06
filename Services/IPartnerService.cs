using System.Collections.Generic;
using System.Threading.Tasks;
using Master_Floor_Project.Models;

namespace Master_Floor_Project.Services
{
    public interface IPartnerService
    {
        Task<List<Partner>> GetPartnersAsync();
    }
}
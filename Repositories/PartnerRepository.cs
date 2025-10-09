// Repositories/PartnerRepository.cs
using Master_Floor_Project.Data;
using Master_Floor_Project.Models;

namespace Master_Floor_Project.Repositories
{
    public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
    {
        public PartnerRepository(AppDbContext context) : base(context)
        {
        }

        // Здесь будет реализация специфичных методов,
        // объявленных в IPartnerRepository.
    }
}
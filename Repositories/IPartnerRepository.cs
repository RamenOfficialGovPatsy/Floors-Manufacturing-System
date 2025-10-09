// Repositories/IPartnerRepository.cs
using Master_Floor_Project.Models;

namespace Master_Floor_Project.Repositories
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        // В будущем здесь можно будет добавить методы,
        // специфичные только для партнеров.
        // Например, поиск по ИНН или по имени директора.
    }
}
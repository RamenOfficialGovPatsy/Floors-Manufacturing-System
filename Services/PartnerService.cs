using System.Collections.Generic;
using System.Threading.Tasks;
using Master_Floor_Project.Models;

namespace Master_Floor_Project.Services
{
    public class PartnerService : IPartnerService
    {
        public Task<List<Partner>> GetPartnersAsync()
        {
            var partners = new List<Partner>
            {
                new Partner { Name = "ООО \"Вектор\"", Inn = "1234567890", DirectorName = "Иванов И.И.", Phone = "+79991234567", Email = "vector@mail.com" },
                new Partner { Name = "ООО \"Стройка\"", Inn = "0987654321", DirectorName = "Петров П.П.", Phone = "+79997654321", Email = "stroika@mail.com" },
                new Partner { Name = "ИП Сидоров А.В.", Inn = "5554443331", DirectorName = "Сидоров А.В.", Phone = "+79995554433", Email = "sidorov@mail.com" }
            };

            return Task.FromResult(partners);
        }
    }
}
// Services/PartnerService.cs
using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Master_Floor_Project.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master_Floor_Project.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PartnerService()
        {
            // В будущем здесь будет внедрение зависимостей (DI).
            // Пока что создаем экземпляры напрямую.
            _unitOfWork = new UnitOfWork(new AppDbContext());
        }

        public async Task<IEnumerable<Partner>> GetPartnersAsync()
        {
            return await _unitOfWork.Partners.GetAllAsync();
        }

        public async Task AddPartnerAsync(Partner partner)
        {
            await _unitOfWork.Partners.AddAsync(partner);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdatePartnerAsync(Partner partner)
        {
            _unitOfWork.Partners.Update(partner);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePartnerAsync(int partnerId)
        {
            var partner = await _unitOfWork.Partners.GetByIdAsync(partnerId);
            if (partner != null)
            {
                _unitOfWork.Partners.Delete(partner);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
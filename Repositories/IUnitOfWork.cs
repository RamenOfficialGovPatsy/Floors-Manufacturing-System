// Repositories/IUnitOfWork.cs
using System;
using System.Threading.Tasks;

namespace Master_Floor_Project.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPartnerRepository Partners { get; }
        // Здесь будут другие репозитории, например, IProductRepository

        Task<int> CompleteAsync();
    }
}
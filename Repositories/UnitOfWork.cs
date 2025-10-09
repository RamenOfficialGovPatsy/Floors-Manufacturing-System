// Repositories/UnitOfWork.cs
using Master_Floor_Project.Data;
using System.Threading.Tasks;

namespace Master_Floor_Project.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IPartnerRepository Partners { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Partners = new PartnerRepository(_context);
            // Здесь будет инициализация других репозиториев
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
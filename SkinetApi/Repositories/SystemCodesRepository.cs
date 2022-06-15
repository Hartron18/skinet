using Microsoft.EntityFrameworkCore;
using SkinetApi.Entities;

namespace SkinetApi.Repositories
{
    public class SystemCodesRepository : RepositoryBase<SystemCodes>, ISystemCodesRepository
    {
        public SystemCodesRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SystemCodes>> GetAllSystemCodes(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<SystemCodes> GetSystemCodeById(int id, bool trackChanges) =>
            await FindByCondition(e => e.OID.Equals(id), trackChanges).FirstOrDefaultAsync();

        public void CreateSystemCode(SystemCodes systemCode) => Create(systemCode);
        public void UpdateSystemCode(SystemCodes systemCode) => Update(systemCode);
        public void DeleteSystemCode(SystemCodes systemCode) => Delete(systemCode);
    }
}

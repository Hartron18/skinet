using SkinetApi.Entities;

namespace SkinetApi.Repositories
{
    public interface ISystemCodesRepository
    {
        void CreateSystemCode(SystemCodes systemCode);
        void DeleteSystemCode(SystemCodes systemCode);
        Task<IEnumerable<SystemCodes>> GetAllSystemCodes(bool trackChanges);
        Task<SystemCodes> GetSystemCodeById(int id, bool trackChanges);
        void UpdateSystemCode(SystemCodes systemCode);
    }
}
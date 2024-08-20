using USFWebAPI.Entities;

namespace USFWebAPI.Services
{
    public interface IPersonRepository<TModel> : IRepository<TModel> where TModel : class
    {
        Task<TModel> GetByCiAsync(string ci);
        Task<IEnumerable<TModel>> GetListByNameAsync(string name);
    }
}

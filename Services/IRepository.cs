namespace USFWebAPI.Services
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<TModel> GetByIdAsync(int id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task CreateAsync(TModel model);
    }
}

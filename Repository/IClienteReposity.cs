using Model;

namespace Repository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteModel>> GetAll();
        Task<ClienteModel> Get(string id);
        Task Add(ClienteModel cliente);
        Task Update(ClienteModel cliente);
        Task Delete(string id);
    }
}

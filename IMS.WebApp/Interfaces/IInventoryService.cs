using IMS.Core.Entities;

namespace IMS.WebApp.Interfaces
{
    public interface IInventoryService
    {
        Task<List<Inventory>> ExecuteAsync(string name);
    }
}

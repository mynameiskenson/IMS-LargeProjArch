using IMS.Core.Entities;
using IMS.Core.Interfaces;
using IMS.WebApp.Interfaces;
using IMS.WebApp.Responses;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace IMS.WebApp.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly NavigationManager _navigationManager;

        public InventoryService(IHttpClientFactory clientFactory, NavigationManager navigationManager)
        {
            _clientFactory = clientFactory;
            _navigationManager = navigationManager;
        }

        public async Task<List<Inventory>> ExecuteAsync(string name)
        {
            var client = _clientFactory.CreateClient("LocalApi");
            var response = await client.GetFromJsonAsync<ApiResponse<List<Inventory>>>($"api/Inventory/GetInventoryList/Engine");
            return response.Data;
        }
    }
}

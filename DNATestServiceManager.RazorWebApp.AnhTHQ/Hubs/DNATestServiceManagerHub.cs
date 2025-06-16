using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Hubs
{
    public class DNATestServiceManagerHub : Hub
    {
        private readonly IServicesAnhTHQService _servicesAnhTHQService;

        // SignalR Hub để xử lý CRUD các service
        public DNATestServiceManagerHub(IServicesAnhTHQService servicesAnhTHQService)
        {
            _servicesAnhTHQService = servicesAnhTHQService;
        }

        public async Task HubDelete_ServicesAnhTHQ(int serviceID)
        {
            await _servicesAnhTHQService.DeleteAsync(serviceID);
            await Clients.All.SendAsync("Receiver_DeleteServicesAnhTHQ", serviceID);
        }

        public async Task HubCreate_ServicesAnhTHQ(string jsonData)
        {
            var item = JsonConvert.DeserializeObject<ServiceAnhThq>(jsonData);
            item.CreatedDate = DateTime.Now;

            // Save to DB via DI service
            var id = await _servicesAnhTHQService.CreateAsync(item);
            item.ServiceAnhThqid = id;

            var itemJson = JsonConvert.SerializeObject(item);
            await Clients.All.SendAsync("Receiver_CreateServicesAnhTHQ", itemJson);
        }


        public async Task HubUpdate_ServicesAnhTHQ(string JsonServicesString)
        {
            var item = JsonConvert.DeserializeObject<ServiceAnhThq>(JsonServicesString);

            // Cập nhật dữ liệu vào DB trước
            await _servicesAnhTHQService.UpdateAsync(item);

            // Sau đó gửi thông tin cập nhật cho các client
            await Clients.All.SendAsync("Receiver_UpdateServicesAnhTHQ", item);
        }
    }
}

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

            var id = await _servicesAnhTHQService.CreateAsync(item);
            item.ServiceAnhThqid = id;

            var itemJson = JsonConvert.SerializeObject(item);

            // Gửi tới tất cả clients (hiển thị item mới)
            await Clients.All.SendAsync("Receiver_CreateServicesAnhTHQ", itemJson);

            // Gửi riêng cho người tạo (hiển thị thông báo)
            await Clients.Caller.SendAsync("HubCreateSuccess", item.ServiceName);
        }

        public async Task HubUpdate_ServicesAnhTHQ(string JsonServicesString)
        {
            var item = JsonConvert.DeserializeObject<ServiceAnhThq>(JsonServicesString);

            // Cập nhật dữ liệu vào DB trước
            await _servicesAnhTHQService.UpdateAsync(item);
            Console.WriteLine("Hub Update ID: " + item.ServiceAnhThqid);

            // Sau đó gửi thông tin cập nhật cho các client
            await Clients.All.SendAsync("Receiver_UpdateServicesAnhTHQ", item);
        }
    }
}

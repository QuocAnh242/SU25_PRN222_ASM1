using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
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

            // Ensure server-side CreatedDate override (you can keep or remove this)
            item.CreatedDate = DateTime.Now;

            // Validate
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(item, null, null);
            bool isValid = Validator.TryValidateObject(item, context, validationResults, true);

            // Check for IValidatableObject custom logic (e.g., CreatedDate < ModifiedDate)
            if (item is IValidatableObject validatable)
            {
                foreach (var result in validatable.Validate(context))
                {
                    validationResults.Add(result);
                    isValid = false;
                }
            }

            // If not valid, send error back to caller
            if (!isValid)
            {
                await Clients.Caller.SendAsync("HubCreateFailed", validationResults);
                return;
            }

            // Save to DB
            var id = await _servicesAnhTHQService.CreateAsync(item);
            item.ServiceAnhThqid = id;

            // Notify all clients with the new item
            var itemJson = JsonConvert.SerializeObject(item);
            await Clients.All.SendAsync("Receiver_CreateServicesAnhTHQ", itemJson);

            // Notify creator with success
            await Clients.Caller.SendAsync("HubCreateSuccess", item.ServiceName);
        }

        public async Task HubUpdate_ServicesAnhTHQ(string jsonData)
        {
            var item = JsonConvert.DeserializeObject<ServiceAnhThq>(jsonData);

            // Validate the object
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(item, null, null);
            bool isValid = Validator.TryValidateObject(item, context, validationResults, true);

            // Support for IValidatableObject custom rules
            if (item is IValidatableObject validatable)
            {
                foreach (var result in validatable.Validate(context))
                {
                    validationResults.Add(result);
                    isValid = false;
                }
            }

            if (!isValid)
            {
                // Send validation errors to caller
                await Clients.Caller.SendAsync("HubUpdateFailed", validationResults);
                return;
            }

            // If valid, proceed to update
            await _servicesAnhTHQService.UpdateAsync(item);

            // Notify all clients (optional)
            await Clients.All.SendAsync("Receiver_UpdateServicesAnhTHQ", item);

            // Notify caller with success message
            await Clients.Caller.SendAsync("HubUpdateSuccess", item.ServiceName);
        }
    }
}

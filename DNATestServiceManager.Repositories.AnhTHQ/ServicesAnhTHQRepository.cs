using DNATestServiceManager.Repositories.AnhTHQ.Basic;
using DNATestServiceManager.Repositories.AnhTHQ.DBContext;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using Microsoft.EntityFrameworkCore;

public class ServicesAnhTHQRepository : GenericRepository<ServicesAnhTHQRepository>
{
    public ServicesAnhTHQRepository() { }

    public ServicesAnhTHQRepository(SU25_PRN222_SE1706_G6_DNATestServiceManagerContext context) => _context = context;

    public async Task<List<ServiceAnhThq>> GetAllAsync()
    {
        return await _context.ServicesAnhThqs
            .Include(s => s.ServicePriceListAnhThqs)
            .ToListAsync();

    }

    public async Task<ServiceAnhThq> GetByIdAsync(int id)
    {
        return await _context.ServicesAnhThqs
            .Include(s => s.ServicePriceListAnhThqs)
            .Include(s => s.BookingMinhNda) // Include bookings liên quan để delete
            .ThenInclude(b => b.AppointmentScheduleTuTtcs) // nếu cần xóa các bảng con khác
            .Include(s => s.BookingMinhNda)
            .ThenInclude(b => b.InvoiceMinhNda)
            .Include(s => s.BookingMinhNda)
            .ThenInclude(b => b.KitDeliveryNguyenTqs)
            .Include(s => s.BookingMinhNda)
            .ThenInclude(b => b.SampleNhanTtts)
            .Include(s => s.BookingMinhNda)
            .ThenInclude(b => b.TestResultThanhDcs)
            .FirstOrDefaultAsync(s => s.ServiceAnhThqid == id)
            ?? new ServiceAnhThq();
    }

    public async Task<List<ServiceAnhThq>> SearchAsync(string serviceName, string serviceType, string category)
    {
        return await _context.ServicesAnhThqs
            .Include(s => s.ServicePriceListAnhThqs)
            .Where(s =>
                (string.IsNullOrEmpty(serviceName) || s.ServiceName.Contains(serviceName)) &&
                (string.IsNullOrEmpty(serviceType) || s.ServiceType.Contains(serviceType)) &&
                (string.IsNullOrEmpty(category) || s.Category.Contains(category))
            )
            .ToListAsync();
    }

    public async Task<int> CreateAsync(ServiceAnhThq entity)
    {
        _context.ServicesAnhThqs.Add(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(ServiceAnhThq entity)
    {
        _context.ServicesAnhThqs.Update(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        var service = await _context.ServicesAnhThqs
            .Include(s => s.BookingMinhNda)
                .ThenInclude(b => b.AppointmentScheduleTuTtcs)
            .Include(s => s.BookingMinhNda)
                .ThenInclude(b => b.InvoiceMinhNda)
            .Include(s => s.BookingMinhNda)
                .ThenInclude(b => b.KitDeliveryNguyenTqs)
            .Include(s => s.BookingMinhNda)
                .ThenInclude(b => b.SampleNhanTtts)
            .Include(s => s.BookingMinhNda)
                .ThenInclude(b => b.TestResultThanhDcs)
            .FirstOrDefaultAsync(s => s.ServiceAnhThqid == id);

        if (service == null) return 0;

        // Xóa các bản ghi con của Booking trước
        foreach (var booking in service.BookingMinhNda)
        {
            _context.AppointmentScheduleTuTtcs.RemoveRange(booking.AppointmentScheduleTuTtcs);
            _context.InvoiceMinhNda.RemoveRange(booking.InvoiceMinhNda);
            _context.KitDeliveryNguyenTqs.RemoveRange(booking.KitDeliveryNguyenTqs);
            _context.SampleNhanTtts.RemoveRange(booking.SampleNhanTtts);
            _context.TestResultThanhDcs.RemoveRange(booking.TestResultThanhDcs);
        }

        // Xóa các Booking liên quan
        _context.BookingMinhNda.RemoveRange(service.BookingMinhNda);

        // Xóa Service
        _context.ServicesAnhThqs.Remove(service);

        return await _context.SaveChangesAsync();
    }
}

using Core.DTOs.CourierDto;
using Core.Filters;
using Core.Responses;

namespace Infrastructure.Interfaces.Services;

public interface ICourierService
{
    Task<Response<List<GetCourierDto>>> GetCouriers(CourierFilter filter);
    Task<Response<GetCourierDto>> GetCourierById(int id);
    Task<Response<GetCourierDto>> CreateCourier(CreateCourierDto dto);
    Task<Response<GetCourierDto>> UpdateCourier(int id, UpdateCourierDto dto);
    Task<Response<string>> DeleteCourier(int id);
}
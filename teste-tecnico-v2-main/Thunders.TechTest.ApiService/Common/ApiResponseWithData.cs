namespace Thunders.TechTest.ApiService.Common;

public record ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }
}

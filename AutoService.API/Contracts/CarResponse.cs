namespace AutoService.API.Contracts
{
    public record CarResponse(Guid Id,
        string CarName,
        string Price,
        bool isAvailable);
}

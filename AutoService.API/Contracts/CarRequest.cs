namespace AutoService.API.Contracts
{
    public record CarRequest(
        string CarName,
        string Price,
        bool IsAvailable);
}

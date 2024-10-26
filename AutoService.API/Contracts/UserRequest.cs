namespace AutoService.API.Contracts
{
    public record UserRequest(
                              string Username,
                              string Password,
                              string Email);
}

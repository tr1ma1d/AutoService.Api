﻿namespace AutoService.API.Contracts
{
    public record UsersResponse(Guid Id,
                                string Username,
                                string Password,
                                string Email);
}

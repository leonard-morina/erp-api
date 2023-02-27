using Erp.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Erp.Api.Extensions;

public static class UserManagerExtensions
{
    public static async Task<bool> ValidateIfUsernameOrEmailExistsAsync<T>(this UserManager<T> userManager, string email, string username)
        where T : class
    {
        var foundUserByUsername = await userManager.FindByNameAsync(username);
        if (foundUserByUsername != null)
        {
            throw new UsernameExistsException();
        }

        var foundUserByEmail = await userManager.FindByEmailAsync(email);
        if (foundUserByEmail != null)
        {
            throw new EmailExistsException();
        }

        return true;
    }
}
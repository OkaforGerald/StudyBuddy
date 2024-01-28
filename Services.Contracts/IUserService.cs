using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedAPI.Data;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<UserDetailsDto> GetUserDetails(string username, bool trackChanges);
    }
}

using Mobilize.Contract.Base;

namespace Mobilize.Contract.GetCurrentUserInfoService
{
    public class GetCurrentUserInfoResponse : ServiceResponse
    {
        public Mobilize.Contract.GetUsersService.User User { get; set; }
    }
    
}

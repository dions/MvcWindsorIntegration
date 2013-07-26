using System.Collections.Generic;

namespace LM.AM.Core.Infrastructure.Session
{
    public interface IUserSession
    {
        string SessionId { get; set; }
        int? UserId { get; set; }
        string UserName { get; set; }
    }
}

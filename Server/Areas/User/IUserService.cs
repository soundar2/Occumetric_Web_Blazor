using System.Linq;
using System;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.User
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}
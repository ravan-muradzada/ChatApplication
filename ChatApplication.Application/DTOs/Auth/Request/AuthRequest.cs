using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.DTOs.Auth.Request
{
    public sealed record AuthRequest(string Username, string Password);
}

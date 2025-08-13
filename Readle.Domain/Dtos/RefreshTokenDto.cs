using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readle.Domain.Dtos
{
   public class RefreshTokenDto
    {
        public Guid UserId { get; set; }
        public string? RefreshToken { get; set; }
    }
}

using Sabio.Models.Domain.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Interfaces
{
    public interface IPaymentService
    {
        Payment Get(int id);
    }
}

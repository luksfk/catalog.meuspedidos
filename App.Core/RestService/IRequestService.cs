using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.RestService
{
    public interface IRequestService<T>
    {
        Task<List<T>> GetAll(string url);
    }
}

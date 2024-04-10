using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
    public interface ISkateboardService
    {
        public bool CreateSkateboard(string name, int brandId, int categoryId, string wheelId, string deckId, string bearingId, string griptapeId, string truckId, int quantity);
        public bool UpdateSkateboard(string id, string name, int brandId, int categoryId, string wheelId, string deckId, string bearingId, string griptapeId, string truckId,  int quantity);
    }
}

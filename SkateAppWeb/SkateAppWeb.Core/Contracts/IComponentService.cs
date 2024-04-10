using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
    public interface IComponentService : ICreatableComponents, IUpdatableComponents
    {
        public List<Product> GetComponents();
    }
}

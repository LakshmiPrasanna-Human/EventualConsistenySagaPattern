using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hierarchy.Models;

namespace Hierarchy.Interfaces
{
    public interface IGrandParent
    {
        List<GrandParentModel> GetAllGrandParents();
    }
}

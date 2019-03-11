using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hierarchy.Interfaces;
using Hierarchy.Models;

namespace Hierarchy.Class
{
    public class GrandParent: IGrandParent
    {
        public List<GrandParentModel> GetAllGrandParents()
        {
            List<GrandParentModel> _lst = new List<GrandParentModel>();
            _lst.Add(new GrandParentModel
            {
                GrandParentID = "1",
                OpuID = "2",
                IsActive = true,
                GrandParentName = "DHL"
            });

            return _lst;

        }

    }
}

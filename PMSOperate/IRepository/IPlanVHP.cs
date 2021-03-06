﻿using PMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSIRepository
{
    public interface IPlanVHP
    {
        IList<PlanVHP> GetAll();
        IList<PlanVHP> GetPlansBySearch();
        int Add(PlanVHP plan);
        int Update(PlanVHP plan);
        int Disable(Guid id);
    }
}

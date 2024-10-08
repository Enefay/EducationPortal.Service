﻿using EducationPortal.DataAccessLayer.Abstract;
using EducationPortal.DataAccessLayer.Concrete;
using EducationPortal.DataAccessLayer.Repositories;
using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DataAccessLayer.EntityFramework
{
    public class EfContentDal : GenericRepository<Content>, IContentDal
    {
        public EfContentDal(EducationPortalContext context) : base(context)
        {
        }
    }
}

using EducationPortal.BusinessLayer.Abstract;
using EducationPortal.DataAccessLayer.Abstract;
using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BusinessLayer.Concrete
{
    public class ContentManager : IContentService
    {
        private readonly IContentDal _contentDal;

        public ContentManager(IContentDal contentDal)
        {
            _contentDal = contentDal;
        }

        public void TAdd(Content entity)
        {
            _contentDal.Add(entity);
        }

        public void TDelete(Content entity)
        {
            _contentDal.Delete(entity);
        }

        public Content TGetByID(int id)
        {
            return _contentDal.GetByID(id);
        }

        public List<Content> TGetListAll()
        {
            return _contentDal.GetListAll();
        }

        public void TUpdate(Content entity)
        {
            _contentDal.Update(entity);
        }
    }
}

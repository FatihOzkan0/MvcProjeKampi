﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class HeadingManager : IHeadingService
    {

        IHeadingDal _headingDal;

        public HeadingManager(IHeadingDal headingDal)
        {
            _headingDal = headingDal;
        }

        public Heading GetByID(int id)
        {
            return _headingDal.Get(x=>x.HeadingID== id);
        }

        public List<Heading> GetList()
        {
            return _headingDal.List();
        }

        public List<Heading> GetListByWriter()
        {
            return _headingDal.List(x=>x.WriterID== 1);
        }

        public void HeadingAdd(Heading heading)
        {
             _headingDal.Insert(heading);
        }

        public void HeadingDelete(Heading heading)      //Silme işlemi yapmak bize sorun yaratabileceği için onun yerine durumunu false'a çekiyoruz.
        {
            
            _headingDal.Update(heading);
        }

        public void HeadingUpdate(Heading heading)
        {
           _headingDal.Update(heading);
        }
    }
}

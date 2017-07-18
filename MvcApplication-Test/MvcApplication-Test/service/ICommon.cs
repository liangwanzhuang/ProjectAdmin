using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication_Test
{
    public interface ICommon<T> where T : class,IEntity  
    {
        T Add(T model);
        T Update(T model);
        void Delete(T model);
        //按主键删除,keyValues是主键值  
        void Delete(params object[] keyValues);
        //keyValues是主键值  
        T Find(params object[] keyValues);
        List<T> FindAll();  
    }
}
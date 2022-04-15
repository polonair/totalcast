using System.Collections.Generic;
using System.Linq;

namespace tc2
{
    class ServiceContainer
    {
        private List<object> InnerList = new List<object>();

        internal List<T> GetAll<T>() => InnerList.Where(o => o.Is<T>()).Select(o => (T)o).ToList();
        internal T GetOne<T>() => InnerList.Where(o => o.Is<T>()).Select(o => (T)o).FirstOrDefault();
        internal void Add(object o) { if (!InnerList.Contains(o)) InnerList.Add(o); }
    }
}

using Newtonsoft.Json;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMulti
{
    public class EqpStatDb<T> : DBComm<EqpStatBase>
    {
        public EqpStatDb() : base()
        {

        }

        public EqpStat<T> GetStat(string name)
        {
            return EqpStat<T>.CopyFrom(Single(d => d.Name == name));
        }

        public T GetData(string name)
        {
            var stat = Single(d => d.Name == name);
            if (stat == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(stat.Data);
        }

        public void SetObject(string name, T obj)
        {
            if (!IsInitialized) return;
            using (var db = dbFactory.Open())
            {
                var exist = db.Single<EqpStatBase>(d => d.Name == name); 
                if (exist == null)
                {
                    var stat = new EqpStatBase
                    {
                        Name = name,
                        Data = JsonConvert.SerializeObject(obj),
                    };
                    db.Insert<EqpStatBase>(stat);
                }
                else
                {
                    exist.Data = JsonConvert.SerializeObject(obj);
                    exist.IndexCount++;
                    db.Update<EqpStatBase>(exist);
                }
            }
        }
    }
}

using Newtonsoft.Json;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMulti
{
    [Alias("EQPStat")]
    public class EqpStatBase
    {
        #region DataBase Field

        [AutoIncrement]
        public int Key { get; set; }

        //[PrimaryKey]                                    // Specify field to use as Primary Key
        [Index(Unique = true)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 통신에서 명령 관련된 항목들을 관리한다.
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 단순 변수값들을 관리한다.
        /// </summary>
        public string Data { get; set; }

        public string Desc { get; set; }

        public long IndexCount { get; set; }
        #endregion DataBase Field

    }

    public class EqpStat<T> : EqpStatBase
    {
        [Ignore]
        public T DataObject
        {
            get 
            {
                if (Data == null)
                    return default(T);
                return JsonConvert.DeserializeObject<T>(Data);
            }
            set { Data = JsonConvert.SerializeObject(value); }
        }

        [Ignore]
        public T EventObject
        {
            get
            {
                if (Data == null)
                    return default(T);
                return JsonConvert.DeserializeObject<T>(Event);
            }
            set { Event = JsonConvert.SerializeObject(value); }
        }


        public static EqpStat<T> CopyFrom(object statBase)
        {
            if (statBase == null)
                return null;
            var serialized = JsonConvert.SerializeObject(statBase);
            return JsonConvert.DeserializeObject<EqpStat<T>>(serialized);
        }
    }
}

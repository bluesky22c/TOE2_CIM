using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMulti
{
    public class StatCommBase<T>
    {
        public bool UseDB { get; set; } = false;
        public bool UseUDP { get; set; } = true;
        protected EqpStatDb<T> db;
        EqpStat<T> _eqpStat = null;
        EqpStat<T> _oldEqpStat = null;

        object statLockObj = new object();
        public EqpStat<T> EqpStat
        {
            get
            {
                lock (statLockObj)
                {
                    if (_eqpStat == null)
                        _eqpStat = new EqpStat<T>();
                    return _eqpStat;
                }
            }
            set
            {
                lock (statLockObj)
                {
                    if (_oldEqpStat == null)
                    {
                        _eqpStat = value;
                        _oldEqpStat = value;
                    }
                    else
                    {
                        if (_oldEqpStat.IndexCount > _eqpStat.IndexCount)
                            return;
                        _oldEqpStat = _eqpStat;
                        _eqpStat = value;
                    }
                }

            }
        }
        public string Name
        {
            get => EqpStat.Name;
            set => EqpStat.Name = value;
        }

        public T SendObject
        {
            set
            {
                lock (statLockObj)
                    EqpStat.DataObject = value;
            }
            get
            {
                lock (statLockObj)
                    return EqpStat.DataObject;
            }
        }

    }
}

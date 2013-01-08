using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCADA.RTDB.SimulationDeviceBase
{
    public class DeviceBase
    {

        public string Name { get; set; }
        /// <summary>
        /// 周期时间，单位ms，默认值1000ms
        /// </summary>
        public int CycleTime { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// 当前值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 上次数据产生时间间隔
        /// </summary>
        public int TimeSpace { get; set; }

        /// <summary>
        /// 产生下一个数据
        /// </summary>
        /// <returns>返回生成的数据</returns>
        public virtual int CreatData()
        {
            return Value;
        }

        protected DeviceBase()
        {
            
        }
    }
}

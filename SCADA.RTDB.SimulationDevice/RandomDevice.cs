using System;
using SCADA.RTDB.SimulationDeviceBase;

namespace SCADA.RTDB.SimulationDevice
{
    public class RandomDevice : DeviceBase
    {
        private readonly Random _rnd = new Random();

        #region 属性
        

        #endregion

        #region 公共方法

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="minValue">随机最小值</param>
        /// <param name="maxValue">随机最大值</param>
        /// <param name="name">随机数名</param>
        /// <param name="cycleTime">周期时间</param>
        public RandomDevice(string name = "rnd",int cycleTime = 1000, int minValue = 0, int maxValue = 100)
        {
            if (cycleTime <= 0)
            {
                throw new ArgumentOutOfRangeException(Resource1.RandomDevice_RandomDevice_CycleTimeMustBeGreaterThanZero);
            }
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
            CycleTime = cycleTime;

        }

        /// <summary>
        /// 产生下一个数据
        /// </summary>
        /// <returns>返回生成的数据</returns>
        public override int CreatData()
        {
            if (TimeSpace >= CycleTime)
            {
                Value = _rnd.Next(MinValue, MaxValue);
                TimeSpace = 0;
            }
            return Value;
        }

        #endregion


    }
}

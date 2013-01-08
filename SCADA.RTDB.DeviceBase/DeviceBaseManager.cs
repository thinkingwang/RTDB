using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SCADA.RTDB.SimulationDeviceBase
{
    public static class DeviceBaseManager
    {
        private static readonly List<DeviceBase> DeviceBases = new List<DeviceBase>();
        private static Thread _thread;

        #region 属性

        /// <summary>
        /// 获取线程状态，true为运行状态，false为停止状态
        /// </summary>
        public static bool IsRunning { get; private set; }

        /// <summary>
        /// 数据产生最小周期
        /// </summary>
        public static int CycleTime { get; set; }

        /// <summary>
        /// 设备列表
        /// </summary>
        public static List<DeviceBase> Devices
        {
            get { return DeviceBases; }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="device">设备</param>
        public static void AddDevice(DeviceBase device)
        {
            DeviceBases.Add(device);
        }

        /// <summary>
        /// 移除设备
        /// </summary>
        /// <param name="device">设备</param>
        public static void RemoveDevice(DeviceBase device)
        {
            DeviceBase deviceBase = DeviceBases.First(m=>m.Name==device.Name);
            DeviceBases.Remove(device);
        }

        /// <summary>
        /// 移除所有设备
        /// </summary>
        public static void Clear()
        {
            DeviceBases.Clear();
        }

        /// <summary>
        /// 启动设备生成数据
        /// </summary>
        public static void Start()
        {
            if (_thread == null || !_thread.IsAlive)
            {
                _thread = new Thread(CreateData);
                IsRunning = true;
                _thread.Start();
            }

        }

        /// <summary>
        /// 停止设备生成数据
        /// </summary>
        public static void Stop()
        {
            IsRunning = false;
            if (_thread == null)
            {
                return;
            }
            //等待线程结束
            while (_thread.IsAlive)
            {
                Thread.Sleep(10);
            }
        }

        #endregion

        static DeviceBaseManager()
        {
            CycleTime = 10;
        }

        /// <summary>
        /// 线程方法
        /// </summary>
        private static void CreateData()
        {
            while (IsRunning)
            {
                Thread.Sleep(CycleTime);
                foreach (var deviceBase in DeviceBases)
                {
                    deviceBase.TimeSpace += CycleTime;
                    deviceBase.CreatData();
                }
                
            }
        }
    }
}

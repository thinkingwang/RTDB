using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SCADA.RTDB.SimulationDeviceBase;
using SCADA.RTDB.VariableModel;
using System.Threading;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SCADA.RTDB.Adaptation
{
    public static class DeviceAdapter
    {
        //private static readonly DeviceMapContext DeviceMapContext = new DeviceMapContext("data source=VariableDB1.sdf;Password=666666");
        private static readonly Dictionary<VariableBase, DeviceBase> DeviceDictionary = new Dictionary<VariableBase, DeviceBase>();
        private static Thread _thread;
        //private static DeviceAdapter _deviceAdapter;

        /// <summary>
        /// 获取线程状态，true为运行状态，false为停止状态
        /// </summary>
        public static bool IsRunning { get; private set; }


        /// <summary>
        /// 周期时间，单位ms，默认值1000ms
        /// </summary>
        public static int CycleTime { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        static DeviceAdapter()
        {
            CycleTime = 1000;
        }
        
        /// <summary>
        /// 添加变量映射关系
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="deviceBase">映射方式</param>
        public static void AddMap(VariableBase variable, DeviceBase deviceBase)
        {
            if (variable == null)
            {
                return;
            }

            //如果变量为只写变量，不允许同步设备值
            if (variable.OperateProperty == VarOperateProperty.WriteOnly)
            {
                return;
            }

            if (deviceBase == null)
            {
                return;
            }
            var element = new DeviceMapMode()
                {
                    DeviceModeId = 0,
                    Name = deviceBase.Name,
                    CycleTime = deviceBase.CycleTime,
                    MaxValue = deviceBase.MinValue,
                    MinValue = deviceBase.MaxValue,
                    Variable = variable
                };
            
            //DeviceMapContext.DeviceMapModeSet.Add(element);
            if(DeviceDictionary.ContainsKey(variable)) return;
            DeviceDictionary.Add(variable,deviceBase);
            DeviceBaseManager.AddDevice(deviceBase);
        }

        /// <summary>
        /// 移除变量映射关系
        /// </summary>
        /// <param name="variable">变量</param>
        public static void RemoveMap(VariableBase variable)
        {
            //DeviceMapMode device = DeviceMapContext.DeviceMapModeSet.First(m => m.Variable.Name == variable.Name);
            //DeviceBaseManager.RemoveDevice(device);
            //DeviceMapContext.DeviceMapModeSet.Remove(device);
        }

        /// <summary>
        /// 启动同步数据
        /// </summary>
        public static void Start()
        {
            DeviceBaseManager.Start();
            if (_thread == null || !_thread.IsAlive)
            {
                _thread = new Thread(Synchrodata);
                IsRunning = true;
                _thread.Start();
            }
        }
        
        /// <summary>
        /// 停止同步数据
        /// </summary>
        public static void Stop()
        {
            IsRunning = false;
            DeviceBaseManager.Stop();
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

        /// <summary>
        /// 关闭所有后台线程，程序退出时调用
        /// </summary>
        public static void Close()
        {
            Stop();
            DeviceBaseManager.Clear();
        }
        
        /// <summary>
        /// 数据同步线程函数
        /// </summary>
        private static void Synchrodata()
        {
            while (IsRunning)
            {
                try
                {
                    foreach (var deviceBase in DeviceDictionary)
                    {
                        //如果变量为只写变量，不允许同步设备值，跳过
                        if (deviceBase.Key.OperateProperty == VarOperateProperty.WriteOnly)
                        {
                            continue;
                        }
                        if (deviceBase.Key is DigitalVariable)
                        {
                            ((DigitalVariable)deviceBase.Key).Value = deviceBase.Value.Value % 2 > 0;
                        }
                        else if (deviceBase.Key is AnalogVariable)
                        {
                            ((AnalogVariable)deviceBase.Key).Value = deviceBase.Value.Value;
                        }
                    }
                }
                catch 
                {
                    
                }

                Thread.Sleep(CycleTime);
            }

        }
    }
}

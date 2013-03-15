using System;
using System.Collections.Generic;
using System.Linq;
using SCADA.RTDB.Common.Base;
using SCADA.RTDB.Common.Design;
using SCADA.RTDB.Core.Alarm;
using SCADA.RTDB.EntityFramework.DbConfig;
using SCADA.RTDB.EntityFramework.ExtendMethod;
using SCADA.RTDB.EntityFramework.Repository.Base;

namespace SCADA.RTDB.EntityFramework.Repository.Design
{
    /// <summary>
    ///     报警设计仓储类
    /// </summary>
    public class AlarmDesignRepository : IAlarmDesignRepository
    {
        /// <summary>
        /// 变量仓库引用
        /// </summary>
        private readonly IAlarmRepository _iAlarmRepository;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="config">仓库配置信息</param>
        public AlarmDesignRepository(RepositoryConfig config)
        {
            _iAlarmRepository = new AlarmRepository(config);

            //注册报警变量或者报警组修改时触发的事件
            AlarmBase.VerifyTheUniqueness+=AlarmBase_VerifyTheUniqueness;
            AlarmGroup.VerifyTheUniqueness+=AlarmGroup_VerifyTheUniqueness;
        }

        #region 修改报警或者报警组名称之前的事件处理函数

        /// <summary>
        /// 修改报警变量名称触发的事件
        /// </summary>
        /// <param name="name">报警名称</param>
        /// <returns>数据库中是否存在该名称的列</returns>
        private bool AlarmBase_VerifyTheUniqueness(string name)
        {
            return RealTimeRepositoryBase.RtDbContext.AlarmSet == null ||
                   RealTimeRepositoryBase.RtDbContext.AlarmSet.Local.FirstOrDefault(m => m.Name == name) == null;
        }

        /// <summary>
        /// 修改报警组名称触发的事件
        /// </summary>
        /// <param name="name">报警组名称</param>
        /// <returns>数据库中是否存在该名称的列</returns>
        private bool AlarmGroup_VerifyTheUniqueness(string name)
        {
            return RealTimeRepositoryBase.RtDbContext.AlarmGroupSet == null ||
                   RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.Local.FirstOrDefault(m => m.Name == name) == null;
        }

        #endregion

        #region 变量报警

        /// <summary>
        ///     添加变量报警
        /// </summary>
        /// <param name="alarm"></param>
        public void AddAlarm(AlarmBase alarm)
        {
            if (alarm == null)
            {
                throw new ArgumentNullException(Resource1.AlarmRepository_AddAlarm_alarm);
            }
            CheckAlarmSetIsNull();

            //判断报警名称是否存在
            if (IsExistAlarmName(alarm.Name))
            {
                throw new Exception("变量报警名称已存在");
            }
            alarm.CreateTime = DateTime.Now;
            RealTimeRepositoryBase.RtDbContext.AlarmSet.Add(alarm);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        /// <summary>
        ///     报警组数据加载
        /// </summary>
        public void Load()
        {
            _iAlarmRepository.Load();
        }

        /// <summary>
        ///     根据报警名称，删除报警
        /// </summary>
        /// <param name="name"></param>
        public void RemoveAlarm(string name)
        {
            CheckAlarmSetIsNull();
            var item = RealTimeRepositoryBase.RtDbContext.AlarmSet.FirstOrDefault(a => a.Name == name);
            if (item == null)
            {
                return;
            }
            RealTimeRepositoryBase.RtDbContext.AlarmSet.Remove(item);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }
        
        /// <summary>
        ///     清除报警
        /// </summary>
        public void ClearAlarm()
        {
            CheckAlarmSetIsNull();
            for (int i = 0; i < RealTimeRepositoryBase.RtDbContext.AlarmSet.Count(); i++)
            {
                RealTimeRepositoryBase.RtDbContext.AlarmSet.Remove(
                    RealTimeRepositoryBase.RtDbContext.AlarmSet.ElementAt(i));
            }
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        /// <summary>
        ///     修改报警
        /// </summary>
        public void EditAlarm(string name, AlarmBase newAlarmBase)
        {
            CheckAlarmSetIsNull();
            if (!IsExistAlarmName(newAlarmBase.Name))
            {
                throw new Exception("报警名称已存在，不能修改");
            }
            AlarmBase alarm = FindAlarmByName(name);
            if (alarm == null)
            {
                throw new Exception("需要修改的报警不存在");
            }
            ObjectCopier.CopyProperties(alarm, newAlarmBase);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        /// <summary>
        ///     修改报警名称
        /// </summary>
        /// <param name="name">需要修改的报警名称</param>
        /// <param name="propertyName">需要修改的指定属性名称</param>
        /// <param name="value">属性修改后的值</param>
        public void EditAlarm(string name, string propertyName, object value)
        {
            CheckAlarmSetIsNull();
            AlarmBase alarm = FindAlarmByName(name);
            if (alarm == null)
            {
                throw new Exception("需要修改的报警不存在");
            }
            ObjectCopier.CopyProperty(alarm, propertyName, value);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }
        
        /// <summary>
        ///     根据变量绝对路径，查找指定变量的报警对象
        /// </summary>
        /// <param name="variablePath">变量全路径</param>
        /// <returns>找到的首个符合条件的报警，为找到返回null</returns>
        public AlarmBase FindAlarmByVariable(string variablePath)
        {
            return _iAlarmRepository.FindAlarmByVariable(variablePath);
        }

        /// <summary>
        ///     根据报警名查找抱紧对象
        /// </summary>
        /// <param name="name">报警名称</param>
        /// <returns>返回找到的报警，未找到则返回null</returns>
        public AlarmBase FindAlarmByName(string name)
        {
            return _iAlarmRepository.FindAlarmByName(name);
        }

        /// <summary>
        ///     查找所有报警对象
        /// </summary>
        /// <returns></returns>
        public List<AlarmBase> FindAlarms()
        {
            return _iAlarmRepository.FindAlarms();
        }
        
        /// <summary>
        ///     查找所有报警对象
        /// </summary>
        /// <returns></returns>
        public List<AlarmBase> FindAlarmByGroup(AlarmGroup group)
        {
            return _iAlarmRepository.FindAlarmByGroup(group);
        }
        
        /// <summary>
        /// 检查变量报警名称是否存在
        /// </summary>
        /// <param name="name">变量报警名称</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public bool IsExistAlarmName(string name)
        {
            CheckAlarmSetIsNull();
            var item = RealTimeRepositoryBase.RtDbContext.AlarmSet.FirstOrDefault(a => a.Name == name);
            return item != null;
        }
        
        private static void CheckAlarmSetIsNull()
        {
            if (RealTimeRepositoryBase.RtDbContext.AlarmSet == null)
            {
                throw new Exception("AlarmSet为null，数据库未初始化或初始化失败");
            }
        }

        #endregion

        #region 报警组

        /// <summary>
        /// 增加变量组
        /// </summary>
        /// <param name="alarmGroup">待增加的报警组</param>
        public void AddAlarmGroup(AlarmGroup alarmGroup)
        {
            if (alarmGroup == null)
            {
                throw new ArgumentNullException(Resource1.AlarmRepository_AddAlarm_alarm);
            }
            CheckAlarmSetIsNull();

            //判断报警名称是否存在
            if (IsExistAlarmGroupName(alarmGroup.Name))
            {
                throw new Exception("变量报警组名称已存在");
            }
            alarmGroup.CreateTime = DateTime.Now;
            RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.Add(alarmGroup);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        /// <summary>
        ///     删除指定报警组
        /// </summary>
        /// <param name="alarmGroupName">报警组名称</param>
        public void RemoveAlarmGroup(string alarmGroupName)
        {
            CheckAlarmGroupSetIsNull();
            AlarmGroup alarmGroup = FindAlarmGroupByName(alarmGroupName);
            RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.Remove(alarmGroup);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        /// <summary>
        ///     修改报警
        /// </summary>
        public void EditAlarmGroup(string name, AlarmGroup newAlarmGroup)
        {
            CheckAlarmGroupSetIsNull();
            if (!IsExistAlarmGroupName(newAlarmGroup.Name))
            {
                throw new Exception("报警组名称已存在，不能修改");
            }
            var alarmGroup = FindAlarmGroupByName(name);
            if (alarmGroup == null)
            {
                throw new Exception("需要修改的报警组不存在");
            }
            ObjectCopier.CopyProperties(alarmGroup, newAlarmGroup);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        /// <summary>
        ///     修改报警名称
        /// </summary>
        /// <param name="name">需要修改的报警名称</param>
        /// <param name="propertyName">需要修改的指定属性名称</param>
        /// <param name="value">属性修改后的值</param>
        public void EditAlarmGroup(string name, string propertyName, object value)
        {
            CheckAlarmGroupSetIsNull();
            var alarmGroup = FindAlarmGroupByName(name);
            if (alarmGroup == null)
            {
                throw new Exception("需要修改的报警不存在");
            }
            ObjectCopier.CopyProperty(alarmGroup, propertyName, value);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        /// <summary>
        ///     根据报警名查找抱紧对象
        /// </summary>
        /// <param name="name">报警名称</param>
        /// <returns>返回找到的报警，未找到则返回null</returns>
        public AlarmGroup FindAlarmGroupByName(string name)
        {
            return _iAlarmRepository.FindAlarmGroupByName(name);
        }

        /// <summary>
        /// 查找所有报警组对象
        /// </summary>
        /// <returns></returns>
        public List<AlarmGroup> FindAlarmGroups()
        {
            return _iAlarmRepository.FindAlarmGroups();
        }

        private static void CheckAlarmGroupSetIsNull()
        {
            if (RealTimeRepositoryBase.RtDbContext.AlarmGroupSet == null)
            {
                throw new Exception("AlarmGroupSet为null，数据库未初始化或初始化失败");
            }
        }

        /// <summary>
        /// 检查变量报警名称是否存在
        /// </summary>
        /// <param name="name">变量报警名称</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public bool IsExistAlarmGroupName(string name)
        {
            CheckAlarmSetIsNull();
            var item = RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.FirstOrDefault(a => a.Name == name);
            return item != null;
        }

        /// <summary>
        ///     清除报警组
        /// </summary>
        public void ClearAlarmGroup()
        {
            CheckAlarmGroupSetIsNull();
            for (int i = 0; i < RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.Count(); i++)
            {
                RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.Remove(
                    RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.ElementAt(i));
            }
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SCADA.RTDB.Common.Base;
using SCADA.RTDB.Core.Alarm;
using SCADA.RTDB.EntityFramework.DbConfig;

namespace SCADA.RTDB.EntityFramework.Repository.Base
{
    /// <summary>
    /// 变量报警仓储类，负责加载和查询变量
    /// </summary>
    public class AlarmRepository : IAlarmRepository
    {
        /// <summary>
        /// 变量仓库引用
        /// </summary>
        private readonly IVariableRepository _iVariableRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">变量仓储</param>
        public AlarmRepository(RepositoryConfig config)
        {
            _iVariableRepository = new VariableRepository(config);
        }

        /// <summary>
        ///     报警组数据加载
        /// </summary>
        public void Load()
        {
            RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.Load();
            RealTimeRepositoryBase.RtDbContext.AlarmSet.Load();
            foreach (AlarmBase alarm in RealTimeRepositoryBase.RtDbContext.AlarmSet.Local)
            {
                alarm.Variable = _iVariableRepository.FindVariableByPath(alarm.AbsolutePath);
            }
        }

        /// <summary>
        ///     根据变量绝对路径，查找指定变量的报警对象
        /// </summary>
        /// <param name="variablePath">变量全路径</param>
        /// <returns>找到的首个符合条件的报警，为找到返回null</returns>
        public AlarmBase FindAlarmByVariable(string variablePath)
        {
            return RealTimeRepositoryBase.RtDbContext.AlarmSet.FirstOrDefault(a => a.Variable.Name == variablePath);
        }

        /// <summary>
        ///     根据报警名查找抱紧对象
        /// </summary>
        /// <param name="name">报警名称</param>
        /// <returns>返回找到的报警，未找到则返回null</returns>
        public AlarmBase FindAlarmByName(string name)
        {
            return RealTimeRepositoryBase.RtDbContext.AlarmSet.FirstOrDefault(a => a.Name == name);
        }

        /// <summary>
        ///     查找所有报警对象
        /// </summary>
        /// <returns></returns>
        public List<AlarmBase> FindAlarms()
        {
            if (RealTimeRepositoryBase.RtDbContext.AlarmSet == null)
            {
                throw new Exception("RealTimeRepositoryBase.RtDbContext.AlarmSet is null");
            }
            if (RealTimeRepositoryBase.RtDbContext.AlarmSet.Local == null)
            {
                throw new Exception("RealTimeRepositoryBase.RtDbContext.AlarmSet.Local is null");
            }
            return RealTimeRepositoryBase.RtDbContext.AlarmSet.Local.ToList();
        }

        /// <summary>
        ///     查找所有报警对象
        /// </summary>
        /// <returns></returns>
        public List<AlarmBase> FindAlarmByGroup(AlarmGroup group)
        {
            if (RealTimeRepositoryBase.RtDbContext.AlarmSet == null)
            {
                throw new Exception("RealTimeRepositoryBase.RtDbContext.AlarmSet is null");
            }
            if (RealTimeRepositoryBase.RtDbContext.AlarmSet.Local == null)
            {
                throw new Exception("RealTimeRepositoryBase.RtDbContext.AlarmSet.Local is null");
            }
            return RealTimeRepositoryBase.RtDbContext.AlarmSet.Local.ToList().FindAll(m => m.Group == group);
        }
        
        /// <summary>
        ///     根据报警名查找抱紧对象
        /// </summary>
        /// <param name="name">报警名称</param>
        /// <returns>返回找到的报警，未找到则返回null</returns>
        public AlarmGroup FindAlarmGroupByName(string name)
        {
            return RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.FirstOrDefault(a => a.Name == name);
        }

        /// <summary>
        /// 查找所有报警组对象
        /// </summary>
        /// <returns></returns>
        public List<AlarmGroup> FindAlarmGroups()
        {
            if (RealTimeRepositoryBase.RtDbContext.AlarmGroupSet == null)
            {
                throw new Exception("RealTimeRepositoryBase.RtDbContext.AlarmSet is null");
            }
            if (RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.Local == null)
            {
                throw new Exception("RealTimeRepositoryBase.RtDbContext.AlarmSet.Local is null");
            }
            return RealTimeRepositoryBase.RtDbContext.AlarmGroupSet.Local.ToList();
        }
    }
}
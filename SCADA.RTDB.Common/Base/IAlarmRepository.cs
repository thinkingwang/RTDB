using System.Collections.Generic;
using SCADA.RTDB.Core.Alarm;

namespace SCADA.RTDB.Common.Base
{
    /// <summary>
    /// 变量报警仓储类接口，负责加载和查询变量
    /// </summary>
    public interface IAlarmRepository
    {
        /// <summary>
        ///     报警组数据加载
        /// </summary>
        void Load();

        /// <summary>
        ///     根据变量绝对路径，查找指定变量的报警对象
        /// </summary>
        /// <param name="variablePath">变量全路径</param>
        /// <returns>找到的首个符合条件的报警，为找到返回null</returns>
        AlarmBase FindAlarmByVariable(string variablePath);

        /// <summary>
        ///     根据报警名查找抱紧对象
        /// </summary>
        /// <param name="name">报警名称</param>
        /// <returns>返回找到的报警，未找到则返回null</returns>
        AlarmBase FindAlarmByName(string name);

        /// <summary>
        ///     查找所有报警对象
        /// </summary>
        /// <returns></returns>
        List<AlarmBase> FindAlarms();

        /// <summary>
        ///     查找所有报警对象
        /// </summary>
        /// <returns></returns>
        List<AlarmBase> FindAlarmByGroup(AlarmGroup group);

        /// <summary>
        ///     根据报警名查找抱紧对象
        /// </summary>
        /// <param name="name">报警名称</param>
        /// <returns>返回找到的报警，未找到则返回null</returns>
        AlarmGroup FindAlarmGroupByName(string name);

        /// <summary>
        /// 查找所有报警组对象
        /// </summary>
        /// <returns></returns>
        List<AlarmGroup> FindAlarmGroups();
    }
}
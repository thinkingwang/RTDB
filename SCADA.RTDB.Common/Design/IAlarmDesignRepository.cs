using System.Collections.Generic;
using SCADA.RTDB.Core.Alarm;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.Common.Design
{
    /// <summary>
    /// 报警设计仓储接口
    /// </summary>
    public interface IAlarmDesignRepository
    {
        /// <summary>
        ///     添加变量报警
        /// </summary>
        /// <param name="alarm"></param>
        void AddAlarm(AlarmBase alarm);

        /// <summary>
        ///     报警组数据加载
        /// </summary>
        void Load();

        /// <summary>
        ///     根据报警名称，删除报警
        /// </summary>
        /// <param name="name"></param>
        void RemoveAlarm(string name);

        /// <summary>
        ///     清除报警
        /// </summary>
        void ClearAlarm();

        /// <summary>
        ///     修改报警
        /// </summary>
        void EditAlarm(string name, AlarmBase newAlarmBase);

        /// <summary>
        ///     修改报警名称
        /// </summary>
        /// <param name="name">需要修改的报警名称</param>
        /// <param name="propertyName">需要修改的指定属性名称</param>
        /// <param name="value">属性修改后的值</param>
        void EditAlarm(string name, string propertyName, object value);

        /// <summary>
        ///     根据变量绝对路径，查找指定变量的报警对象
        /// </summary>
        /// <param name="variablePath"></param>
        /// <returns></returns>
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
        /// 检查变量报警名称是否存在
        /// </summary>
        /// <param name="name">变量报警名称</param>
        /// <returns>存在返回true，不存在返回false</returns>
        bool IsExistAlarmName(string name);

        /// <summary>
        /// 增加变量组
        /// </summary>
        /// <param name="alarmGroup">待增加的报警组</param>
        void AddAlarmGroup(AlarmGroup alarmGroup);

        /// <summary>
        ///     删除指定报警组
        /// </summary>
        /// <param name="alarmGroupName">报警组名称</param>
        void RemoveAlarmGroup(string alarmGroupName);

        /// <summary>
        ///     修改报警
        /// </summary>
        void EditAlarmGroup(string name, AlarmGroup newAlarmGroup);

        /// <summary>
        ///     修改报警名称
        /// </summary>
        /// <param name="name">需要修改的报警名称</param>
        /// <param name="propertyName">需要修改的指定属性名称</param>
        /// <param name="value">属性修改后的值</param>
        void EditAlarmGroup(string name, string propertyName, object value);

        /// <summary>
        /// 查找所有报警组对象
        /// </summary>
        /// <returns></returns>
        List<AlarmGroup> FindAlarmGroups();

        /// <summary>
        /// 检查变量报警名称是否存在
        /// </summary>
        /// <param name="name">变量报警名称</param>
        /// <returns>存在返回true，不存在返回false</returns>
        bool IsExistAlarmGroupName(string name);

        /// <summary>
        ///     根据报警名查找抱紧对象
        /// </summary>
        /// <param name="name">报警名称</param>
        /// <returns>返回找到的报警，未找到则返回null</returns>
        AlarmGroup FindAlarmGroupByName(string name);

        /// <summary>
        ///     清除报警组
        /// </summary>
        void ClearAlarmGroup();
    }
}
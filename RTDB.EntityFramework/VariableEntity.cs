using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RTDB.Common;
using RTDB.VariableModel;

namespace RTDB.EntityFramework
{
    public class VariableEntity : IVariableContext
    {

        #region 变量集合和组集合

        /// <summary>
        /// 模拟变量集合
        /// </summary>
        private static readonly Dictionary<string, AnalogVariable> AnalogVarSet =
            new Dictionary<string, AnalogVariable>();

        /// <summary>
        /// 数字变量集合
        /// </summary>
        private static readonly Dictionary<string, DigitalVariable> DigitalVarSet =
            new Dictionary<string, DigitalVariable>();

        /// <summary>
        /// 字符变量集合
        /// </summary>
        private static readonly Dictionary<string, StringVariable> StringVarSet =
            new Dictionary<string, StringVariable>();

        /// <summary>
        /// 变量组集合
        /// </summary>
        private static readonly List<VariableGroup> VarGroupColletion = new List<VariableGroup>();


        public Dictionary<string, AnalogVariable> AnalogSet
        {
            get { return AnalogVarSet; }
        }

        public Dictionary<string, DigitalVariable> DigitalSet
        {
            get { return DigitalVarSet; }
        }

        public Dictionary<string, StringVariable> StringSet
        {
            get { return StringVarSet; }
        }

        public List<VariableGroup> VariableGroupSet
        {
            get { return VarGroupColletion; }
        }

        #endregion

        public VariableEntity()
        {
            //将根组添加到集合
            VariableGroupSet.Add(VariableGroup.RootGroup);
        }



        public void VariableLoad()
        {
            
        }

        public void VariableSave()
        {
            
        }




        ///// <summary>
        ///// 初始化变量组
        ///// </summary>
        //private void InitVariableGroup()
        //{
        //    //VariableGroup.RootGroup = _variableContext.VariableGroupSet.Find(m => m.ParentGroupId == null);
        //    if (VariableGroup.RootGroup != null) AddGroupMethod(VariableGroup.RootGroup);
        //}

        ///// <summary>
        ///// 初始化添加组方法
        ///// </summary>
        ///// <param name="currentNode"></param>
        //private void AddGroupMethod(VariableGroup currentNode)
        //{
        //    if (currentNode == null)
        //    {
        //        Debug.Assert(Resource1.VariableUnitOfWork_AddGroupMethod_currentNodeIsNull != null, "Resource1.VariableUnitOfWork_AddGroupMethod_currentNodeIsNull != null");
        //        throw new ArgumentNullException(Resource1.VariableUnitOfWork_AddGroupMethod_currentNodeIsNull);
        //    }
        //    foreach (
        //        VariableGroup variableGroup in
        //            _variableContext.VariableGroupSet.FindAll(m => m.ParentGroupId == currentNode.VariableGroupId))
        //    {
        //        AddGroupMethod(variableGroup);
        //        currentNode.ChildGroups.Add(variableGroup);
        //        AddVariableMethod(variableGroup);
        //    }
        //}
        ///// <summary>
        ///// 初始化添加变量
        ///// </summary>
        ///// <param name="currentNode"></param>
        //private void AddVariableMethod(VariableGroup currentNode)
        //{
        //    if (currentNode == null)
        //    {
        //        Debug.Assert(Resource1.VariableUnitOfWork_AddGroupMethod_currentNodeIsNull != null, "Resource1.VariableUnitOfWork_AddGroupMethod_currentNodeIsNull != null");
        //        throw new ArgumentNullException(Resource1.VariableUnitOfWork_AddGroupMethod_currentNodeIsNull);
        //    }
        //    currentNode.ChildVariables.AddRange(from n in _variableContext.AnalogSet
        //                                        where n.Value.Group == currentNode
        //                                        select n.Value);
        //    currentNode.ChildVariables.AddRange(from n in _variableContext.DigitalSet
        //                                        where n.Value.Group == currentNode
        //                                        select n.Value);
        //    currentNode.ChildVariables.AddRange(from n in _variableContext.StringSet
        //                                        where n.Value.Group == currentNode
        //                                        select n.Value);
        //}

    }
}

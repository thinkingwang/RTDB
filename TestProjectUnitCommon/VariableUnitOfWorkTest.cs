using RTDB.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RTDB.EntityFramework;
using RTDB.VariableModel;

namespace TestProjectUnitCommon
{
    
    
    /// <summary>
    ///这是 VariableUnitOfWorkTest 的测试类，旨在
    ///包含所有 VariableUnitOfWorkTest 单元测试
    ///</summary>
    [TestClass()]
    public class VariableUnitOfWorkTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///VariableUnitOfWork 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void VariableUnitOfWorkConstructorTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext);
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///AddGroup 的测试
        ///</summary>
        [TestMethod()]
        public void AddGroupTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string groupName = string.Empty; // TODO: 初始化为适当的值
            string parentVariableGroupId = string.Empty; // TODO: 初始化为适当的值
            target.AddGroup(groupName, parentVariableGroupId);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddGroupMethod 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void AddGroupMethodTest()
        {
            PrivateObject param0 = new PrivateObject(new object()); // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            VariableGroup currentNode = null; // TODO: 初始化为适当的值
            target.AddGroupMethod(currentNode);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddVar 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void AddVarTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            target.AddVar(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddVariable 的测试
        ///</summary>
        [TestMethod()]
        public void AddVariableTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            target.AddVariable(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddVariableMethod 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void AddVariableMethodTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            VariableGroup currentNode = null; // TODO: 初始化为适当的值
            target.AddVariableMethod(currentNode);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///ClearVariable 的测试
        ///</summary>
        [TestMethod()]
        public void ClearVariableTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            VariableGroup currentVariableGroup = null; // TODO: 初始化为适当的值
            target.ClearVariable(currentVariableGroup);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///EditVar 的测试
        ///</summary>
        [TestMethod()]
        public void EditVarTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            target.EditVar(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///GetGroupById 的测试
        ///</summary>
        [TestMethod()]
        public void GetGroupByIdTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string groupId = string.Empty; // TODO: 初始化为适当的值
            VariableGroup expected = null; // TODO: 初始化为适当的值
            VariableGroup actual;
            actual = target.GetGroupById(groupId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GetInitVarName 的测试
        ///</summary>
        [TestMethod()]
        public void GetInitVarNameTest()
        {
            VariableGroup group = null; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = VariableUnitOfWork.GetInitVarName(group);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///InitVariableGroup 的测试
        ///</summary>
        [TestMethod()]
        public void InitVariableGroupTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            target.InitVariableGroup();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///IsExistName 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void IsExistNameTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            string name = string.Empty; // TODO: 初始化为适当的值
            VariableGroup currentVariableGroup = null; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.IsExistName(name, currentVariableGroup);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///RemoveGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveGroupTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string curVariableGroupId = string.Empty; // TODO: 初始化为适当的值
            target.RemoveGroup(curVariableGroupId);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RemoveVar 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void RemoveVarTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            target.RemoveVar(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RemoveVariable 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveVariableTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string variableName = string.Empty; // TODO: 初始化为适当的值
            string curVariableGroupId = string.Empty; // TODO: 初始化为适当的值
            target.RemoveVariable(variableName, curVariableGroupId);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RenameGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RenameGroupTest()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string groupName = string.Empty; // TODO: 初始化为适当的值
            string curVariableGroupId = string.Empty; // TODO: 初始化为适当的值
            target.RenameGroup(groupName, curVariableGroupId);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///VariableUnitOfWork 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void VariableUnitOfWorkConstructorTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext);
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///AddGroup 的测试
        ///</summary>
        [TestMethod()]
        public void AddGroupTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string groupName = string.Empty; // TODO: 初始化为适当的值
            string parentVariableGroupId = string.Empty; // TODO: 初始化为适当的值
            target.AddGroup(groupName, parentVariableGroupId);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddGroupMethod 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void AddGroupMethodTest1()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            VariableGroup currentNode = null; // TODO: 初始化为适当的值
            target.AddGroupMethod(currentNode);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddVar 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void AddVarTest1()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            target.AddVar(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddVariable 的测试
        ///</summary>
        [TestMethod()]
        public void AddVariableTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            target.AddVariable(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddVariableMethod 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void AddVariableMethodTest1()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            VariableGroup currentNode = null; // TODO: 初始化为适当的值
            target.AddVariableMethod(currentNode);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///ClearVariable 的测试
        ///</summary>
        [TestMethod()]
        public void ClearVariableTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            VariableGroup currentVariableGroup = null; // TODO: 初始化为适当的值
            target.ClearVariable(currentVariableGroup);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///EditVar 的测试
        ///</summary>
        [TestMethod()]
        public void EditVarTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            target.EditVar(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///GetGroupById 的测试
        ///</summary>
        [TestMethod()]
        public void GetGroupByIdTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string groupId = string.Empty; // TODO: 初始化为适当的值
            VariableGroup expected = null; // TODO: 初始化为适当的值
            VariableGroup actual;
            actual = target.GetGroupById(groupId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GetInitVarName 的测试
        ///</summary>
        [TestMethod()]
        public void GetInitVarNameTest1()
        {
            VariableGroup group = null; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = VariableUnitOfWork.GetInitVarName(group);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///InitVariableGroup 的测试
        ///</summary>
        [TestMethod()]
        public void InitVariableGroupTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            target.InitVariableGroup();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///IsExistName 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void IsExistNameTest1()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            string name = string.Empty; // TODO: 初始化为适当的值
            VariableGroup currentVariableGroup = null; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.IsExistName(name, currentVariableGroup);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///RemoveGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveGroupTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string curVariableGroupId = string.Empty; // TODO: 初始化为适当的值
            target.RemoveGroup(curVariableGroupId);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RemoveVar 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RTDB.Common.dll")]
        public void RemoveVarTest1()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            target.RemoveVar(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RemoveVariable 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveVariableTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string variableName = string.Empty; // TODO: 初始化为适当的值
            string curVariableGroupId = string.Empty; // TODO: 初始化为适当的值
            target.RemoveVariable(variableName, curVariableGroupId);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RenameGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RenameGroupTest1()
        {
            IVariableContext variableContext = null; // TODO: 初始化为适当的值
            VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
            string groupName = string.Empty; // TODO: 初始化为适当的值
            string curVariableGroupId = string.Empty; // TODO: 初始化为适当的值
            target.RenameGroup(groupName, curVariableGroupId);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

/// <summary>
///VariableUnitOfWork 构造函数 的测试
///</summary>
[TestMethod()]
public void VariableUnitOfWorkConstructorTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
    VariableUnitOfWork target = new VariableUnitOfWork(variableContext);
    Assert.Inconclusive("TODO: 实现用来验证目标的代码");
}

/// <summary>
///AddGroup 的测试
///</summary>
[TestMethod()]
public void AddGroupTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
string groupName = string.Empty; // TODO: 初始化为适当的值
string parentVariableGroupId = string.Empty; // TODO: 初始化为适当的值
    target.AddGroup(groupName, parentVariableGroupId);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///AddGroupMethod 的测试
///</summary>
[TestMethod()]
[DeploymentItem("RTDB.Common.dll")]
public void AddGroupMethodTest2()
{
PrivateObject param0 = null; // TODO: 初始化为适当的值
VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
VariableGroup currentNode = null; // TODO: 初始化为适当的值
    target.AddGroupMethod(currentNode);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///AddVar 的测试
///</summary>
[TestMethod()]
[DeploymentItem("RTDB.Common.dll")]
public void AddVarTest2()
{
PrivateObject param0 = null; // TODO: 初始化为适当的值
VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
VariableBase variable = null; // TODO: 初始化为适当的值
    target.AddVar(variable);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///AddVariable 的测试
///</summary>
[TestMethod()]
public void AddVariableTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
VariableBase variable = null; // TODO: 初始化为适当的值
    target.AddVariable(variable);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///AddVariableMethod 的测试
///</summary>
[TestMethod()]
[DeploymentItem("RTDB.Common.dll")]
public void AddVariableMethodTest2()
{
PrivateObject param0 = null; // TODO: 初始化为适当的值
VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
VariableGroup currentNode = null; // TODO: 初始化为适当的值
    target.AddVariableMethod(currentNode);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///ClearVariable 的测试
///</summary>
[TestMethod()]
public void ClearVariableTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
VariableGroup currentVariableGroup = null; // TODO: 初始化为适当的值
    target.ClearVariable(currentVariableGroup);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///EditVar 的测试
///</summary>
[TestMethod()]
public void EditVarTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
VariableBase variable = null; // TODO: 初始化为适当的值
    target.EditVar(variable);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///GetGroupById 的测试
///</summary>
[TestMethod()]
public void GetGroupByIdTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
string groupId = string.Empty; // TODO: 初始化为适当的值
VariableGroup expected = null; // TODO: 初始化为适当的值
    VariableGroup actual;
    actual = target.GetGroupById(groupId);
    Assert.AreEqual(expected, actual);
    Assert.Inconclusive("验证此测试方法的正确性。");
}

/// <summary>
///GetInitVarName 的测试
///</summary>
[TestMethod()]
public void GetInitVarNameTest2()
{
VariableGroup group = null; // TODO: 初始化为适当的值
string expected = string.Empty; // TODO: 初始化为适当的值
    string actual;
    actual = VariableUnitOfWork.GetInitVarName(group);
    Assert.AreEqual(expected, actual);
    Assert.Inconclusive("验证此测试方法的正确性。");
}

/// <summary>
///InitVariableGroup 的测试
///</summary>
[TestMethod()]
public void InitVariableGroupTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
    target.InitVariableGroup();
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///IsExistName 的测试
///</summary>
[TestMethod()]
[DeploymentItem("RTDB.Common.dll")]
public void IsExistNameTest2()
{
PrivateObject param0 = null; // TODO: 初始化为适当的值
VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
string name = string.Empty; // TODO: 初始化为适当的值
VariableGroup currentVariableGroup = null; // TODO: 初始化为适当的值
bool expected = false; // TODO: 初始化为适当的值
    bool actual;
    actual = target.IsExistName(name, currentVariableGroup);
    Assert.AreEqual(expected, actual);
    Assert.Inconclusive("验证此测试方法的正确性。");
}

/// <summary>
///RemoveGroup 的测试
///</summary>
[TestMethod()]
public void RemoveGroupTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
string curVariableGroupId = string.Empty; // TODO: 初始化为适当的值
    target.RemoveGroup(curVariableGroupId);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///RemoveVar 的测试
///</summary>
[TestMethod()]
[DeploymentItem("RTDB.Common.dll")]
public void RemoveVarTest2()
{
PrivateObject param0 = null; // TODO: 初始化为适当的值
VariableUnitOfWork_Accessor target = new VariableUnitOfWork_Accessor(param0); // TODO: 初始化为适当的值
VariableBase variable = null; // TODO: 初始化为适当的值
    target.RemoveVar(variable);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///RemoveVariable 的测试
///</summary>
[TestMethod()]
public void RemoveVariableTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
string variableName = string.Empty; // TODO: 初始化为适当的值
string curVariableGroupId = string.Empty; // TODO: 初始化为适当的值
    target.RemoveVariable(variableName, curVariableGroupId);
    Assert.Inconclusive("无法验证不返回值的方法。");
}

/// <summary>
///RenameGroup 的测试
///</summary>
[TestMethod()]
public void RenameGroupTest2()
{
IVariableContext variableContext = null; // TODO: 初始化为适当的值
VariableUnitOfWork target = new VariableUnitOfWork(variableContext); // TODO: 初始化为适当的值
string groupName = string.Empty; // TODO: 初始化为适当的值
string curVariableGroupId = string.Empty; // TODO: 初始化为适当的值
    target.RenameGroup(groupName, curVariableGroupId);
    Assert.Inconclusive("无法验证不返回值的方法。");
}
    }
}

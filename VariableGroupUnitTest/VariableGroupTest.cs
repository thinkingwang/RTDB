using Variable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace VariableGroupUnitTest
{
    
    
    /// <summary>
    ///这是 VariableGroupTest 的测试类，旨在
    ///包含所有 VariableGroupTest 单元测试
    ///</summary>
    [TestClass()]
    public class VariableGroupTest
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
        ///VariableGroup 构造函数 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("VariableObj.dll")]
        public void VariableGroupConstructorTest()
        {
            string groupName = string.Empty; // TODO: 初始化为适当的值
            VariableGroup_Accessor target = new VariableGroup_Accessor(groupName);
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///VariableGroup 构造函数 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("VariableObj.dll")]
        public void VariableGroupConstructorTest1()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///AddGroup 的测试
        ///</summary>
        [TestMethod()]
        public void AddGroupTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            string groupName = "yui"; // TODO: 初始化为适当的值
            target.AddGroup(groupName);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddVariable 的测试
        ///</summary>
        [TestMethod()]
        public void AddVariableTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            VariableBase variable = new VariableBase(); // TODO: 初始化为适当的值
            variable.GroupID = "123";
            target.AddVariable(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///ClearVariable 的测试
        ///</summary>
        [TestMethod()]
        public void ClearVariableTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            target.ClearVariable();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///GetFullPath 的测试
        ///</summary>
        [TestMethod()]
        public void GetFullPathTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            bool isHideRoot = false; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.GetFullPath(isHideRoot);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GetGroup 的测试
        ///</summary>
        [TestMethod()]
        public void GetGroupTest()
        {
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            bool isHideRoot = false; // TODO: 初始化为适当的值
            VariableGroup expected = new VariableGroup(); // TODO: 初始化为适当的值
            VariableGroup actual;
            actual = VariableGroup.GetGroup(fullPath, isHideRoot);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsContainVariable 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("VariableObj.dll")]
        public void IsContainVariableTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.IsContainVariable(variable);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsExistGroupName 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("VariableObj.dll")]
        public void IsExistGroupNameTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            string groupNameId = string.Empty; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.IsExistGroupName(groupNameId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///RemoveGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveGroupTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            target.RemoveGroup();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RemoveVariable 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveVariableTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            string variableName = string.Empty; // TODO: 初始化为适当的值
            target.RemoveVariable(variableName);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RenameGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RenameGroupTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            string groupName = string.Empty; // TODO: 初始化为适当的值
            target.RenameGroup(groupName);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///ChildGroups 的测试
        ///</summary>
        [TestMethod()]
        public void ChildGroupsTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            VariableGroup[] actual;
            actual = target.ChildGroups;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ChildVariables 的测试
        ///</summary>
        [TestMethod()]
        public void ChildVariablesTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            VariableBase[] actual;
            actual = target.ChildVariables;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GroupName 的测试
        ///</summary>
        [TestMethod()]
        public void GroupNameTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            target.GroupName = expected;
            actual = target.GroupName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GroupsCount 的测试
        ///</summary>
        [TestMethod()]
        public void GroupsCountTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            int actual;
            actual = target.GroupsCount;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///RootGroup 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("VariableObj.dll")]
        public void RootGroupTest()
        {
            VariableGroup expected = null; // TODO: 初始化为适当的值
            VariableGroup actual;
            VariableGroup_Accessor.RootGroup = expected;
            actual = VariableGroup_Accessor.RootGroup;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariablesCount 的测试
        ///</summary>
        [TestMethod()]
        public void VariablesCountTest()
        {
            VariableGroup_Accessor target = new VariableGroup_Accessor(); // TODO: 初始化为适当的值
            int actual;
            actual = target.VariablesCount;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}

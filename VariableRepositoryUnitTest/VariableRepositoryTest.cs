using SCADA.RTDB.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SCADA.RTDB.VariableModel;
using System.Collections.Generic;

namespace VariableRepositoryUnitTest
{
    
    
    /// <summary>
    ///这是 VariableRepositoryTest 的测试类，旨在
    ///包含所有 VariableRepositoryTest 单元测试
    ///</summary>
    [TestClass()]
    public class VariableRepositoryTest
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
        ///VariableRepository 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void VariableRepositoryConstructorTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString);
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///AddGroup 的测试
        ///</summary>
        [TestMethod()]
        public void AddGroupTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string name = string.Empty; // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            target.AddGroup(name, fullPath);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///AddVariable 的测试
        ///</summary>
        [TestMethod()]
        public void AddVariableTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            target.AddVariable(variable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///ClearVariable 的测试
        ///</summary>
        [TestMethod()]
        public void ClearVariableTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            target.ClearVariable(fullPath);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///CopyGroup 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SCADA.RTDB.Repository.dll")]
        public void CopyGroupTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableRepository_Accessor target = new VariableRepository_Accessor(param0); // TODO: 初始化为适当的值
            VariableGroup sourse = null; // TODO: 初始化为适当的值
            VariableGroup group = null; // TODO: 初始化为适当的值
            uint pasteMode = 0; // TODO: 初始化为适当的值
            target.CopyGroup(sourse, group, pasteMode);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///EditVariable 的测试
        ///</summary>
        [TestMethod()]
        public void EditVariableTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            VariableBase oldVariable = null; // TODO: 初始化为适当的值
            VariableBase newVariable = null; // TODO: 初始化为适当的值
            target.EditVariable(oldVariable, newVariable);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///FindGroupById 的测试
        ///</summary>
        [TestMethod()]
        public void FindGroupByIdTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            VariableGroup expected = null; // TODO: 初始化为适当的值
            VariableGroup actual;
            actual = target.FindGroupById(fullPath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///FindGroupByPath 的测试
        ///</summary>
        [TestMethod()]
        public void FindGroupByPathTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            VariableGroup expected = null; // TODO: 初始化为适当的值
            VariableGroup actual;
            actual = target.FindGroupByPath(fullPath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///FindGroups 的测试
        ///</summary>
        [TestMethod()]
        public void FindGroupsTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            IEnumerable<VariableGroup> expected = null; // TODO: 初始化为适当的值
            IEnumerable<VariableGroup> actual;
            actual = target.FindGroups(fullPath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///FindVariableById 的测试
        ///</summary>
        [TestMethod()]
        public void FindVariableByIdTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            VariableBase expected = null; // TODO: 初始化为适当的值
            VariableBase actual;
            actual = target.FindVariableById(fullPath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///FindVariableByPath 的测试
        ///</summary>
        [TestMethod()]
        public void FindVariableByPathTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            VariableBase expected = null; // TODO: 初始化为适当的值
            VariableBase actual;
            actual = target.FindVariableByPath(fullPath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///FindVariables 的测试
        ///</summary>
        [TestMethod()]
        public void FindVariablesTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            IEnumerable<VariableBase> expected = null; // TODO: 初始化为适当的值
            IEnumerable<VariableBase> actual;
            actual = target.FindVariables(fullPath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GetDefaultName 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SCADA.RTDB.Repository.dll")]
        public void GetDefaultNameTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableRepository_Accessor target = new VariableRepository_Accessor(param0); // TODO: 初始化为适当的值
            VariableGroup group = null; // TODO: 初始化为适当的值
            string defaultName = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.GetDefaultName(group, defaultName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsExistName 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SCADA.RTDB.Repository.dll")]
        public void IsExistNameTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableRepository_Accessor target = new VariableRepository_Accessor(param0); // TODO: 初始化为适当的值
            string name = string.Empty; // TODO: 初始化为适当的值
            VariableGroup group = null; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.IsExistName(name, group);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///PasteGroup 的测试
        ///</summary>
        [TestMethod()]
        public void PasteGroupTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            VariableGroup source = null; // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            bool isCopy = false; // TODO: 初始化为适当的值
            uint pasteMode = 0; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.PasteGroup(source, fullPath, isCopy, pasteMode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///PasteVariable 的测试
        ///</summary>
        [TestMethod()]
        public void PasteVariableTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            VariableBase source = null; // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            bool isCopy = false; // TODO: 初始化为适当的值
            uint pasteMode = 0; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.PasteVariable(source, fullPath, isCopy, pasteMode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///RemoveGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveGroupTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            target.RemoveGroup(fullPath);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RemoveVar 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SCADA.RTDB.Repository.dll")]
        public void RemoveVarTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableRepository_Accessor target = new VariableRepository_Accessor(param0); // TODO: 初始化为适当的值
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
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string name = string.Empty; // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            target.RemoveVariable(name, fullPath);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RenameGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RenameGroupTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            string name = string.Empty; // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            target.RenameGroup(name, fullPath);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///Save 的测试
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            string dbNameOrConnectingString = string.Empty; // TODO: 初始化为适当的值
            VariableRepository target = new VariableRepository(dbNameOrConnectingString); // TODO: 初始化为适当的值
            target.Save();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///findRecursion 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SCADA.RTDB.Repository.dll")]
        public void findRecursionTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableRepository_Accessor target = new VariableRepository_Accessor(param0); // TODO: 初始化为适当的值
            VariableGroup group = null; // TODO: 初始化为适当的值
            string fullPath = string.Empty; // TODO: 初始化为适当的值
            VariableGroup expected = null; // TODO: 初始化为适当的值
            VariableGroup actual;
            actual = target.findRecursion(group, fullPath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsChanged 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SCADA.RTDB.Repository.dll")]
        public void IsChangedTest()
        {
            PrivateObject param0 = null; // TODO: 初始化为适当的值
            VariableRepository_Accessor target = new VariableRepository_Accessor(param0); // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            target.IsChanged = expected;
            actual = target.IsChanged;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}

using SCADA.RTDB.EntityFramework.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SCADA.RTDB.EntityFramework.DbConfig;
using SCADA.RTDB.Core.Variable;
using System.Collections.Generic;
using SCADA.RTDB.StorageModel;

namespace UnitTest
{
    
    
    /// <summary>
    ///这是 EfVariableRepositoryTest 的测试类，旨在
    ///包含所有 EfVariableRepositoryTest 单元测试
    ///</summary>
    [TestClass()]
    public class EfVariableRepositoryTest
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
        ///EfVariableDesignRepository 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void EfVariableRepositoryConstructorTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig);
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///AddGroup 的测试
        ///</summary>
        [TestMethod()]
        public void AddGroupTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            string name = string.Empty; // TODO: 初始化为适当的值
            string absolutePath = string.Empty; // TODO: 初始化为适当的值
            VariableGroup expected = null; // TODO: 初始化为适当的值
            VariableGroup actual;
            actual = target.AddGroup(name, absolutePath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///AddVariable 的测试
        ///</summary>
        [TestMethod()]
        public void AddVariableTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            VariableBase expected = null; // TODO: 初始化为适当的值
            VariableBase actual;
            actual = target.AddVariable(variable);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///EditVariable 的测试
        ///</summary>
        [TestMethod()]
        public void EditVariableTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            List<string> variableStrings = null; // TODO: 初始化为适当的值
            VariableBase expected = null; // TODO: 初始化为适当的值
            VariableBase actual;
            actual = target.EditVariable(variable, variableStrings);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///EditVariable 的测试
        ///</summary>
        [TestMethod()]
        public void EditVariableTest1()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            VariableBase variable = null; // TODO: 初始化为适当的值
            VariableBase newVariable = null; // TODO: 初始化为适当的值
            VariableBase expected = null; // TODO: 初始化为适当的值
            VariableBase actual;
            actual = target.EditVariable(variable, newVariable);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ExitWithSaving 的测试
        ///</summary>
        [TestMethod()]
        public void ExitWithSavingTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            target.ExitWithSaving();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///Load 的测试
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            target.Load();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///LoadVariable 的测试
        ///</summary>
        //[TestMethod()]
        //[DeploymentItem("SCADA.RTDB.EntityFramework.dll")]
        //public void LoadVariableTest()
        //{
        //    PrivateObject param0 = null; // TODO: 初始化为适当的值
        //    EfVariableRepository_Accessor target = new EfVariableRepository_Accessor(param0); // TODO: 初始化为适当的值
        //    VariableGroup variablegroup = null; // TODO: 初始化为适当的值
        //    VariableGroupStorage parentGroupStorage = null; // TODO: 初始化为适当的值
        //    target.LoadVariable(variablegroup, parentGroupStorage);
        //    Assert.Inconclusive("无法验证不返回值的方法。");
        //}

        /// <summary>
        ///MoveGroup 的测试
        ///</summary>
        [TestMethod()]
        public void MoveGroupTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            string groupAbsolutePath = string.Empty; // TODO: 初始化为适当的值
            string desAbsolutePath = string.Empty; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.MoveGroup(groupAbsolutePath, desAbsolutePath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///RemoveGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveGroupTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            string absolutePath = string.Empty; // TODO: 初始化为适当的值
            target.RemoveGroup(absolutePath);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RemoveVariable 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveVariableTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            string name = string.Empty; // TODO: 初始化为适当的值
            string absolutePath = string.Empty; // TODO: 初始化为适当的值
            target.RemoveVariable(name, absolutePath);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///RenameGroup 的测试
        ///</summary>
        [TestMethod()]
        public void RenameGroupTest()
        {
            RepositoryConfig variableRepositoryConfig = null; // TODO: 初始化为适当的值
            EfVariableDesignRepository target = new EfVariableDesignRepository(variableRepositoryConfig); // TODO: 初始化为适当的值
            string name = string.Empty; // TODO: 初始化为适当的值
            string absolutePath = string.Empty; // TODO: 初始化为适当的值
            VariableGroup expected = null; // TODO: 初始化为适当的值
            VariableGroup actual;
            actual = target.RenameGroup(name, absolutePath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}

using RTDB.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Resources;
using System.Globalization;

namespace TestProjectUnitCommon
{
    
    
    /// <summary>
    ///这是 Resource1Test 的测试类，旨在
    ///包含所有 Resource1Test 单元测试
    ///</summary>
    [TestClass()]
    public class Resource1Test
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
        ///VariableGroup_IsExistGroupName_parentGroup_is_null 的测试
        ///</summary>
        [TestMethod()]
        public void VariableGroup_IsExistGroupName_parentGroup_is_nullTest()
        {
            string actual;
            actual = Resource1.VariableGroup_IsExistGroupName_parentGroup_is_null;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableGroup_AddVariable_variable_is_null 的测试
        ///</summary>
        [TestMethod()]
        public void VariableGroup_AddVariable_variable_is_nullTest()
        {
            string actual;
            actual = Resource1.VariableGroup_AddVariable_variable_is_null;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableBase_EditVar_varObj_is_null 的测试
        ///</summary>
        [TestMethod()]
        public void VariableBase_EditVar_varObj_is_nullTest()
        {
            string actual;
            actual = Resource1.VariableBase_EditVar_varObj_is_null;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableBase_AddVar_stringElement_is_null 的测试
        ///</summary>
        [TestMethod()]
        public void VariableBase_AddVar_stringElement_is_nullTest()
        {
            string actual;
            actual = Resource1.VariableBase_AddVar_stringElement_is_null;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableBase_AddVar_digitalElement_is_null 的测试
        ///</summary>
        [TestMethod()]
        public void VariableBase_AddVar_digitalElement_is_nullTest()
        {
            string actual;
            actual = Resource1.VariableBase_AddVar_digitalElement_is_null;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableBase_AddVar_analogElement_is_null 的测试
        ///</summary>
        [TestMethod()]
        public void VariableBase_AddVar_analogElement_is_nullTest()
        {
            string actual;
            actual = Resource1.VariableBase_AddVar_analogElement_is_null;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///UnitofWork_AddGroup_currentVariableGroup 的测试
        ///</summary>
        [TestMethod()]
        public void UnitofWork_AddGroup_currentVariableGroupTest()
        {
            string actual;
            actual = Resource1.UnitofWork_AddGroup_currentVariableGroup;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ResourceManager 的测试
        ///</summary>
        [TestMethod()]
        public void ResourceManagerTest()
        {
            ResourceManager actual;
            actual = Resource1.ResourceManager;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Culture 的测试
        ///</summary>
        [TestMethod()]
        public void CultureTest()
        {
            CultureInfo expected = null; // TODO: 初始化为适当的值
            CultureInfo actual;
            Resource1.Culture = expected;
            actual = Resource1.Culture;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///CVariableGroup_AddGroup_GroupeNameIsExist 的测试
        ///</summary>
        [TestMethod()]
        public void CVariableGroup_AddGroup_GroupeNameIsExistTest()
        {
            string actual;
            actual = Resource1.CVariableGroup_AddGroup_GroupeNameIsExist;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///CVariableGroup_AddGroup_GroupNameIsNull 的测试
        ///</summary>
        [TestMethod()]
        public void CVariableGroup_AddGroup_GroupNameIsNullTest()
        {
            string actual;
            actual = Resource1.CVariableGroup_AddGroup_GroupNameIsNull;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Resource1 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void Resource1ConstructorTest()
        {
            Resource1 target = new Resource1();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///VariableGroup_ReGroupName_groupName_Is_Null 的测试
        ///</summary>
        [TestMethod()]
        public void VariableGroup_ReGroupName_groupName_Is_NullTest()
        {
            string actual;
            actual = Resource1.VariableGroup_ReGroupName_groupName_Is_Null;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup 的测试
        ///</summary>
        [TestMethod()]
        public void VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroupTest()
        {
            string actual;
            actual = Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableGroup_ReplaceGroupName_editGroup_is_null 的测试
        ///</summary>
        [TestMethod()]
        public void VariableGroup_ReplaceGroupName_editGroup_is_nullTest()
        {
            string actual;
            actual = Resource1.VariableGroup_ReplaceGroupName_editGroup_is_null;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableGroup_addVariable_variableName_is_Exist 的测试
        ///</summary>
        [TestMethod()]
        public void VariableGroup_addVariable_variableName_is_ExistTest()
        {
            string actual;
            actual = Resource1.VariableGroup_addVariable_variableName_is_Exist;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableRepository_AddVar_VariableIsExist 的测试
        ///</summary>
        [TestMethod()]
        public void VariableRepository_AddVar_VariableIsExistTest()
        {
            string actual;
            actual = Resource1.VariableRepository_AddVar_VariableIsExist;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///VariableRepository_AddVar_VariableIsNull 的测试
        ///</summary>
        [TestMethod()]
        public void VariableRepository_AddVar_VariableIsNullTest()
        {
            string actual;
            actual = Resource1.VariableRepository_AddVar_VariableIsNull;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}

using RTDB.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RTDB.VariableModel;
using System.Collections.Generic;

namespace TestProjectUnitCommon
{
    
    
    /// <summary>
    ///这是 IVariableContextTest 的测试类，旨在
    ///包含所有 IVariableContextTest 单元测试
    ///</summary>
    [TestClass()]
    public class IVariableContextTest
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


        internal virtual IVariableContext CreateIVariableContext()
        {
            // TODO: 实例化相应的具体类。
            IVariableContext target = null;
            return target;
        }

        /// <summary>
        ///VariableGroupSet 的测试
        ///</summary>
        [TestMethod()]
        public void VariableGroupSetTest()
        {
            IVariableContext target = CreateIVariableContext(); // TODO: 初始化为适当的值
            List<VariableGroup> actual;
            actual = target.VariableGroupSet;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///StringSet 的测试
        ///</summary>
        [TestMethod()]
        public void StringSetTest()
        {
            IVariableContext target = CreateIVariableContext(); // TODO: 初始化为适当的值
            Dictionary<string, StringVariable> actual;
            actual = target.StringSet;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///DigitalSet 的测试
        ///</summary>
        [TestMethod()]
        public void DigitalSetTest()
        {
            IVariableContext target = CreateIVariableContext(); // TODO: 初始化为适当的值
            Dictionary<string, DigitalVariable> actual;
            actual = target.DigitalSet;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///AnalogSet 的测试
        ///</summary>
        [TestMethod()]
        public void AnalogSetTest()
        {
            IVariableContext target = CreateIVariableContext(); // TODO: 初始化为适当的值
            Dictionary<string, AnalogVariable> actual;
            actual = target.AnalogSet;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}

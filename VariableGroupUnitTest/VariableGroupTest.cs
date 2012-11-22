using VariableObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
        public void VariableGroupConstructorTest()
        {
            VariableGroup target = new VariableGroup();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///AddGroup 的测试
        ///</summary>
        [TestMethod()]
        public void AddGroupTest()
        {
            VariableGroup target = new VariableGroup(); // TODO: 初始化为适当的值
            string groupName = string.Empty; // TODO: 初始化为适当的值
            VariableGroup parentGroup = null; // TODO: 初始化为适当的值
            VariableGroup.AddGroup(groupName, parentGroup);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///DeleteGroup 的测试
        ///</summary>
        [TestMethod()]
        public void DeleteGroupTest()
        {
            VariableGroup target = new VariableGroup(); // TODO: 初始化为适当的值
            VariableGroup delelteGroupObj = null; // TODO: 初始化为适当的值
            VariableGroup.DeleteGroup(delelteGroupObj);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///EditGroupName 的测试
        ///</summary>
        [TestMethod()]
        public void EditGroupNameTest()
        {
            VariableGroup target = new VariableGroup(); // TODO: 初始化为适当的值
            VariableGroup editGroup = null; // TODO: 初始化为适当的值
            string groupName = string.Empty; // TODO: 初始化为适当的值
            VariableGroup.EditGroupName(editGroup, groupName);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///GetVars 的测试
        ///</summary>
        [TestMethod()]
        public void GetVarsTest()
        {
            VariableGroup target = new VariableGroup(); // TODO: 初始化为适当的值
            string id = string.Empty; // TODO: 初始化为适当的值
            List<VariableBaseObj> expected = null; // TODO: 初始化为适当的值
            List<VariableBaseObj> actual;
            actual = VariableGroup.GetVars(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }


        /// <summary>
        ///VariableGroup 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void VariableGroupConstructorTest1()
        {
            VariableGroup target = new VariableGroup();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///AddGroup 的测试
        ///</summary>
        [TestMethod()]
        public void AddGroupTest1()
        {
            string groupName = string.Empty; // TODO: 初始化为适当的值
            TreeNode parentGroup = null; // TODO: 初始化为适当的值
            TreeNode expected = null; // TODO: 初始化为适当的值
            TreeNode actual;
            actual = VariableGroup.AddGroup(groupName, parentGroup);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///DeleteGroup 的测试
        ///</summary>
        [TestMethod()]
        public void DeleteGroupTest1()
        {
            TreeNode delelteGroupObj = null; // TODO: 初始化为适当的值
            VariableGroup.DeleteGroup(delelteGroupObj);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///EditGroupName 的测试
        ///</summary>
        [TestMethod()]
        public void EditGroupNameTest1()
        {
            TreeNode editGroup = null; // TODO: 初始化为适当的值
            string groupName = string.Empty; // TODO: 初始化为适当的值
            VariableGroup.EditGroupName(editGroup, groupName);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///GetVars 的测试
        ///</summary>
        [TestMethod()]
        public void GetVarsTest1()
        {
            string id = string.Empty; // TODO: 初始化为适当的值
            List<VariableBaseObj> expected = null; // TODO: 初始化为适当的值
            List<VariableBaseObj> actual;
            actual = VariableGroup.GetVars(id);
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
            string groupNameId = string.Empty; // TODO: 初始化为适当的值
            TreeNode parentGroup = null; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = VariableGroup_Accessor.IsExistGroupName(groupNameId, parentGroup);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ReplaceGroupName 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("VariableObj.dll")]
        public void ReplaceGroupNameTest()
        {
            TreeNode editGroup = null; // TODO: 初始化为适当的值
            string oldGroupName = string.Empty; // TODO: 初始化为适当的值
            string newGroupName = string.Empty; // TODO: 初始化为适当的值
            VariableGroup_Accessor.ReplaceGroupName(editGroup, oldGroupName, newGroupName);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///GroupId 的测试
        ///</summary>
        [TestMethod()]
        public void GroupIdTest()
        {
            VariableGroup target = new VariableGroup(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            target.GroupId = expected;
            actual = target.GroupId;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///RootGroupName 的测试
        ///</summary>
        [TestMethod()]
        public void RootGroupNameTest()
        {
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            VariableGroup.RootGroupName = expected;
            actual = VariableGroup.RootGroupName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}

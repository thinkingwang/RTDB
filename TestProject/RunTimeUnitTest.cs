using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SCADA.RTDB.Common.Design;

namespace TestProject
{
    [TestClass]
    public class RunTimeUnitTest
    {
        private Mock<IVariableDesignRepository> _iVariableDesignRepository;

        [TestMethod]
        public void TestMethod1()
        {
            _iVariableDesignRepository = new Mock<IVariableDesignRepository>();
            var result =
                _iVariableDesignRepository.Setup(m => m.PasteGroup(null, null, true, 0))
                                          .Returns("This Method has been mocked");
            MessageBox.Show(_iVariableDesignRepository.Object.PasteGroup(null,null,true,0));
        }
    }
}

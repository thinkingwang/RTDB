using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Variable;

namespace VariableObjTestSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private RichTextBox rtb;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                rtb = richTextBox1;
                VariableGroup.RootGroup.GroupName = "变量字典测试";
                VariableGroup.RootGroup.AddVariable(new AnalogVar("")); //添加变量

                VariableGroup.RootGroup.AddGroup("varGroup1");
                VariableGroup.RootGroup.AddGroup("varGroup2");
                VariableGroup.RootGroup.AddGroup("varGroup3");
                VariableGroup.RootGroup.AddGroup("varGroup4");
                VariableGroup varGroup = VariableGroup.GetGroup("varGroup1");

                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new StringVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new DigitalVar(varGroup.GetFullPath())); //添加变量

                varGroup.AddGroup("varGroup1_sub1");
                varGroup.AddGroup("varGroup1_sub2");
                varGroup.AddGroup("varGroup1_sub3");
                varGroup.AddGroup("varGroup1_sub4");

                varGroup = VariableGroup.GetGroup("varGroup2");

                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new StringVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new DigitalVar(varGroup.GetFullPath())); //添加变量


                varGroup.AddGroup("varGroup2_sub1");
                varGroup.AddGroup("varGroup2_sub2");
                varGroup.AddGroup("varGroup2_sub3");
                varGroup.AddGroup("varGroup2_sub4");

                varGroup = VariableGroup.GetGroup("varGroup3");

                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new StringVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new DigitalVar(varGroup.GetFullPath())); //添加变量


                varGroup.AddGroup("varGroup3_sub1");
                varGroup.AddGroup("varGroup3_sub2");
                varGroup.AddGroup("varGroup3_sub3");
                varGroup.AddGroup("varGroup3_sub4");

                varGroup = VariableGroup.GetGroup("varGroup4");

                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new StringVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new DigitalVar(varGroup.GetFullPath())); //添加变量


                varGroup.AddGroup("varGroup4_sub1");
                varGroup.AddGroup("varGroup4_sub2");
                varGroup.AddGroup("varGroup4_sub3");
                varGroup.AddGroup("varGroup4_sub4");

                varGroup = VariableGroup.GetGroup("varGroup2.varGroup2_sub4");

                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new StringVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new DigitalVar(varGroup.GetFullPath())); //添加变量


                varGroup.AddGroup("varGroup2_sub1");
                varGroup.AddGroup("varGroup2_sub2");
                varGroup.AddGroup("varGroup2_sub3");
                varGroup.AddGroup("varGroup2_sub4");

                varGroup = VariableGroup.GetGroup("varGroup2.varGroup2_sub4.varGroup2_sub3");

                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new StringVar(varGroup.GetFullPath())); //添加变量
                varGroup.AddVariable(new DigitalVar(varGroup.GetFullPath())); //添加变量


                varGroup.AddGroup("varGroup2_sub1");
                varGroup.AddGroup("varGroup2_sub2");
                varGroup.AddGroup("varGroup2_sub3");
                varGroup.AddGroup("varGroup2_sub4");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rtb = richTextBox1;
            rtb.Text = "";
            ErgodicTree(VariableGroup.RootGroup, 0);
        }

        private void ErgodicTree(VariableGroup element,int deep)
        {
            deep ++;
            string headStr = "--";
            for (int i = 0; i < deep; i++)
            {
                headStr += "--";
            }
            rtb.Text += headStr + element.GroupName + "\r\n";
            for (int i =0;i<  element.ChildGroups.Count();i++)
            {
                if (element.ChildGroups[i].ChildGroups != null)
                {
                    ErgodicTree(element.ChildGroups[i], deep);
                }
                if (i + 1 == element.ChildGroups.Count())
                {
                    headStr += "--";
                    foreach (var child in element.ChildVariables)
                    {
                        rtb.Text += headStr + child.GroupID + "." + child.VarName + "\r\n";
                    }
                }
            } 
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rtb = richTextBox2;
            rtb.Text = "";
            VariableGroup varGroup = VariableGroup.GetGroup("varGroup2.varGroup2_sub4");
            if (varGroup != null)
            {
                varGroup.RemoveGroup();
            }
            ErgodicTree(VariableGroup.RootGroup, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rtb = richTextBox2;
            rtb.Text = "";
            VariableGroup varGroup = VariableGroup.GetGroup("varGroup2.varGroup2_sub3");
            if (varGroup != null)
            {
                varGroup.RenameGroup("思考哥");
            }
            ErgodicTree(VariableGroup.RootGroup, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            rtb = richTextBox2;
            rtb.Text = "";
            VariableGroup varGroup = VariableGroup.GetGroup("varGroup2.varGroup2_sub4.varGroup2_sub3");
            if (varGroup != null)
            {
                varGroup.RemoveVariable("Variable1");
                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath()));
                varGroup.AddVariable(new AnalogVar(varGroup.GetFullPath()));
                //varGroup.ClearVariable();
            }
            ErgodicTree(VariableGroup.RootGroup, 0);
        }
      

    }
}

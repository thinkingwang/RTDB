using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VariableObj;

namespace VariableObjTestSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes[0].Nodes[0].Nodes.Add(VariableGroup.RootGroup);
            VariableGroup.RootGroup.ContextMenuStrip = contextMenuStrip1;
            VariableGroup.RootGroup.Text = "变量字典";
            treeView1.ExpandAll();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = VariableGroup.AddGroup(textBox1.Text, treeView1.SelectedNode);
                node.ContextMenuStrip = contextMenuStrip1;
                treeView1.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                VariableGroup.DeleteGroup(treeView1.SelectedNode);
                treeView1.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                VariableGroup.EditGroupName(treeView1.SelectedNode, textBox1.Text);
                treeView1.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

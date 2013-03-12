using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SCADA.RTDB.Common.Design;
using SCADA.RTDB.Core.Alarm;

namespace SCADA.RTDB.Repository.Test
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AlarmGroupList : Form
    {
        private readonly IAlarmDesignRepository _iAlarmDesignRepository;
        private readonly string _alarmName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iAlarmDesignRepository"></param>
        /// <param name="alarmName"></param>
        public AlarmGroupList(IAlarmDesignRepository iAlarmDesignRepository, string alarmName)
        {
            InitializeComponent();
            _iAlarmDesignRepository = iAlarmDesignRepository;
            _alarmName = alarmName;
        }

        private void btn_AlarmGroupListOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择报警组");
                return;
            }
            try
            {
                var alarmGroup =
                    _iAlarmDesignRepository.FindAlarmGroupByName(
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                _iAlarmDesignRepository.EditAlarm(_alarmName, "Group", alarmGroup);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void btn_AlarmGroupListCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AlarmGroupList_Load(object sender, EventArgs e)
        {
            var alarmGroups = _iAlarmDesignRepository.FindAllAlarmGroup();
            dataGridView1.Rows.Clear();
            foreach (var alarmGroup in alarmGroups)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = alarmGroup.Name;
                row.Cells[1].Value = alarmGroup.Description;
                dataGridView1.Rows.Add(row);
            }
        }

        private void btn_AlarmGroupListClear_Click(object sender, EventArgs e)
        {
            _iAlarmDesignRepository.EditAlarm(_alarmName, "Group", null);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

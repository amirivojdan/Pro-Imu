using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows.Forms;
using Probotix.Imu;
using Probotix.Mpu.UI;
using Probotix.Utils;


namespace Probotix.Mpu
{
    public partial class Main : Form
    {
        private readonly List<int> _basicSearch = new List<int> { 1, 34 };
        private readonly List<int> _advanceSearch = new List<int> { 1, 3, 4, 7, 9, 16, 34, 103, 207 };
        private readonly List<int> _fullSearch = new List<int>();


        public Main()
        {

            InitializeComponent();

            EnumerateComPorts();
            comboBox_search_options.SelectedIndex = 0;
            for (int i = 0; i < 254; i++)
            {
                _fullSearch.Add(i);
            }
            panel_device_data.Visible = panel_device_data.Enabled = false;
            panel_search.Enabled = panel_search.Visible = false;
        }

        public enum SearchMode
        {
            Basic,
            Advance,
            Custom,
            Full,
            Fast
        }

        public void SelectSearchMode(SearchMode mode)
        {
            switch (mode)
            {
                case SearchMode.Basic:
                    FillBaudRates(_basicSearch, true);
                    break;

                case SearchMode.Advance:
                    FillBaudRates(_advanceSearch, true);
                    break;

                case SearchMode.Custom:
                    FillBaudRates(_fullSearch, false);
                    break;

                case SearchMode.Full:
                    FillBaudRates(_fullSearch, true);
                    break;
            }
        }

        public void FillBaudRates(List<int> baudNumList, bool check)
        {
            dataGridView_baudRates.Rows.Clear();
            var index = 0;
            foreach (var baudNumValue in baudNumList)
            {
                dataGridView_baudRates.Rows.Add(new DataGridViewRow());
                dataGridView_baudRates.Rows[index].Cells["baudnum"].Value = baudNumValue;
                dataGridView_baudRates.Rows[index].Cells["baudrate"].Value = 2000000 / (baudNumValue + 1);
                dataGridView_baudRates.Rows[index++].Cells["SearchChecked"].Value = check;
            }
        }

        public List<int> GetBaudSearchList()
        {
            var baudlist = new List<int>();
            for (int i = 0; i < dataGridView_baudRates.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridView_baudRates.Rows[i].Cells["SearchChecked"].Value))
                {
                    baudlist.Add(Convert.ToInt32(dataGridView_baudRates.Rows[i].Cells["baudnum"].Value));
                }
            }
            return baudlist;
        }

        private SearchDynamixels _searchDynamixels;
        private void button_start_search_Click(object sender, EventArgs e)
        {
            treeView_devices.Nodes.Clear();
            _searchDynamixels = new SearchDynamixels(_mpu6050, GetBaudSearchList(), treeView_devices, toolStripProgressBar_progress, toolStrip_TopToolStrip, toolStripStatusLabel_progress_percent, toolStripStatusLabel_state);
            _searchDynamixels.Start();
        }

        private void button_stop_search_Click(object sender, EventArgs e)
        {
            _searchDynamixels.Stop();
        }

        private void comboBox_search_options_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox_search_options.SelectedItem.ToString() == "Basic Search")
            {
                SelectSearchMode(SearchMode.Basic);
            }
            else if (comboBox_search_options.SelectedItem.ToString() == "Advance Search")
            {
                SelectSearchMode(SearchMode.Advance);
            }
            else if (comboBox_search_options.SelectedItem.ToString() == "Custom Search")
            {
                SelectSearchMode(SearchMode.Custom);
            }
            else if (comboBox_search_options.SelectedItem.ToString() == "Full Search")
            {
                SelectSearchMode(SearchMode.Full);
            }
        }

        private void EnumerateComPorts()
        {
            toolStripComboBox_com_port.Items.Clear();
            toolStripComboBox_com_port.Text = string.Empty;
            toolStripComboBox_com_port.Items.AddRange(SerialPort.GetPortNames());
            if (toolStripComboBox_com_port.Items.Count > 0)
            {
                toolStripComboBox_com_port.SelectedIndex = 0;
            }
        }

        private void toolStripComboBox_com_port_Click(object sender, EventArgs e)
        {
            EnumerateComPorts();
        }

        private void toolStripButton_refreshports_Click(object sender, EventArgs e)
        {
            EnumerateComPorts();
        }

        private Mpu6050 _mpu6050;
        private void toolStripButton_connect_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox_com_port.SelectedItem == null )
            {
                return;
            }
            _mpu6050 = new Mpu6050(toolStripComboBox_com_port.SelectedItem.ToString(), 1);

            if (_mpu6050.Open())
            {
                toolStripButton_connect.Enabled = false;
                toolStripButton_disconnect.Enabled = true;
                panel_search.Enabled = panel_search.Visible = true;
                panel_device_data.Visible = panel_device_data.Enabled = false;
                Console.WriteLine(String.Format("Connected !"));
                return;
            }
            Console.WriteLine(String.Format("Connection Failed !"));
        }

        private void toolStripButton_disconnect_Click(object sender, EventArgs e)
        {
            if (_updater != null) _updater.Stop();
            if (_searchDynamixels != null) _searchDynamixels.Stop();
            if (_mpu6050.Close())
            {
                toolStripButton_connect.Enabled = true;
                toolStripButton_disconnect.Enabled = false;

                panel_search.Enabled = panel_search.Visible = false;
                panel_device_data.Visible = panel_device_data.Enabled = false;

                treeView_devices.Nodes.Clear();
                Console.WriteLine(String.Format("Disconnected !"));
                return;
            }
            Console.WriteLine(String.Format("Disconnection Failed !"));
        }

        private DeviceDataUpdater _updater;
        private int _deviceId;
        private void treeView_devices_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                panel_device_data.Enabled = panel_device_data.Visible = true;
                //panel_search.Enabled = panel_search.Visible = false;
                _deviceId = Convert.ToInt32(e.Node.Name);
                var selectedDeviceBaudrate = Convert.ToInt32(e.Node.Parent.Name);
                _mpu6050.BaudRate = selectedDeviceBaudrate;
                if (_updater != null)
                {
                    _updater.Stop();
                }
                _updater = new DeviceDataUpdater(_mpu6050, dataGridView_device_data, _deviceId);
                _updater.Start();
                RefreshRegisters(_deviceId);
            }
        }

        private Simulator _simulator;
        private BackgroundWorker _backgroundWorker;
        private void toolStripButton_showVisualizer_Click(object sender, EventArgs e)
        {
            _simulator = new Simulator();
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += (delegate { _simulator.ShowDialog(); });
            _backgroundWorker.RunWorkerAsync();
        }


        //Quaternion orientation of earth frame relative to auxiliary frame.
        double AEq_1 = 1;
        double AEq_2 = 0;
        double AEq_3 = 0;
        double AEq_4 = 0;

        //Estimated orientation quaternion elements with initial conditions.
        double SEq_1 = 1;
        double SEq_2 = 0;
        double SEq_3 = 0;
        double SEq_4 = 0;


        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((_simulator != null) && (_mpu6050 != null))
            {
                _simulator.Pitch = (int)_mpu6050.FilteredAnglePitch;
                _simulator.Roll = (int)_mpu6050.FilteredAngleRoll;
                _simulator.Yaw = (int)_mpu6050.FilteredAngleYaw;

            }
        }



        private void dataGridView_device_data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var register = Mpu6050.GetRegisterFromDescription(
                dataGridView_device_data.Rows[e.RowIndex].Cells["description"].Value.ToString());

            var data = (float)Convert.ToDouble(dataGridView_device_data.Rows[e.RowIndex].Cells["value"].Value);

            if (Mpu6050.RegisterLength(register) == 4)
            {
                var floatValue = new ByteArrayFloat();
                floatValue.FloatNumber = data;
                _mpu6050.WriteData(_deviceId, (int)register, new List<Byte> { floatValue.Byte0, floatValue.Byte1, floatValue.Byte2, floatValue.Byte3 }, true);
            }
            else if (Mpu6050.RegisterLength(register) == 2)
            {
                _mpu6050.WriteWord(_deviceId, (int)register, (int)data, true);
            }
            else
            {
                _mpu6050.WriteByte(_deviceId, (int)register, (int)data, true);
            }


            RefreshRegisters(_deviceId);
        }

        private void RefreshRegisters(int id)
        {
            var data = _mpu6050.ReadData(id, Mpu6050.Register.ModelNumber, Mpu6050.Register.Beta);
            SetRowValue(0, "value", (data[1] << 8) + data[0]); // ModelNumber
            SetRowValue(1, "value", data[2]); // Firmware Verison
            SetRowValue(2, "value", data[3]); // Id
            SetRowValue(3, "value", data[4]); // Baud Num
            SetRowValue(4, "value", data[5]); // AutoSend

            SetRowValue(5, "value", data[6]); // Gyro X Offset
            SetRowValue(6, "value", data[7]); // Gyro Y Offset
            SetRowValue(7, "value", data[8]); // Gyro Z Offset



        }

        private void SetRowValue(int rowindex, string cellname, double value)
        {
            dataGridView_device_data.Rows[rowindex].Cells[cellname].Value = value;
        }

    }
}

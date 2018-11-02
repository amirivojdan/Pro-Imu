using System;
using System.Windows.Forms;
using Probotix.Imu;
using Probotix.Utils;
 

namespace Probotix.Mpu.UI
{
    public class DeviceDataUpdater
    {
        private readonly DataGridView _dataGridView;
        private readonly Mpu6050 _mpu6050;
        private readonly Thread _thread;
        private readonly int _id;

        public DeviceDataUpdater(Mpu6050 networkBus, DataGridView dgv, int id)
        {
            _dataGridView = dgv;
            _mpu6050 = networkBus;

            _thread = new Thread(UpdateData) { Interval = 0 };
            _thread.Starting += InitializeRows;
            _thread.Starting += InitializeStaticData;
            _id = id;
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Stop()
        {
            _thread.Stop();
        }

        private void UpdateData()
        {
            try
            {
                _mpu6050.UpdateAll(_id);



                SetRowValue(8, "value", _mpu6050.GetBeta(_id));

                SetRowValue(9, "value", _mpu6050.RawAccelX);
                SetRowValue(10, "value", _mpu6050.RawAccelY);
                SetRowValue(11, "value", _mpu6050.RawAccelZ);
                 

                SetRowValue(13, "value", _mpu6050.RawGyroX);
                SetRowValue(14, "value", _mpu6050.RawGyroY);
                SetRowValue(15, "value", _mpu6050.RawGyroZ);
 


                SetRowValue(16, "value", _mpu6050.Q0);
                SetRowValue(17, "value", _mpu6050.Q1);
                SetRowValue(18, "value", _mpu6050.Q2);
                SetRowValue(19, "value", _mpu6050.Q3);

                SetRowValue(20, "value", _mpu6050.FilteredAngleRoll  );
                SetRowValue(21, "value", _mpu6050.FilteredAnglePitch  );
                SetRowValue(22, "value", _mpu6050.FilteredAngleYaw );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void SetRowValue(int rowindex, string cellname, double value)
        {
            if (!_dataGridView.InvokeRequired)
            {
                _dataGridView.Rows[rowindex].Cells[cellname].Value = value;
            }
            else
            {
                _dataGridView.Invoke(new MethodInvoker(() => SetRowValue(rowindex, cellname, value)));
            }
        }

        private void InitializeRows()
        {
            if (!_dataGridView.InvokeRequired)
            {

                var regs = (Mpu6050.Register[])Enum.GetValues(typeof(Mpu6050.Register));
                int index = 0;
                _dataGridView.Rows.Clear();
                foreach (var register in regs)
                {
                    _dataGridView.Rows.Add(new DataGridViewRow());
                    _dataGridView.Rows[index].Cells["addr"].Value = (int)register;
                    _dataGridView.Rows[index].Cells["description"].Value = register.GetDescription();
                    index++;
                }
            }

            else
            {
                _dataGridView.Invoke(new MethodInvoker(InitializeRows));
            }
        }

        private void InitializeStaticData()
        {
          
            var data = _mpu6050.ReadData(_id, Mpu6050.Register.ModelNumber, Mpu6050.Register.AutoSend);
            SetRowValue(0, "value", (data[1] << 8) + data[0]);
            SetRowValue(1, "value", data[2]);
            SetRowValue(2, "value", data[3]);
            SetRowValue(3, "value", data[4]);
            SetRowValue(4, "value", data[5]);
        }
    }
}

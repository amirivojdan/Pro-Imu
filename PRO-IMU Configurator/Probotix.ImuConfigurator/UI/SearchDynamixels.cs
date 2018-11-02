
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Probotix.Imu;
using Probotix.Utils;

namespace Probotix.Mpu.UI
{
    public class SearchDynamixels
    {
        private readonly Thread _searchThread;
        private readonly TreeView _treeView;
        private readonly ToolStripProgressBar _progressBar;
        private readonly ToolStrip _toolStrip;
        private readonly ToolStripLabel _toolStripPercent;
        private readonly ToolStripLabel _stateLabel;
        private readonly Mpu6050 _mpu6050;
        private readonly List<int> _baudNumList;

        public SearchDynamixels(Mpu6050 mpu6050,List<int> baudNumList, TreeView treeView, ToolStripProgressBar progressBar, ToolStrip toolStrip, ToolStripLabel percentLabel,ToolStripLabel stateLabel)
        {
            _treeView = treeView;
            _progressBar = progressBar;
            _toolStrip = toolStrip;
            _toolStripPercent = percentLabel;
            _searchThread = new Thread(Search) {Interval = 0};
            _mpu6050 = mpu6050;
            _baudNumList = baudNumList;
            _stateLabel = stateLabel;
        }

        public void Start()
        {
            _treeView.Enabled = false;
            _searchThread.Start(false);
           
        }

        public void Stop()
        {
            _searchThread.Stop(true,false);
            _treeView.Enabled = true;
            SetState("Stoped!");
        }

        private void Search()
        {
            
            double eachSearchPercent = 100.0/(_baudNumList.Count*254.0);
            int baudListIndex = 0;
            bool isFirst = true;
            int foundcounter = 0;
            double percent = 0;
            foreach (var baud in _baudNumList)
            {
                _mpu6050.BaudNum = baud;
                
                for (int id = 0; id < 253; id++)
                {
                    SetState(String.Format("Searching... [{0} bps]-[ID: {1}]",_mpu6050.BaudRate,id));
                    percent += eachSearchPercent;
                    SetProgressValue((int) System.Math.Round(percent,MidpointRounding.AwayFromZero));
                    SetPercent((int)System.Math.Round(percent, MidpointRounding.AwayFromZero));
                    if (_mpu6050.Ping(id))
                    {
                        foundcounter++;
                        if (isFirst)
                        {
                            AddPrentNode(_mpu6050.BaudRate);
                            baudListIndex++;
                            isFirst = false;
                        }
                        AddChild(baudListIndex-1, id); 
                    }
                }
                
                isFirst = true;
            }

            SetState(String.Format("Total of {0} Dynamixel(s) found.",foundcounter));
            TreeViewEnable(true);
        }

        public void TreeViewEnable(bool enable)
        {
            if (!_treeView.InvokeRequired)
            {
                _treeView.Enabled = enable;
            }
            else
            {
                _treeView.Invoke(new MethodInvoker(() => TreeViewEnable(enable)));
            }
        }

        public void SetProgressValue(int value)
        {
            if (!_progressBar.Control.InvokeRequired)
            {
                _progressBar.Value = value;
            }

            else
            {
                _progressBar.Control.Invoke(new MethodInvoker(() => SetProgressValue(value)));
            }
        }

        public void SetPercent(int percent)
        {
            if (!_toolStrip.InvokeRequired)
            {
                _toolStripPercent.Text = String.Format("%{0}", percent);
            }

            else
            {
                _toolStrip.Invoke(new MethodInvoker(() => SetPercent(percent)));
            }
        }

        public void AddPrentNode(int baudrate)
        {
            if (!_treeView.InvokeRequired)
            {
                _treeView.Nodes.Add(new TreeNode(String.Format("{0} bps", baudrate)){Name = String.Format("{0}",baudrate) });
               
            }

            else
            {
                _treeView.Invoke(new MethodInvoker(() => AddPrentNode(baudrate)));
            } 
        }

        public void AddChild(int parentInex,int id)
        {
            if (!_treeView.InvokeRequired)
            {
                _treeView.Nodes[parentInex].Nodes.Add(new TreeNode(string.Format("[ID:{0}]", id)) { Name = String.Format("{0}", id) });
                _treeView.Nodes[parentInex].ExpandAll();
            }

            else
            {
                _treeView.Invoke(new MethodInvoker(() => AddChild(parentInex, id)));
            } 
        }

        public void SetState(string text)
        {
            if (!_toolStrip.InvokeRequired)
            {
                _stateLabel.Text = text;
            }

            else
            {
                _toolStrip.Invoke(new MethodInvoker(() => SetState(text)));
            } 
        }
    }
}

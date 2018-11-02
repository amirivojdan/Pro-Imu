namespace Probotix.Mpu
{
    partial class Simulator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.glControl_visualizer = new OpenTK.GLControl();
            this.timer_Updater = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // glControl_visualizer
            // 
            this.glControl_visualizer.BackColor = System.Drawing.Color.Black;
            this.glControl_visualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl_visualizer.Location = new System.Drawing.Point(0, 0);
            this.glControl_visualizer.Name = "glControl_visualizer";
            this.glControl_visualizer.Size = new System.Drawing.Size(546, 343);
            this.glControl_visualizer.TabIndex = 0;
            this.glControl_visualizer.VSync = false;
            this.glControl_visualizer.Load += new System.EventHandler(this.glControl_visualizer_Load);
            this.glControl_visualizer.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_visualizer_Paint);
            // 
            // timer_Updater
            // 
            this.timer_Updater.Enabled = true;
            this.timer_Updater.Interval = 10;
            this.timer_Updater.Tick += new System.EventHandler(this.timer_Updater_Tick);
            // 
            // Simulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 343);
            this.Controls.Add(this.glControl_visualizer);
            this.Name = "Simulator";
            this.Text = "Simulator";
            this.Resize += new System.EventHandler(this.Simulator_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl glControl_visualizer;
        private System.Windows.Forms.Timer timer_Updater;
    }
}
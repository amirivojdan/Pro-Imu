using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Probotix.IO;
using Probotix.Utils;

namespace Probotix.Imu
{
    public class Mpu6050 : DynamixelNetwork
    {
        private object _key;
        public enum Register : byte
        {
            [Description("Model Number")]
            ModelNumber = 0,

            [Description("Firmware Version")]
            FirmwareVersion = 2,

            [Description("Id")]
            Id = 3,

            [Description("Baud Rate")]
            BaudRate = 4,

            [Description("Auto Send")]
            AutoSend = 5,

            [Description("Gyro Offset X")]
            GyroXOffset = 6,

            [Description("Gyro Offset Y")]
            GyroYOffset = 7,

            [Description("Gyro Offset Z")]
            GyroZOffset = 8,

            [Description("Filter Gain - Beta")]
            Beta = 9,

            [Description("Raw_Accel_X")]
            RawAccelX = 30,

            [Description("Raw_Accel_Y")]
            RawAccelY = 32,

            [Description("Raw_Accel_Z")]
            RawAccelZ = 34,

            [Description("Temprature")]
            RawTemprature = 36,

            [Description("Raw_Gyro_X")]
            RawGyroX = 38,

            [Description("Raw_Gyro_Y")]
            RawGyroY = 40,

            [Description("Raw_Gyro_Z")]
            RawGyroZ = 42,


            [Description("Quaternion_0")]
            FilteredQ0 = 44,

            [Description("Quaternion_1")]
            FilteredQ1 = 48,

            [Description("Quaternion_2")]
            FilteredQ2 = 52,

            [Description("Quaternion_3")]
            FilteredQ3 = 56,


            [Description("Filtered_Angle_Roll")]
            FilteredAngleRoll = 60,

            [Description("Filtered_Angle_Pitch")]
            FilteredAnglePitch = 64,

            [Description("Filtered_Angle_Yaw")]
            FilteredAngleYaw = 68,
        }

        public int Id
        {
            set;
            get;
        }

        public int ModelNumber
        {
            set;
            get;
        }

        public int FirmwareVersion
        {
            set;
            get;
        }

        public bool AutoSend
        {
            set;
            get;
        }

        public double RawAccelX
        {
            set;
            get;
        }

        public double RawAccelY
        {
            set;
            get;
        }

        public double RawAccelZ
        {
            set;
            get;
        }

        public double RawGyroX
        {
            set;
            get;
        }

        public double RawGyroY
        {
            set;
            get;
        }

        public double RawGyroZ
        {
            set;
            get;
        }

        public double FilteredAngleRoll
        {
            set;
            get;
        }

        public double FilteredAnglePitch
        {
            set;
            get;
        }

        public double FilteredAngleYaw
        {
            set;
            get;
        }

        public double Q0
        {
            set;
            get;
        }
        public double Q1
        {
            set;
            get;
        }
        public double Q2
        {
            set;
            get;
        }
        public double Q3
        {
            set;
            get;
        }

        public static Register[] GetRegistersArray()
        {
            return (Register[])Enum.GetValues(typeof(Register));
        }

        public static Register GetRegisterFromDescription(string description)
        {
            var regs = (Register[])Enum.GetValues(typeof(Register));
            return regs.SingleOrDefault(register => description == register.GetDescription());
        }

        public static int RegisterLength(Register reg)
        {
            switch (reg)
            {
                case Register.FilteredAngleRoll:
                case Register.FilteredAnglePitch:
                case Register.FilteredAngleYaw:
                case Register.FilteredQ0:
                case Register.FilteredQ1:
                case Register.FilteredQ2:
                case Register.FilteredQ3:
                case Register.Beta:
                    return 4;

                case Register.RawAccelX:
                case Register.RawAccelY:
                case Register.RawAccelZ:
                case Register.RawGyroX:
                case Register.RawGyroY:
                case Register.RawGyroZ:
                case Register.ModelNumber:
                    return 2;

                //case Register.FirmwareVersion:
                //case Register.Id:
                //case Register.AutoSend:
                //case Register.BaudRate:
                //case Register.RawTemprature:
                //    return 1;

            }
            return 1;
        }


        public Mpu6050(string portName, int baudNum)
            : base(portName, baudNum)
        {
            _key = new object();
        }

        public void Calibrate(int id, int sample)
        {
            lock (_key)
            {


                double x = 0, y = 0, z = 0;
                for (int i = 0; i < sample; i++)
                {
                    UpdateAll(id);
                    x += RawGyroX;
                    y += RawGyroY;
                    z += RawGyroZ;
                }

                x /= sample;
                y /= sample;
                z /= sample;

                WriteByte(id, (int) Register.GyroXOffset, (int) x, true);
                WriteByte(id, (int) Register.GyroYOffset, (int) y, true);
                WriteByte(id, (int) Register.GyroZOffset, (int) z, true);
            }
        }

        public byte[] ReadData(int id, Register start, int count)
        {
            lock (_key)
            {

                if (count <= 0)
                {
                    count = RegisterLength(start);
                }
                return ReadData(id, (byte) start, count);
            }
        }

        public byte[] ReadData(int id, Register start, Register end)
        {
            lock (_key)
            {

                return ReadData(id, start, (int) end + RegisterLength(end) - (int) start);
            }
        }

        public void UpdateUntil(int id, Register start, Register end)
        {

        }

        public void UpdateAll(int id)
        {
            lock (_key)
            {

                var data = ReadData(id, Register.RawAccelX, Register.FilteredAngleYaw);

                RawAccelX = (short) ((data[0] << 8) + data[1]);
                RawAccelY = (short) ((data[2] << 8) + data[3]);
                RawAccelZ = (short) ((data[4] << 8) + data[5]);

                //temp = (short)(data[6] << 8 + data[7]);

                RawGyroX = (short) ((data[8] << 8) + data[9]);
                RawGyroY = (short) ((data[10] << 8) + data[11]);
                RawGyroZ = (short) ((data[12] << 8) + data[13]);

                ByteArrayFloat q0 = new ByteArrayFloat();
                q0.Byte0 = data[14];
                q0.Byte1 = data[15];
                q0.Byte2 = data[16];
                q0.Byte3 = data[17];

                ByteArrayFloat q1 = new ByteArrayFloat();
                q1.Byte0 = data[18];
                q1.Byte1 = data[19];
                q1.Byte2 = data[20];
                q1.Byte3 = data[21];

                ByteArrayFloat q2 = new ByteArrayFloat();
                q2.Byte0 = data[22];
                q2.Byte1 = data[23];
                q2.Byte2 = data[24];
                q2.Byte3 = data[25];

                ByteArrayFloat q3 = new ByteArrayFloat();
                q3.Byte0 = data[26];
                q3.Byte1 = data[27];
                q3.Byte2 = data[28];
                q3.Byte3 = data[29];


                ByteArrayFloat roll = new ByteArrayFloat();
                roll.Byte0 = data[30];
                roll.Byte1 = data[31];
                roll.Byte2 = data[32];
                roll.Byte3 = data[33];

                ByteArrayFloat pitch = new ByteArrayFloat();
                pitch.Byte0 = data[34];
                pitch.Byte1 = data[35];
                pitch.Byte2 = data[36];
                pitch.Byte3 = data[37];

                ByteArrayFloat yaw = new ByteArrayFloat();
                yaw.Byte0 = data[38];
                yaw.Byte1 = data[39];
                yaw.Byte2 = data[40];
                yaw.Byte3 = data[41];

                Q0 = q0.FloatNumber;
                Q1 = q1.FloatNumber;
                Q2 = q2.FloatNumber;
                Q3 = q3.FloatNumber;

                FilteredAngleRoll = roll.FloatNumber*180/Math.PI;
                FilteredAnglePitch = pitch.FloatNumber*180/Math.PI;
                FilteredAngleYaw = yaw.FloatNumber*180/Math.PI;
            }
        }

        public void UpdateFilteredAngles(int id)
        {
            lock (_key)
            {

                var data = ReadData(id, Register.FilteredQ0, Register.FilteredAngleYaw);

                ByteArrayFloat q0 = new ByteArrayFloat();
                q0.Byte0 = data[0];
                q0.Byte1 = data[1];
                q0.Byte2 = data[2];
                q0.Byte3 = data[3];

                ByteArrayFloat q1 = new ByteArrayFloat();
                q1.Byte0 = data[4];
                q1.Byte1 = data[5];
                q1.Byte2 = data[6];
                q1.Byte3 = data[7];

                ByteArrayFloat q2 = new ByteArrayFloat();
                q2.Byte0 = data[8];
                q2.Byte1 = data[9];
                q2.Byte2 = data[10];
                q2.Byte3 = data[11];

                ByteArrayFloat q3 = new ByteArrayFloat();
                q3.Byte0 = data[12];
                q3.Byte1 = data[13];
                q3.Byte2 = data[14];
                q3.Byte3 = data[15];


                ByteArrayFloat roll = new ByteArrayFloat();
                roll.Byte0 = data[16];
                roll.Byte1 = data[17];
                roll.Byte2 = data[18];
                roll.Byte3 = data[19];

                ByteArrayFloat pitch = new ByteArrayFloat();
                pitch.Byte0 = data[20];
                pitch.Byte1 = data[21];
                pitch.Byte2 = data[22];
                pitch.Byte3 = data[23];

                ByteArrayFloat yaw = new ByteArrayFloat();
                yaw.Byte0 = data[24];
                yaw.Byte1 = data[25];
                yaw.Byte2 = data[26];
                yaw.Byte3 = data[27];

                Q0 = q0.FloatNumber;
                Q1 = q1.FloatNumber;
                Q2 = q2.FloatNumber;
                Q3 = q3.FloatNumber;

                FilteredAngleRoll = roll.FloatNumber*180/Math.PI;
                FilteredAnglePitch = pitch.FloatNumber*180/Math.PI;
                FilteredAngleYaw = yaw.FloatNumber*180/Math.PI;

            }
        }

        public void UpdateRawAngles(int id)
        {

        }

        public void UpdateTemrature()
        {

        }

        public void SetBeta(int id, float value)
        {
            lock (_key)
            {

                var valuetoByte = new ByteArrayFloat();
                valuetoByte.FloatNumber = value;
                WriteData(id, (int) Register.Beta,
                    new List<byte> {valuetoByte.Byte0, valuetoByte.Byte1, valuetoByte.Byte2, valuetoByte.Byte3}, true);
            }
        }

        public float GetBeta(int id)
        {
            lock (_key)
            {

                var data = ReadData(id, Register.Beta, 4);
                var beta = new ByteArrayFloat();
                beta.Byte0 = data[0];
                beta.Byte1 = data[1];
                beta.Byte2 = data[2];
                beta.Byte3 = data[3];
                return beta.FloatNumber;
            }
        }
    }
}

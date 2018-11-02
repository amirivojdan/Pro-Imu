using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;

namespace Probotix.IO
{
    public class DynamixelNetwork
    {
        private const int Header = 0xff;

        /// <summary>
        /// The types of instructions that can be sent to Dynamixels using WriteInstruction.
        /// </summary>
        /// <seealso cref="WriteInstruction"/>
        public enum Instruction : byte
        {
            /// <summary>
            /// Respond only with a status packet.
            /// </summary>
            Ping = 1,
            /// <summary>
            /// Read register data.
            /// </summary>
            ReadData = 2,
            /// <summary>
            /// Write register data.
            /// </summary>
            WriteData = 3,
            /// <summary>
            /// Delay writing register data
            /// until an Action instruction is received.
            /// </summary>
            RegWrite = 4,
            /// <summary>
            /// Perform pending RegWrite instructions.
            /// </summary>
            Action = 5,
            /// <summary>
            /// Reset all registers (including ID) to default values.
            /// </summary>
            Reset = 6,
            /// <summary>
            /// Write register data to multiple Dynamixels at once.
            /// </summary>
            SyncWrite = 0x83,
        }

        [Flags]
        public enum ErrorStatus : byte
        {
            /// <summary>
            /// Input Voltage Error
            /// </summary>
            [Description("Input Voltage Error")]
            InputVoltage = 1,
            /// <summary>
            /// Angle Limit Error
            /// </summary>
            [Description("Angle Limit Error")]
            AngleLimit = 2,
            /// <summary>
            /// Overheating Error
            /// </summary>
            [Description("Overheating Error")]
            Overheating = 4,
            /// <summary>
            /// Range Error
            /// </summary>
            [Description("Range Error")]
            Range = 8,
            /// <summary>
            /// Checksum Error
            /// </summary>
            [Description("Checksum Error")]
            Checksum = 0x10,
            /// <summary>
            /// Overload Error
            /// </summary>
            [Description("Overload Error")]
            Overload = 0x20,
            /// <summary>
            /// Instruction Error
            /// </summary>
            [Description("Instruction Error")]
            Instruction = 0x40,
        }

        public const int BroadcastId = 254;
        private object _key;
        private SerialPort Stream
        {
            set;
            get;
        }


        public bool IsOpen
        {
            protected set;
            get;
        }

        public DynamixelNetwork(string portName, int baudNum)
        {
            _key = new object();
            Stream = new SerialPort(portName)
            {
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DtrEnable = true,
                ReadBufferSize = 2048,
                WriteBufferSize = 2048,
                ReadTimeout = 30,
                WriteTimeout = 20
            };
            BaudNum = baudNum;
        }

        private int _baudnum;
        public int BaudNum
        {
            get
            {
                return _baudnum;
            }
            set
            {
                lock (_key)
                {
                    _baudnum = value;
                    Stream.BaudRate = 2000000 / (value + 1);
                }
            }
        }

        public int BaudRate
        {
            get
            {
                lock (_key)
                {
                    return Stream.BaudRate;
                }
            }
            set
            {
                lock (_key)
                {
                    Stream.BaudRate = value;
                    _baudnum = (2000000 / value) - 1;
                }
            }
        }

        public bool Open()
        {
            try
            {
                lock (_key)
                {
                    Stream.Open();
                    IsOpen = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                IsOpen = false;
            }
            return IsOpen;
        }

        public bool Close()
        {
            try
            {
                lock (_key)
                {
                    Stream.Close();
                    IsOpen = false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                IsOpen = true;
            }
            return !IsOpen;
        }

        private enum PacketState
        {
            FirstHeader,
            SecondHeader,
            Id,
            Length,
            Error,
            Parameters,
            Checksum
        }


        private bool FetchPacket(out int id, out byte[] data)
        {
            lock (_key)
            {

                id = Header;
                int length = 0;
                int error = 0;
                int checksum = 0;
                data = null;
                var state = PacketState.FirstHeader;

                while (true)
                {
                    int newByte;
                    try
                    {
                        newByte = Stream.ReadByte();
                    }
                    catch (Exception e)
                    {
                        return false;
                    }

                    switch (state)
                    {
                        case PacketState.FirstHeader:
                            if (newByte == Header)
                            {
                                state = PacketState.SecondHeader;
                            }
                            break;

                        case PacketState.SecondHeader:
                            if (newByte == Header)
                            {
                                state = PacketState.Id;
                            }
                            break;

                        case PacketState.Id:
                            id = newByte;
                            state = PacketState.Length;
                            break;

                        case PacketState.Length:
                            length = newByte - 2;
                            state = PacketState.Error;
                            break;

                        case PacketState.Error:
                            error = newByte; // TODO: Call Some Events on error conditions 
                            if (length <= 0)
                            {
                                data = null;
                                state = PacketState.Checksum;
                            }
                            else
                            {
                                state = PacketState.Parameters;
                            }
                            break;

                        case PacketState.Parameters:
                            data = new byte[length];
                            data[0] = (byte)newByte;
                            length--;
                            int offset = 1;
                            int count;
                            while (length > 0)
                            {
                                try
                                {
                                    count = Stream.Read(data, offset, length);
                                    length -= count;
                                    offset += count;
                                }
                                catch (Exception e)
                                {
                                    return false;
                                }
                            }
                            state = PacketState.Checksum;
                            break;
                        case PacketState.Checksum:
                            checksum = newByte; // TODO: Calculate Checksum and return false if is not eqaul to recieved checksum
                            return true;
                    }

                }
            }
        }


        private int _packetId;
        private int _packetLength;
        private bool GetPacket(int id, int length, out byte[] data)
        {
            data = null;
            if (id == BroadcastId)
                return false;

            var sw = new Stopwatch();
            sw.Start();
            do
            {
                if (FetchPacket(out _packetId, out data))
                {
                    _packetLength = data == null ? 0 : data.Length;
                    if (_packetId == id && _packetLength == length)
                        return true;	// this is the normal return case
                }

            } while (sw.ElapsedMilliseconds < Stream.ReadTimeout);

            return false;
        }

        private void WriteInstruction(int id, Instruction instruction, List<byte> parms)
        {
            lock (_key)
            {
                // command packet sent to Dynamixel servo:
                // [0xFF] [0xFF] [id] [length] [...data...] [checksum]
                var instructionPacket = new List<byte>
                           {
                               0xFF,
                               0xFF,
                               (byte) id,
                               (byte) (((parms != null) ? parms.Count : 0) + 2), // length is the data-length + 2
                               (byte)instruction
                           };

                if (parms != null && parms.Count != 0)
                    instructionPacket.AddRange(parms);

                var cheksum = 0;
                for (var i = 2; i < instructionPacket.Count; i++)
                {
                    cheksum += instructionPacket[i];
                }
                instructionPacket.Add((byte)(~(cheksum & 0xff)));

                try
                {
                    Stream.Write(instructionPacket.ToArray(), 0, instructionPacket.Count);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public int ReadByte(int id, int address)
        {
            byte[] data = ReadData(id, (byte)address, 1);
            return data[0];
        }

        public int ReadWord(int id, int address)
        {
            byte[] data = ReadData(id, (byte)address, 2);
            return (data[1] << 8) + data[0];
        }

        public byte[] ReadData(int id, byte startAddress, int count)
        {
            var parameters = new List<byte> { startAddress, (byte)count };
            WriteInstruction(id, Instruction.ReadData, parameters);
            byte[] data;
            GetPacket(id, count, out data);
            return data;
        }

        public void WriteData(int id, int startAddress, List<byte> values, bool flush)
        {

            var writePacket = new List<byte> { (byte)startAddress };
            writePacket.AddRange(values);
            WriteInstruction(id, flush ? Instruction.WriteData : Instruction.RegWrite, writePacket);
            if (flush)
            {
                byte[] data;
                GetPacket(id, 0, out data);
            }
        }

        public void WriteByte(int id, int registerAddress, int value, bool flush)
        {
            WriteData(id, registerAddress, new List<byte> { (byte)value }, flush);
        }

        public void WriteWord(int id, int registerAddress, int value, bool flush)
        {
            WriteData(id, registerAddress, new List<byte> { (byte)(value & 0xFF), (byte)(value >> 8) }, flush);
        }

        public List<int> ScanIds(int startId, int endId)
        {
            if (endId > 253 || endId < 0)
                return null;
            if (startId > endId || startId < 0)
                return null;

            var ids = new List<int>();
            for (int id = startId; id <= endId; id++)
            {
                if (Ping(id))
                    ids.Add(id);
            }
            return ids;
        }

        public void Action()
        {
            WriteInstruction(BroadcastId, Instruction.Action, null);
        }

        public bool Ping(int id)
        {
            WriteInstruction(id, Instruction.Ping, null);
            byte[] data;
            return GetPacket(id, 0, out data);
        }

    }
}

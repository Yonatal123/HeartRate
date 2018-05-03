using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Timers;
using System.IO;
using Newtonsoft.Json;

namespace SerialPortDeviceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            CommConfig commConfig = null;
            using (StreamReader file = File.OpenText(@"Config/CommConfig.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                commConfig = (CommConfig)serializer.Deserialize(file, typeof(CommConfig));
            }

            m_serialPort = new SerialPort(commConfig.SerialPortName);
            m_serialPort.Open();

            Timer timer = new Timer(commConfig.SendFrequencyInSeconds * 1000);
            timer.Enabled = true;
            timer.Elapsed += onTimeTick;
            timer.Start();

            Console.ReadLine();
        }

        private static void onTimeTick(object sender, ElapsedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            long dateTimeAsLong = dateTime.ToBinary();
            byte[] nowBytes = BitConverter.GetBytes(dateTimeAsLong);
            Random rnd = new Random();
            int value = rnd.Next(50, 200);
            byte[] valueBytes = BitConverter.GetBytes(value);
            byte[] joinedBuffer = new byte[nowBytes.Length + valueBytes.Length];

            for (int i = 0; i < nowBytes.Length; i++)
            {
                joinedBuffer[i] = nowBytes[i];
            }

            int index = 0;
            for (int i = nowBytes.Length; i < joinedBuffer.Length; i++)
            {
                joinedBuffer[i] = valueBytes[index];
                index++;
            }

            m_serialPort.Write(joinedBuffer, 0, joinedBuffer.Length);
        }

        private static SerialPort m_serialPort;
    }

    

    public class HRData
    {
        public HRData(int p_bpm, DateTime p_dateTime)
        {
            Bpm = p_bpm;
            Time = p_dateTime;
        }

        public int Bpm { get; set; }
        public DateTime Time { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainLib.Threading
{
    public class ClsThreadActivety
    {
        public Thread ThreadActivety { get; set; }
        public string Name { get { return this.ThreadActivety.Name; } set { this.ThreadActivety.Name = value; } }
        public int Sleep { get; set; }
        public bool Enable { get; set; }
        public bool Run { get; set; }

        public DgInitialize CallbackInitialize { get; set; }
        public DgActivety CallbackActivety { get; set; }
        public DgFinality CallbackFinality { get; set; }
        //private Thread mThread;
        //private int miSleep = 100;
        //public bool mbEnable = false;
        //public bool mbRun = false;

        public delegate void DgInitialize(string name);
        public delegate void DgActivety(string name);
        public delegate void DgFinality(string name);
        //public DgInitialize mcbInitialize;
        //public DgActivety mcbActivety;
        //public DgFinality mcbFinality;

        public ClsThreadActivety(string Name, bool Enable, bool Run)
        {
            this.ThreadActivety = new Thread(funcThread);
            this.ThreadActivety.Name = Name;
            this.Enable = Enable;
            this.Run = Run;
        }
        public ClsThreadActivety(string Name,
            DgInitialize CallbackInitialize, DgActivety CallbackActivety, DgFinality CallbackFinality,
            bool Enable, bool Run)
        {
            this.ThreadActivety = new Thread(funcThread);
            this.ThreadActivety.Name = Name;
            this.CallbackInitialize = CallbackInitialize;
            this.CallbackActivety = CallbackActivety;
            this.CallbackFinality = CallbackFinality;
            this.Enable = Enable;
            this.Run = Run;
        }

        //public Thread ThreadActivety { get { return mThread; } set { mThread = value; } }
        //public bool Enable { get { return mbEnable; } set { mbEnable = value; } }
        //public bool Run { get { return mbRun; } set { mbRun = value; } }

        //public int Sleep { get { return miSleep; } set { miSleep = value; } }
        //public DgInitialize CallbackInitialize { get { return mcbInitialize; } set { mcbInitialize = value; } }
        //public DgActivety CallbackActivety { get { return mcbActivety; } set { mcbActivety = value; } }
        //public DgFinality CallbackFinality { get { return mcbFinality; } set { mcbFinality = value; } }

        public void Condition(bool Enable, bool Run)
        {
            this.Enable = Enable;
            this.Run = Run;
        }

        public void Start()
        {
            ThreadActivety.Start();
        }
        public void End()
        {
            Condition(false, false);
            //ThreadActivety.Join();
        }


        private void funcThread()
        {
            if (CallbackInitialize != null) CallbackInitialize(ThreadActivety.Name);
            while (Enable)
            {
                while (Run)
                {
                    if (CallbackActivety != null) CallbackActivety(ThreadActivety.Name);
                }
                Thread.Sleep(Sleep);
            }
            if (CallbackFinality != null) CallbackFinality(ThreadActivety.Name);
        }
    }
}

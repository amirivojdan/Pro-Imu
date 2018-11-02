using System.Threading;

namespace Probotix.Utils
{
    public class Thread
    {
        public delegate void CallBackFunction();

        public event CallBackFunction Starting;
        private void OnStarting()
        {
            if (Starting != null) Starting();
        }

        public event CallBackFunction Started;
        private void OnStarted()
        {
            if (Started != null) Started();
        }

        public event CallBackFunction Stopping;
        private void OnStopping()
        {
            if (Stopping != null) Stopping();
        }

        public event CallBackFunction Stopped;
        private void OnStopped()
        {
            if (Stopped != null) Stopped();
        }

        public event CallBackFunction Disposed;
        private void OnDisposed()
        {
            if (Disposed != null) Disposed();
        }

        public event CallBackFunction Paused;
        private void OnPaused()
        {
            if (Paused != null) Paused();
        }

        public event CallBackFunction Continued;
        private void OnContinued()
        {
            if (Continued != null) Continued();
        }

        public int Interval
        {
            set;
            get;
        }
        public bool Enable
        {
            private set;
            get;
        }

        public bool IsPaused
        {
            private set;
            get;
        }

        private ThreadPriority _priority;
        public ThreadPriority Priority
        {
            set
            {
                if (_thread != null)
                {
                    _thread.Priority = value;
                }
                _priority = value;

            }
            get { return _priority; }
        }

        private CallBackFunction _function;
        public CallBackFunction Function
        {
            set { _function = value; }
            get { return _function; }
        }
        private System.Threading.Thread _thread;

        public Thread(CallBackFunction function, ThreadPriority priority = ThreadPriority.Normal, int interval = 50)
        {
            Priority = priority;
            Interval = interval;
            _function = function;
            Enable = false;
        }
        ~Thread()
        {
            Stop(true);
            OnDisposed();
        }
        public void Start(bool inLoop=true)
        {
            OnStarting();
            if (IsPaused) IsPaused = false;
            if (Enable) return;           
            if (inLoop)
            {
                _thread = new System.Threading.Thread(Loop) {Priority = _priority};
            }
            else
            {
                _thread = new System.Threading.Thread(LoopLess) { Priority = _priority }; 
            }
            Enable = true;
            IsPaused = false;
            _thread.Start();
            OnStarted();
        }
        public void Stop(bool force = false,bool join=true)
        {
            Enable = false;
            OnStopping();
            if (force)
            {
                if (_thread != null)
                {
                    _thread.Abort();
                }
            }
            else
            {
                if (_thread != null && join)
                {
                    _thread.Join(100);
                }
               
            }
            OnStopped();
        }
        public void Pause()
        {
            IsPaused = true;
            OnPaused();
        }

        public void Continue()
        {
            IsPaused = false;
            OnContinued();
        }

        public void Sleep(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }

        private void Loop()
        {
            while (Enable)
            {
                if (!IsPaused)
                {
                    _function();
                }
                System.Threading.Thread.Sleep(Interval);
            }
        }
        private void LoopLess()
        {
            _function();         
        }

    }
}

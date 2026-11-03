using System;
using System.Timers;

namespace GetMeOutOfHere
{
    public class Timer
    {
        private readonly System.Timers.Timer _internalTimer;
        private int _remaining;
        private bool _isRunning;
        private bool _isDisposed;

        public int Remaining
        {
            get
            {
                if (_isDisposed) return 0;
                return _remaining;
            }
        }

        public bool IsExpired
        {
            get
            {
                if (_isDisposed) return true;
                return _remaining <= 0;
            }
        }

        public event EventHandler<int>? Tick;
        public event EventHandler? Elapsed;

        public Timer()
        {
            _internalTimer = new System.Timers.Timer(1000); // 1 second interval
            _internalTimer.Elapsed += OnTimerElapsed;
        }

        public void Start(int seconds)
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(Timer));
            _remaining = seconds;
            _isRunning = true;
            _internalTimer.Start();
        }

        public void Stop()
        {
            if (_isDisposed) return;
            _isRunning = false;
            _internalTimer.Stop();
        }

        public void Reset(int seconds)
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(Timer));
            _remaining = seconds;
            _internalTimer.Start();
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (!_isRunning) return;

            _remaining--;

            if (Tick != null)
            {
                Tick(this, _remaining);
            }

            if (_remaining <= 0)
            {
                _isRunning = false;
                _internalTimer.Stop();
                if (Elapsed != null)
                {
                    Elapsed(this, EventArgs.Empty);
                }
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;
            _internalTimer.Stop();
            _internalTimer.Dispose();
        }
    }
}

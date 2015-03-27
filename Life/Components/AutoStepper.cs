using Life.Components.Configuration;
using System;
using System.Windows.Threading;

namespace Life.Components
{
    public class AutoStepper
    {
        readonly AppConfig _config;
        DispatcherTimer _timer;

        public AutoStepper(AppConfig config)
        {
            _config = config;
        }

        public void Initialize(Action makeStep, Dispatcher dispatcher)
        {
            _timer = new DispatcherTimer(_config.AnimationSpeed, DispatcherPriority.Render, (o, e) => makeStep(), dispatcher);
        }

        public void Toggle()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }
            else
            {
                _timer.Start();
            }
        }
    }
}

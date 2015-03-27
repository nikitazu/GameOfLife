using System;
using System.Windows.Threading;

namespace Life.Components
{
    public class AutoStepper
    {
        DispatcherTimer _timer;

        public void Initialize(GameComponent component, Dispatcher dispatcher)
        {
            EventHandler autoStep = (o, e) =>
            {
                component.MakeStep();
            };

            _timer = new DispatcherTimer(component.Config.AnimationSpeed, DispatcherPriority.Render, autoStep, dispatcher);
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

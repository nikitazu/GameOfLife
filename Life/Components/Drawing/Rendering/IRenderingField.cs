using Life.Core;
using System.Windows.Controls;

namespace Life.Components.Drawing.Rendering
{
    public interface IRenderingField : IField<IRenderingFieldItem>
    {
        void Initialize(Canvas screen);
    }
}

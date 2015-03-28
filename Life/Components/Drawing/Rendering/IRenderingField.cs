using Life.Core;
using System.Windows.Controls;

namespace Life.Components.Drawing.Rendering
{
    public interface IRenderingField : IField<IRenderingFieldItem>
    {
        IRenderingFieldItem CreateItemAt(int i, int j, Canvas screen);
    }
}

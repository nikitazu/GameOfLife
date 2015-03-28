using Life.Components.Configuration;
using Life.Core;
using System.Windows.Media;

namespace Life.Components.Drawing
{
    public class RenderingFieldFast : TranslatingMatrix<PathGeometry>
    {
        public RenderingFieldFast(AppConfig config) : base(config.FieldSize)
        {
            // empty
        }
    }
}

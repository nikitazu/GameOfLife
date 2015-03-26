using Life.Components.Configuration;
using Life.Core;
using System.Windows.Shapes;

namespace Life.Components.Drawing
{
    public class RenderingField : TranslatingMatrix<Rectangle>
    {
        public RenderingField(AppConfig config) : base(config.FieldSize)
        {
            // empty
        }
    }
}

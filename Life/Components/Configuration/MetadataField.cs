using Life.Core;

namespace Life.Components.Configuration
{
    public class MetadataField : TranslatingMatrix<CellMetadata>
    {
        public MetadataField(AppConfig config) : base(config.FieldSize)
        {
            ForEach((i, j, value) =>
            {
                this[i, j] = new CellMetadata
                {
                    Generation = 0
                };
            });
        }
    }

    public static class MetadataExtensions
    {
        public static void Reset(this IField<CellMetadata> metadata)
        {
            metadata.ForEach((i, j, value) =>
            {
                value.Generation = 0;
            });
        }
    }
}

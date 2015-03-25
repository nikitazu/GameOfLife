
namespace Life.Core
{
    public interface IRules<T>
    {
        T EvaluateCellStateFromPopulation(T state, int population);

        int EvaluatePopulationForCoordinates(IField<T> field, int i, int j);
    }
}

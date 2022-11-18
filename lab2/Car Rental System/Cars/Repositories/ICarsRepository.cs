using Cars.ModelsDB;
using Cars.ModelsDTO;

namespace Cars.Repositories
{
    public enum ExitCode
    {
        Success,
        Error
    }
    
    public interface ICarsRepository
    {
        Task<List<Car>> FindAll(int page, int size);
    }
}
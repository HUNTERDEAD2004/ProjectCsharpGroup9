namespace API.IService
{
    public interface IAllServices<T> where T : class
    {
        ICollection<T> GetAll();
        T GetByID(dynamic id);
        bool Create(T obj);
        bool Upate(T obj);
        bool Delete(dynamic id);
    }
}

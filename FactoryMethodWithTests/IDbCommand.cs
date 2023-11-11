
namespace FactoryMethod;

public interface IDbCommand<T, Key> { 
    int Insert(T data);
    bool Update(Key key, T newData);
    bool Delete(Key key);
    Task<T> GetAsync(Key key);
    List<T> GetAll();
}

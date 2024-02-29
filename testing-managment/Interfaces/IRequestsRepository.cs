using testing_managment.Models.Entities;

namespace testing_managment.Interfaces
{
    public interface IRequestsRepository
    {
        //Создание объекта
        public bool Create<T>(T obj);

        //Получение всех объектов
        List<T> GetAll<T>() where T : class;

        //Получение объекта по Id
        public T GetById<T>(Guid Id) where T : class;

        //Изменение
        public bool Update<T>(T obj);

        //Удаление 
        public bool Delete<T>(T obj);
    }
}

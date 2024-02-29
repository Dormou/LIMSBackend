using System.Collections.Generic;
using testing_managment.Data;
using testing_managment.Interfaces;
using testing_managment.Models.Entities;

namespace testing_managment.Repository
{
    public class RequestsRepository : IRequestsRepository
    {
        private readonly DBContextTests _context;

        //Управление дутами
        public RequestsRepository(DBContextTests context)
        {
            _context = context;
        }

        //Создание 
        public bool Create<T>(T obj)
        {
            _context.Add(obj);
            return Save();
        }

        //Удаление
        public bool Delete<T>(T obj)
        {
            _context.Remove(obj);
            return Save();
        }

        //Получение всех объектов
        public List<T> GetAll<T>() where T : class 
        {
            var result = _context.Set<T>().ToList();

            return result;
        }

        //Получение по Id
        public T GetById<T>(Guid Id) where T : class
        {
            return _context.Set<T>().Find(Id);
        }

        //Обновление
        public bool Update<T>(T obj)
        {
            _context.Update(obj);
            return Save();
        }

        //Сохранение
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}


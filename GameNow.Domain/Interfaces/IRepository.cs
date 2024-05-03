using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Interfaces
{
	public interface IRepository<T>
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		Task Add(T entity);
		void Update(T entity);
		void Delete(int id);
	}
}

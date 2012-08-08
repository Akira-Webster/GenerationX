using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
// SharePoint 14
using Microsoft.SharePoint;
// SPGenesis Framework http://spgenesis.codeplex.com/
using SPGenesis.Core;
using SPGenesis.Entities;
// SharePoint GenerationX
using SP.GX.Library.Generation.Interfaces;

namespace SP.GX.Entities.Employees
{
	public interface IGenericEmployeesRepository : IInitializeRepository
	{
		Employee GetById(int id);
		IQueryable<Employee> GetByQuery();
		Employee Create(Action<Employee> populate);
		void Update(Employee entity);
		void Delete(Employee entity);
	}

	public class GenericEmployeesRepository : IGenericEmployeesRepository, IInitializeRepository
	{
		public SPList List {get; protected set;}

		public void Initialize(Microsoft.SharePoint.SPWeb web)
		{
			this.List = EmployeeList.Instance.TryGetList(web);
		}

		public void Initialize(string webUrl)
		{
			var manager = EmployeeList.Instance.GetList(webUrl, SPGENListInstanceGetMethod.ByUrl);
			if(manager != null)
			{
				this.List = manager.List;
			}

		}

		public Employee GetById(int id)
		{
			return SPGENEntityManager<Employee, EmployeeMapper>.Instance.GetEntity(this.List, id);
		}

		public IQueryable<Employee> GetByQuery()
		{
			return SPGENEntityManager<Employee, EmployeeMapper>.Instance.GetQueryableList(this.List);
		}

		public Employee Create(Action<Employee> populate)
		{
			var entity = new Employee();
			populate(entity);
			SPGENEntityManager<Employee, EmployeeMapper>.Instance.CreateNewListItem(entity, this.List);
			return entity;
		}

		public void Update(Employee entity)
		{
			SPGENEntityManager<Employee, EmployeeMapper>.Instance.UpdateListItem(entity, this.List);
		}

		public void Delete(Employee entity)
		{
			SPGENEntityManager<Employee, EmployeeMapper>.Instance.DeleteListItem(entity, this.List);
		}

	}

}


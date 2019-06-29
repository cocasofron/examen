using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Services
{
    public interface IPackagesService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        PaginatedList<Package> GetAll(int page,string sender);
        IEnumerable<Package> Group();
        Package GetById(int id);
        Package Create(Package package);
        Package Upsert(int id, Package package);
        Package Delete(int id);
    }
    public class PackagesService : IPackagesService
    {
        private ExamenDbContext context;
        public PackagesService(ExamenDbContext context)
        {
            this.context = context;
        }

        public Package Create(Package package)
        {
            context.Packages.Add(package);
            context.SaveChanges();
            return package;
        }

        public Package Delete(int id)
        {
            var existing = context.Packages.FirstOrDefault(package => package.Id == id);
            if (existing == null)
            {
                return null;
            }

            context.Packages.Remove(existing);
            context.SaveChanges();

            return existing;
        }

        public PaginatedList<Package> GetAll(int page,string sender)
        {
            IQueryable<Package> result = context
                .Packages
                .OrderBy(m => m.Id);

            PaginatedList<Package> paginatedResult = new PaginatedList<Package>();
            paginatedResult.CurrentPage = page;


            if (sender != null)
            {
                result = result.Where(e => e.Sender.Equals(sender));
            }
        
            paginatedResult.NumberOfPages = (result.Count() - 1) / PaginatedList<Package>.EntriesPerPage + 1;
            result = result
            .Take(PaginatedList<Package>.EntriesPerPage);
            paginatedResult.Entries = result.ToList();

            return paginatedResult;
        }

        public IEnumerable<Package> Group()
        {
            IQueryable<Package> result = context
                .Packages
                .OrderBy(x=>x.Id);

            return result;
        }



        public Package GetById(int id)
        {
            return context.Packages
                .FirstOrDefault(f => f.Id == id);
        }

        public Package Upsert(int id, Package package)
        {
            var existing = context.Packages.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (existing == null)
            {
                context.Packages.Add(package);
                context.SaveChanges();
                return package;
            }
            package.Id = id;
            context.Packages.Update(package);
            context.SaveChanges();
            return package;
        }

    }
}

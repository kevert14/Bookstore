using System;
using System.Linq;

namespace Bookstore.Models
{
    public interface IBookstoreRepository
    {
        IQueryable<Books> Books { get; }
    }
}

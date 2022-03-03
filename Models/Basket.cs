using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Basket
    {
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();
        // add item to basket
        public virtual void AddItem (Books book, int qty)
        {
            BasketLineItem line = Items
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            if (line == null)
            {
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }
        // remove items from cart
        public virtual void RemoveItem (Books book)
        {
            Items.RemoveAll(x => x.Book.BookId == book.BookId);
        }
         
        public virtual void ClearBasket ()
        {
            Items.Clear();
        }

        // calculate cart total
        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);

            return sum;
        }
    }

    // for diplaying line items
    public class BasketLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Books Book { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcLearning.Data.Entities
{
    public class Shop
    {
        public Guid Id { get; set; }  // Уникальный идентификатор магазина
        public string Name { get; set; } = string.Empty;  // Название магазина
        public string Description { get; set; } = string.Empty;  // Описание магазина
        public string ImageUrl { get; set; } = "shop_default.png";

        public DateTime CreatedAt { get; set; } = DateTime.Now;  // Дата создания магазина

        // Владелец магазина (ссылка на пользователя, который является владельцем магазина)
        public string OwnerId { get; set; }  // Это будет ключ от IdentityUser
        public User Owner { get; set; }  // Навигационное свойство для владельца магазина

        // Список товаров, которые принадлежат магазину
        public List<Product> Products { get; set; } = new List<Product>();  // Инициализация пустым списком

        // Список клиентов, которые когда-либо делали покупки в этом магазине
        public List<User> Customers { get; set; } = new List<User>();
    }
}

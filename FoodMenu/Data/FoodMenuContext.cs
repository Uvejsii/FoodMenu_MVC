using Microsoft.EntityFrameworkCore;
using FoodMenu.Models;

namespace FoodMenu.Data
{
    public class FoodMenuContext : DbContext
    {
        public FoodMenuContext(DbContextOptions<FoodMenuContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishIngredient>().HasKey(di => new 
            {
                di.DishId,
                di.IngredientId
            });

            modelBuilder.Entity<DishIngredient>().HasOne(d => d.Dish).WithMany(di => di.DishIngredients).HasForeignKey(d => d.DishId);
            modelBuilder.Entity<DishIngredient>().HasOne(i => i.Ingredient).WithMany(di => di.DishIngredients).HasForeignKey(i => i.IngredientId);

            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name = "Margheritta", Price = 7.50, ImageUrl = "https://img.taste.com.au/PwXf3RRU/w720-h480-cfill-q80/taste/2016/11/eat-pray-love-39581-3.jpeg" }
                );
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Tomato Sauce"},
                new Ingredient { Id = 2, Name = "Mozzarella" }
                );
            modelBuilder.Entity<DishIngredient>().HasData(
                new DishIngredient { DishId = 1, IngredientId = 1 },
                new DishIngredient { DishId = 1, IngredientId = 2 }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
    }
}

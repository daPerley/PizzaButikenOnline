using PizzaButikenOnline.Models.DishViewModels;

namespace PizzaButikenOnline.Services
{
    public interface IDishService
    {
        bool AddDish(DishViewModel viewModel);
        bool EditDish(int id, DishViewModel viewModel);
    }
}

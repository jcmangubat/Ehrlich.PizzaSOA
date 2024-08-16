using System.ComponentModel;

namespace Ehrlich.PizzaSOA.Domain.Constants;

public class Rules
{
    public enum PizzaTypeCategoriesEnum
    {
        BBQ,
        Chicken,
        Classic,
        FourCheese,
        GlutenFree,
        Gourmet,
        Hawaiian,
        Margherita,
        MeatLovers,
        Seafood,
        Spicy,
        Supreme,
        Vegetarian,
        Veggie
    }

    public enum PizzaSizesEnum
    {
        [Description("Small")]
        S,
        [Description("Medium")]
        M,
        [Description("Large")]
        L,
        [Description("Extra Large")]
        XL,
        [Description("Double Extra Large")]
        XXL
    }
}
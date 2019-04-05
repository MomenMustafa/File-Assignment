using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Item
    { 
        public string id, name;
        public int quantity;

        public Item(string id, string name, int quantity)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
        }
    }

    class ShoppingCart
    {
        public string userId;
        public List<Item> userItems;
    }
    class Program
    {
     
        static void Main(string[] args)
        {

            string input;

            List<ShoppingCart> shoppingCart = new List<ShoppingCart>();

        begin:
            Console.WriteLine("Please enter \"add\" to add itesm to you shopping cart or \"display\" to display the itmes in your shopping cart");
            input = Console.ReadLine();

            if (input == "add")
            {
                List<Item> items = new List<Item>();
                string continueEntring = "y";

                do
                {
                    // getting the item's data
                    Console.WriteLine("please enter the item ID");
                    string itemId = Console.ReadLine();

                    Console.WriteLine("please enter the itme name");
                    string name = Console.ReadLine();

                    Console.WriteLine("please enter the quantity");
                    int quantity = int.Parse(Console.ReadLine());


                    Item item = new Item(itemId, name, quantity);
                    items.Add(item);

                    // checking if the user want to add other item.
                    Console.WriteLine("Would you like to add new itme? y/n");
                    continueEntring = Console.ReadLine();

                } while (continueEntring != "n");

                Console.WriteLine("please enter your id");

                ShoppingCart newShoppingCart = new ShoppingCart();
                newShoppingCart.userId = (Console.ReadLine());
                newShoppingCart.userItems = items;

                //  to avoid repeating the same id for the same person.
                if (shoppingCart.Exists(s => s.userId == newShoppingCart.userId) == true)
                {
                    for (int i = 0; i < shoppingCart.Capacity; ++i)
                        if (shoppingCart[i].userId == newShoppingCart.userId)
                        {
                            shoppingCart[i].userItems = items;
                            break;
                        }
                }
                else
                    shoppingCart.Add(newShoppingCart);

                // the last section about adding itmes is over and now we are about to check 
                //if the user wants to remove any of the recently added items.
                Console.WriteLine("would you like to remove any recently added item? y/n");
                string removeItem = Console.ReadLine();

                while (removeItem == "y")
                {
                    Console.WriteLine("please enter the item id which you want to delete");
                    string removedItemId = (Console.ReadLine());
                    //shoppingCart.SingleOrDefault(x=>x.userId==newShoppingCart.userId).userItems.SingleOrDefault(y=>y.id==removedItemId)
                    for (int i = 0; i < shoppingCart.Capacity; ++i)
                        if (shoppingCart[i].userId == newShoppingCart.userId)
                        {
                            var removedItem = shoppingCart[i].userItems.SingleOrDefault(x => x.id == removedItemId);

                            if (removedItem != null)
                                shoppingCart[i].userItems.Remove(removedItem);
                            break;
                        }

                    Console.WriteLine("would you like to remove another item> y/n");
                    removeItem = Console.ReadLine();
                }

            }
            else if (input == "display")
            {
                Console.WriteLine("please enter user id to display it's itmes");
                string userId = Console.ReadLine();

                //var itemList = shoppingCart.SingleOrDefault(x => x.userId == userId);
                List<Item> desiredShoppingCart = new List<Item>();
                bool flag = false;
                for(int i=0;i<shoppingCart.Capacity;i++)
                {
                    if(shoppingCart[i].userId==userId)
                    {
                        flag = true;
                        desiredShoppingCart = shoppingCart[i].userItems;
                        break;
                    }
                }

                if (flag != false)
                {
                    foreach (var item in desiredShoppingCart)
                    {
                        Console.WriteLine("id: " + item.id + " name: " + item.name + " quantity: " + item.quantity);
                    }
                }
            }
            else
            {
                Console.WriteLine("invalid");
                goto begin;
            }

            Console.WriteLine("repeat the process? y/n");
            string repeat = Console.ReadLine();
            if (repeat != "n")
                goto begin;

        }
    }
}

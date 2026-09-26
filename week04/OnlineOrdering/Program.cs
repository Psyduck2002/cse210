using System;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address(
            "123 Main Street",
            "Sacramento",
            "CA",
            "USA"
        );

        Customer customer1 = new Customer("John Smith", address1);

        Order order1 = new Order(customer1);

        order1.AddProduct(new Product("Keyboard", "KB101", 49.99, 1));
        order1.AddProduct(new Product("Mouse", "MS202", 24.99, 2));
        order1.AddProduct(new Product("Headset", "HS303", 39.99, 1));

        Address address2 = new Address(
            "45 King Street",
            "Toronto",
            "Ontario",
            "Canada"
        );

        Customer customer2 = new Customer("Emily Johnson", address2);

        Order order2 = new Order(customer2);

        order2.AddProduct(new Product("Laptop Stand", "LS404", 29.99, 1));
        order2.AddProduct(new Product("USB Cable", "UC505", 9.99, 3));

        Console.WriteLine("ORDER 1");
        Console.WriteLine();

        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());

        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());

        Console.WriteLine();
        Console.WriteLine($"Total Price: ${order1.GetTotalCost():F2}");

        Console.WriteLine();
        Console.WriteLine("----------------------------");
        Console.WriteLine();

        Console.WriteLine("ORDER 2");
        Console.WriteLine();

        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());

        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());

        Console.WriteLine();
        Console.WriteLine($"Total Price: ${order2.GetTotalCost():F2}");
    }
}
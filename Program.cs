using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace Xmas_tree_Json
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText("Json.txt");

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var xmasProducts = JsonSerializer.Deserialize<products>(jsonString, options);

            foreach (var product in xmasProducts.Products)
            {
                product.prices.price_min.amountD = product.prices.price_min.MyConvertToDouble();
                Console.WriteLine($"{product.full_name} {product.prices.price_min.amountD}");
                
            }
            Console.ReadLine();

            //Deshman(xmasProducts);
            //GiftForTescha(xmasProducts);
            //AwesomeGiftToMYself(xmasProducts);
            //GiftDeshman50BYN(xmasProducts);
            //GiftRandom80BYN(xmasProducts);
            //GiftGetAllCost(xmasProducts);
            //GiftGetAllCostLess40Byn(xmasProducts);
            //GiftGetAll25Byn(xmasProducts);


        }
        static void Deshman(products products)
        {
            //вариант для илитки
            Console.WriteLine("Вот шо есть по дешману");
            var selectedProducts = products.Products.Where(x => x.prices.price_min.amountD < 10.00);
            foreach (var product in selectedProducts)
            {
                Console.WriteLine($"{product.full_name} {product.prices.price_min.amountD}");
            }
            //вариант для бидла)
            //foreach (var product in Products.Products)
            //{
            //    if (product.prices.price_min.amountD < 10.00)
            //    {
            //        Console.WriteLine($"{product.full_name} {product.prices.price_min.amountD}");
            //    }
            //}
            Console.ReadLine();
        }

        static void GiftForTescha(products products)
        {
            Console.WriteLine("Вот шо есть для любимой тещеньки");
            var teschaGift = products.Products.OrderBy(x => x.prices.price_min.amountD).First();            
            Console.WriteLine($" {teschaGift.full_name} {teschaGift.prices.price_min.amount}");            
            Console.ReadLine();
        }
        static void AwesomeGiftToMYself(products products)
        {
            Console.WriteLine("Вот шо есть для меня любимого");
            var myself = products.Products.OrderBy(x => x.prices.price_min.amountD).Last();
            Console.WriteLine($" {myself.full_name} {myself.prices.price_min.amount}");
            Console.ReadLine();
        }
        static void GiftDeshman50BYN(products products)
        {
            Console.WriteLine("выбрать самые дешевые подарки пока не закончатся 50 рублей, вывести список подарков");
            var ordered =  products.Products.OrderBy(x => x.prices.price_min.amountD).ToList();  
            double summCost = 0.00;
            var Deshman50Byn = new List<Product>();
            int i = 0;
            while ((50.00-summCost) > ordered[i].prices.price_min.amountD)
            {
                summCost += ordered[i].prices.price_min.amountD;
                Deshman50Byn.Add(ordered[i]);
                i++;
            }

            foreach (var product in Deshman50Byn)
            {
                Console.WriteLine($" {product.full_name} {product.prices.price_min.amountD}");
            }            
            Console.ReadLine();
        }

        static void GiftRandom80BYN(products products)
        {
            Console.WriteLine("выбрать случайные подарки покупая всё пока не закончатся 80 рублей - вывести список подарков");            
            double summCost = 0.00;
            var Random80Byn = new List<Product>();
            int i = 0;
            Random random = new Random();
            while ((80.00 - summCost) >= products.Products.Min(x => x.prices.price_min.amountD))
            {
                i = random.Next(products.Products.Count);
                if (80.00 > summCost + products.Products[i].prices.price_min.amountD)
                {
                    summCost += products.Products[i].prices.price_min.amountD;
                    Random80Byn.Add(products.Products[i]);
                }
                
            }
            Console.WriteLine($"игого потрачено {summCost:00.00}, даж вывел кросево");
            foreach (var product in Random80Byn)
            {
                Console.WriteLine($" {product.full_name} {product.prices.price_min.amountD}");
            }
            Console.ReadLine();
        }
        static void GiftGetAllCost(products products)
        {
            Console.WriteLine("посчитать сколько будут стоить все подарки");
            double summCost = 0.00;
            foreach (var product in products.Products)
            {
                summCost += product.prices.price_min.amountD;
            }
            Console.WriteLine($"за все {summCost:00.00}, даж вывел кросево");

            Console.ReadLine();
        }
        static void GiftGetAllCostLess40Byn(products products)
        {
            Console.WriteLine("посчитать сколько будут стоить все подарки с ценой до 40 рублей");
            double summCost = 0.00;
            foreach (var product in products.Products)
            {
                if (product.prices.price_min.amountD < 40.00)
                {
                    summCost += product.prices.price_min.amountD;

                }
            }
            Console.WriteLine($"за все {summCost:00.00}, даж вывел кросево");

            Console.ReadLine();
        }
        static void GiftGetAll25Byn(products products)
        {
            Console.WriteLine("вывести список подарков с ценой до 25 рублей");
            foreach (var product in products.Products)
            {
                if (product.prices.price_min.amountD < 25.00)
                {
                    Console.WriteLine($" {product.full_name} {product.prices.price_min.amountD}");
                }
            }
            Console.ReadLine();
        }
    }
}

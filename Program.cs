using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Xmas_tree_Json
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText("Json.txt");

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            //var xmasProducts = JsonSerializer.Deserialize<products>(jsonString, options);
            var xmasProducts = JsonConvert.DeserializeObject<products>(jsonString);
            foreach (var product in xmasProducts.Products)
            {
                product.Prices.Price_min.CashInDouble = product.Prices.Price_min.MyConvertToDouble();
                Console.WriteLine($"{product.Name} {product.Prices.Price_min.CashInDouble}");
                
            }
            Console.ReadLine();

            //кароч тут для все с запросами DB и мы ленивые и ебанули перегрузку
            //FillProductsDB(xmasProducts);
            //Deshman();
            GiftForTescha();

            static void FillProductsDB(products products)
            {
                using (var context = new ProductDbContext())
                {
                    foreach (var product in products.Products)
                    {
                        context.ProductDBList.Add(new ProductDB()
                        {
                            Name = product.Name,
                            Price = product.Prices.Price_min.CashInDouble,
                            Url = product.Url
                        });
                    }
                    
                  
                    context.SaveChanges();
                }
            }
            static void Deshman()
            {
                Console.WriteLine("Вот шо есть по дешману");
                using (var context = new ProductDbContext())
                {                    
                    var selectedProducts = context
                        .ProductDBList.Where(x => x.Price < 10.00).ToList();
                    foreach (var product in selectedProducts)
                    {
                        Console.WriteLine($"{product.Id} {product.Name}  {product.Price}  ");
                    }
                    SelectMenu(selectedProducts);
                }
                
            }
            static void GiftForTescha()
            {
                Console.WriteLine("Вот шо есть для любимой тещеньки");
                using (var context = new ProductDbContext())
                {
                    var teschaGift = context
                        .ProductDBList.OrderBy(x => x.Price).First();
                    Console.WriteLine($" {teschaGift.Name} {teschaGift.Price}");
                    GetSite(teschaGift);
                    Console.ReadLine();
                }
                
            }

            //кароч тут для JSON без DB

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
            var selectedProducts = products.Products.Where(x => x.Prices.Price_min.CashInDouble < 10.00).ToList();
            int index = 0;
            foreach (var product in selectedProducts)
            {
                Console.WriteLine($"{index}) {product.Name} {product.Prices.Price_min.CashInDouble}");
                index++;
            }
            SelectMenu(selectedProducts);            
            Console.ReadLine();
        }
        static void GiftForTescha(products products)
        {
            Console.WriteLine("Вот шо есть для любимой тещеньки");
            var teschaGift = products.Products.OrderBy(x => x.Prices.Price_min.CashInDouble).First();            
            Console.WriteLine($" {teschaGift.Name} {teschaGift.Prices.Price_min.CashInString}");
            GetSite(teschaGift);
            Console.ReadLine();
        }
        static void AwesomeGiftToMYself(products products)
        {
            Console.WriteLine("Вот шо есть для меня любимого");
            var myself = products.Products.OrderBy(x => x.Prices.Price_min.CashInDouble).Last();
            Console.WriteLine($" {myself.Name} {myself.Prices.Price_min.CashInString}");
            GetSite(myself);
            Console.ReadLine();
        }
        static void GiftDeshman50BYN(products products)
        {
            Console.WriteLine("выбрать самые дешевые подарки пока не закончатся 50 рублей, вывести список подарков");
            var ordered =  products.Products.OrderBy(x => x.Prices.Price_min.CashInDouble).ToList();  
            double summCost = 0.00;
            var Deshman50Byn = new List<Product>();
            int i = 0;
            while ((50.00-summCost) > ordered[i].Prices.Price_min.CashInDouble)
            {
                summCost += ordered[i].Prices.Price_min.CashInDouble;
                Deshman50Byn.Add(ordered[i]);
                i++;
            }

            foreach (var product in Deshman50Byn)
            {
                Console.WriteLine($" {product.Name} {product.Prices.Price_min.CashInDouble}");
            }
            SelectMenu(Deshman50Byn);
            Console.ReadLine();
            
        }
        static void GiftRandom80BYN(products products)
        {
            Console.WriteLine("выбрать случайные подарки покупая всё пока не закончатся 80 рублей - вывести список подарков");            
            double summCost = 0.00;
            var Random80Byn = new List<Product>();
            int i = 0;
            Random random = new Random();
            while ((80.00 - summCost) >= products.Products.Min(x => x.Prices.Price_min.CashInDouble))
            {
                i = random.Next(products.Products.Count);
                if (80.00 > summCost + products.Products[i].Prices.Price_min.CashInDouble)
                {
                    summCost += products.Products[i].Prices.Price_min.CashInDouble;
                    Random80Byn.Add(products.Products[i]);
                }                
            }
            Console.WriteLine($"игого потрачено {summCost:00.00}, даж вывел кросево");
            foreach (var product in Random80Byn)
            {
                Console.WriteLine($" {product.Name} {product.Prices.Price_min.CashInDouble}");
            }
            SelectMenu(Random80Byn);
            Console.ReadLine();
        }
        static void GiftGetAllCost(products products)
        {
            Console.WriteLine("посчитать сколько будут стоить все подарки");
            double summCost = 0.00;
            foreach (var product in products.Products)
            {
                summCost += product.Prices.Price_min.CashInDouble;
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
                if (product.Prices.Price_min.CashInDouble < 40.00)
                {
                    summCost += product.Prices.Price_min.CashInDouble;

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
                if (product.Prices.Price_min.CashInDouble < 25.00)
                {
                    Console.WriteLine($" {product.Name} {product.Prices.Price_min.CashInDouble}");
                }
            }
            Console.ReadLine();
        }

        static void GetSite(Product product)
        {
            Console.WriteLine("зайти на сайт ? y / n");
            while (true)
            {
                string response = Console.ReadLine();
                if (response == "y")
                {
                    Console.WriteLine($"тады вот вам ссылка {product.Url}");
                    //System.Diagnostics.Process.Start(product.Url);
                    break;
                }
                else if (response == "n") { break; }
                else { Console.WriteLine("Некорректный ввод"); }
            }
        }
        static void GetSite(ProductDB product)
        {
            Console.WriteLine("зайти на сайт ? y / n");
            while (true)
            {
                string response = Console.ReadLine();
                if (response == "y")
                {
                    Console.WriteLine($"тады вот вам ссылка {product.Url}");
                    //System.Diagnostics.Process.Start(product.Url);
                    break;
                }
                else if (response == "n") { break; }
                else { Console.WriteLine("Некорректный ввод"); }
            }
        }

        static void SelectMenu(List<Product> products)
        {
            while (true)
            {
                Console.WriteLine("Выберите номер товара который вас интересует");
                string input = Console.ReadLine();
                int selIndex = 0;
                bool result = int.TryParse(input, out selIndex);
                if (result != true) { Console.WriteLine("Какую то фигню вместо индекса вводите"); }
                if (selIndex >= products.Count) { Console.WriteLine("чет индекс слишком большой"); continue; }
                Console.WriteLine($"Выбранный товар {products[selIndex].Name}");
                GetSite(products[selIndex]);
                Console.WriteLine("Желаете продолжить? y/n");
                string response = Console.ReadLine();
                if (response == "y") { }
                else if (response == "n") { break; }
                else { Console.WriteLine("Некорректный ввод"); }
            }
        }
        static void SelectMenu(List<ProductDB> products)
        {
            while (true)
            {
                Console.WriteLine("Выберите номер товара который вас интересует");
                string input = Console.ReadLine();
                int selIndex = 1;
                bool result = int.TryParse(input, out selIndex);
                if (result != true) { Console.WriteLine("Какую то фигню вместо индекса вводите"); }
                //if (selIndex >= products.Count) { Console.WriteLine("чет индекс слишком большой"); continue; }
                Console.WriteLine($"Выбранный товар {products.Where(x => x.Id == selIndex).FirstOrDefault().Name}");
                GetSite(products.Where(x => x.Id == selIndex).FirstOrDefault());
                Console.WriteLine("Желаете продолжить? y/n");
                string response = Console.ReadLine();
                if (response == "y") { }
                else if (response == "n") { break; }
                else { Console.WriteLine("Некорректный ввод"); }
            }
        }
    }
}

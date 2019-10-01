using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {

        //LLAMA A ALA RELACION DE PEDIDOS Y CLIENTES
        public static DataClasses2DataContext context = new DataClasses2DataContext();

        static void Main(string[] args)
        {

            //QUERY
            Console.WriteLine("<-----------QUERY------------>");
            //IntroToLINQ();
            //DataSource();
            //Filtering();
            //Ordering();
            //Grouping();
            //Grouping2();
            Joining();
            Console.WriteLine(System.Environment.NewLine);
            

            //LAMBDA
            Console.WriteLine("<-----------LAMBDA------------>");
            //IntroToLINQ2();
            //DataSource2();
            //Filtering2();
            //Ordering2();
            //GroupingLamb();
            //Grouping2lamb(); 
            Joining2();


            //EJECUTA EL CONSOLE
            Console.Read();
        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                            from num in numbers
                            where (num % 2) == 0
                            select num;

            foreach (int num in numQuery)
            {
                Console.Write( num);
            }
        }
        static void IntroToLINQ2()
        {
            List<int> l = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

            List<int> res = l.Where(n => (n % 2) == 0).ToList();

            foreach (var v in res)
            {
                Console.WriteLine(v);
            }
        }
          

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach(var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void DataSource2()
        {
            var queryAllCustomers2 = context.clientes; 

            foreach (var item2 in queryAllCustomers2)
            {
                Console.WriteLine(item2.NombreCompañia);
            }
        }


        static void Filtering()
        {
            var queryLondoCustomers = from cust in context.clientes
                                      where cust.Ciudad == "Londres"
                                      select cust;
            foreach(var item in queryLondoCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }

        }
        static void Filtering2()
        {
            var queryLondoCustomers2 = context.clientes
                                      .Where(cli => cli.Ciudad == "Londres");
            foreach (var item2 in queryLondoCustomers2)
            {
                Console.WriteLine(item2.Ciudad);
            }

        }
        

        static void Ordering()
        {
            var queryLondoCustomers3 = from cust in context.clientes
                                      where cust.Ciudad == "Londres"
                                      orderby cust.NombreCompañia ascending
                                      select cust;

            foreach (var item in queryLondoCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }
        static void Ordering2()
        {
            var queryLondoCustomers31 = context.clientes
                                       .Where(cli => cli.Ciudad  =="Londres")
                                       .OrderBy(cli => cli.NombreCompañia);

            foreach (var item2 in queryLondoCustomers31)
            {
                Console.WriteLine(item2.NombreCompañia);
            }

        }


        static void Grouping()
        {
            var queryCustomerByCity = from cust in context.clientes
                                      group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomerByCity)
            {

                Console.WriteLine(customerGroup.Key);
                foreach(clientes customer in customerGroup)
                {
                    Console.WriteLine("{0}", customer.NombreCompañia);
                }
            }
        }
        static void GroupingLamb()
        {
            var queryCustomerByCity2 =  context.clientes
                                      .GroupBy(cli => cli.Ciudad);

            foreach (var customerGroup2 in queryCustomerByCity2)
            {

                Console.WriteLine(customerGroup2.Key);
                foreach (clientes customer2 in customerGroup2)
                {
                    Console.WriteLine("{0}", customer2.NombreCompañia);
                }
            }
        }


        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach(var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }

        }
        static void Grouping2lamb()
        {
            var custQuery2 = context.clientes
                            .GroupBy(cli => cli.Ciudad)
                            .Where(cli => cli.Count() > 2)
                            .OrderBy(cli => cli.Key);

            foreach (var item2 in custQuery2)
            {
                Console.WriteLine(item2.Key);
            }

        }


        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }

        }

        static void Joining2()
        {
            var innerJoinQuery2 = context.clientes
                                .Join(
                                        context.Pedidos, 
                                        cli => cli.idCliente,
                                        dist => dist.IdCliente,
                                 (cli,dist) => new { CustomerName = cli.NombreCompañia, DistributorName = dist.PaisDestinatario });

            foreach (var item2 in innerJoinQuery2)
            {
                Console.WriteLine(item2.CustomerName);
            }
            }

    }
}

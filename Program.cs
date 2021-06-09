using System;
using System.Collections.Generic;
using System.IO;

namespace Projeto_de_Vendas
{
    class Product
    {
        public string Name;
        public int Category;
        public double Price;

        public Product()
        {

        }
        public Product(string name, int category, double price)
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }
    class Client
    {
        public string Name;
        public string? Tell;
        public string? Aderess;
        public string? Obs;

        public Client()
        {

        }
        public Client(string name)
        {
            Name = name;
            Tell = null;
            Aderess = null;
            Obs = null;
        }

        public Client(string name, string tell, string aderess, string obs)
        {
            Name = name;
            Tell = tell;
            Aderess = aderess;
            Obs = obs;
        }
    }
    class Shell
    {
        public Product Merchandise;
        public Client Buyer;
        //public double TotalPrice;
        public int Count;

        public Shell(Product merchan, Client buyer, int count)
        {
            Merchandise = merchan;
            Buyer = buyer;
            Count = count;
        }

        public double ValorTotal()
        {
            return Count * Merchandise.Price;
        }
    }
    class Program
    {
        public const string SYSTEM_NAME = "MyFinancialSystem";
        public const string SYSTEM_VERSION = "v0.1";
        public const string ARQUIVO_ENDERECO = "/\\home\\default\\Área de Trabalho\\Gabriel\\Study\\Scripts\\Scripts C#\\Projeto de Vendas\\Produtos.txt";

        static void Header()
        {
            Console.Clear();
            Console.WriteLine("=-=-" +  SYSTEM_NAME + SYSTEM_VERSION +"-=-=\n");
        }

        static void MainMenu()
        {
            Console.WriteLine(" Opcoes: \n");
            Console.WriteLine(" [ 1 ] - Cadastrar Produto;"
                + "\n [ 2 ] - Cadastrar Cliente;"
                + "\n [ 3 ] - Efetuar Compra;"
                + "\n [ 4 ] - Ver Extrato;"
                + "\n [ 5 ] - Produtos Cadastrados;"
                + "\n [ 6 ] - Clientes Cadastrados"
                + "\n [ 7 ] - Salvar Produtos;"
                + "\n [ 0 ] - Sair\n");
        }
        
        static char MenuReading()
        {
            char ans;
            Console.Write(" Digite sua opcao: [   ]");
            ans = char.Parse(Console.ReadLine());
            return ans;
        }

        static void RegisterVerify(string? name)
        {
            Console.Clear();
            Console.WriteLine();

            if(name != null)
            {
                Console.WriteLine(" Cadastrado com suceso");
            }
            else
            {
                Console.WriteLine(" ERRO: nao foi possivel cadastrar, tente novamente");
            }
            Console.WriteLine(" \nAperte ENTER para continuar...");
            Console.ReadLine();
        }

        static Product RegisterProduct()
        {
            Header();

            Console.WriteLine(" -Cadastro de Produto- \n");
            Console.Write(" Digite o nome do produto: ");
            string? name = Console.ReadLine();

            Console.Write(" Digite a categoria do produto: ");
            int category = int.Parse(Console.ReadLine());

            Console.Write(" Digiter o preco do produto: ");
            double price = double.Parse(Console.ReadLine());

            RegisterVerify(name);

            Product product = new Product(name, category, price);

            return product;
        }

        static Client RegisterClient()
        {
            Header();

            Console.WriteLine(" -Cadastro de Cliente- \n");
            Console.Write(" Digite o nome do cliente: ");
            string? name = Console.ReadLine();

            Console.Write(" Digite o telefone do cliente :");
            string tell = Console.ReadLine();

            Console.Write(" Digite o endereco do cliente: ");
            string aderess = Console.ReadLine();

            Console.Write(" Observacoes: ");
            string obv = Console.ReadLine();

            Client client = new Client(name, tell, aderess, obv);

            RegisterVerify(client.Name);

            return client;
        }

        static Product SelectProduct(List<Product> products)
        {
            int i = 0;

            Console.WriteLine(" -Selecione o produto-\n");

            foreach (Product obj in products)
            {
                i++;
                Console.WriteLine(" [ " + i + " ] - " + obj.Name);
            }
            
            Console.Write(" \nDigite o numero do produto: ");
            int opt = int.Parse(Console.ReadLine());
            return products[opt-1];
        }
        
        static Client SelectClient(List<Client> clients)
        {
            int i = 0;

            Console.WriteLine(" -Selecione o produto-\n");

            foreach (Client obj in clients)
            {
                i++;
                Console.WriteLine(" [ "+i+" ] - " + obj.Name);
            }
            Console.Write(" \nDigite o numero do cliente: ");
            int opt = int.Parse(Console.ReadLine());
            return clients[opt-1];
        }
        static Shell Sale(List<Product> products, List<Client> clients)
        {
            Header();

            Product selectedP = SelectProduct(products);

            Console.WriteLine("\n Produto Selecionado: " + selectedP.Name + "\n");

            Console.Write(" Informe a quantidade: ");
            int cont = int.Parse(Console.ReadLine());

            Client selectedC = SelectClient(clients);

            Console.WriteLine("\n Cliente Selecionado: " + selectedC.Name);
            Console.WriteLine("\n Aperte ENTER para continuar...");
            Console.ReadLine();

            Shell sale = new Shell(selectedP, selectedC, cont);

            return sale;
        }

        static void PrintSales(List<Shell> sales)
        {
            double final_extract = 0;
            int i = 0;

            Header();

            Console.WriteLine(" -------------------------------------");

            Console.WriteLine(" -Vendas-\n");
            Console.WriteLine(" -Produto-    -Valor Uni-    -Client-");

            Console.WriteLine(" -------------------------------------");

            foreach (Shell obj in sales)
            {
                
                Console.Write(obj.Merchandise.Name 
                    + "      R$ " + obj.Merchandise.Price
                    + "      " + obj.Buyer.Name);
                Console.WriteLine(" Valor toral:       " + obj.ValorTotal() + "\n");
                Console.WriteLine(" -------------------------------------");
                final_extract += obj.ValorTotal();
                i++;
            }
            
            Console.WriteLine(" Valor total ao final do periodo: R$ " + final_extract);
            Console.WriteLine(" -------------------------------------");

            Console.WriteLine("\n Aperte ENTER para continuar...");
            Console.ReadLine();
        }

        static void SaveProducts(List<Product> products)
        {
            StreamWriter x;
            x = File.CreateText(ARQUIVO_ENDERECO);

            foreach (Product obj in products)
            {
                x.WriteLine(obj.Name + "&" + obj.Category + "&" + obj.Price);
            }
            x.Close();
        }

        static void PrintProducts(List<Product> products)
        {
            Header();

            Console.WriteLine(" Produtos Cadastrados:\n");

            foreach (Product obj in products)
            {
                Console.WriteLine(obj.Name);
            }
            Console.WriteLine("\n Pressione enter para continuar...");
            Console.ReadLine();
        }

        static void PrintClients(List<Client> clients)
        {
            Header();

            Console.WriteLine( "-Clientes Cadastrados-\n");

            foreach (Client obj in clients)
            {
                Console.WriteLine(obj.Name);
            }

            Console.WriteLine("\n Pressione enter para continuar...");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();
            List<Client> clients = new List<Client>();
            List<Shell> sales = new List<Shell>();
            char ans;

            do
            {
                Header();

                MainMenu();

                ans = MenuReading();

                switch (ans)
                {
                    case '1': products.Add(RegisterProduct()); break;
                    case '2': clients.Add(RegisterClient()); break;
                    case '3': sales.Add(Sale(products, clients)); break;
                    case '4': PrintSales(sales); break;
                    case '5': PrintProducts(products); break;
                    case '6': PrintClients(clients); break;
                    case '7': SaveProducts(products); break;
                }
            }while(ans != '0');

            Header();

            Console.WriteLine("Bye!");
        }
    }
}

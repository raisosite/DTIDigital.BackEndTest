using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Console;

namespace DTIDigital.BackEndTest
{
    class Program
    {
       
        static List<Customer> listCustomer = new List<Customer>();
        static List<Carbohidrate> listAl1 = new List<Carbohidrate>()
        {
            new Carbohidrate() { Name = "Leite" , Calories = 200},
            new Carbohidrate() { Name = "Arroz Integral" , Calories = 100},
            new Carbohidrate() { Name = "Arroz Branco" , Calories = 300},
            new Carbohidrate() { Name = "Macarrão Integral" , Calories = 120},
            new Carbohidrate() { Name = "Feijão" , Calories = 180}
        };
        static List<Protein> listAl2 = new List<Protein>()
        {
            new Protein() { Name = "File de Frango" , Calories = 100} ,
            new Protein() { Name = "File de Boi" , Calories = 110} ,
            new Protein() { Name = "File de Porco" , Calories = 150} ,
            new Protein() { Name = "File de Salmão" , Calories = 80} ,
            new Protein() { Name = "Ovo" , Calories = 90}
        };
        static List<Fats> listAl3 = new List<Fats>()
        {
            new Fats() {Name = "Castanhas", Calories = 120},
            new Fats() {Name = "Abacate", Calories = 300},
            new Fats() {Name = "Óleo de coco", Calories = 150},
            new Fats() {Name = "Queijo Branco", Calories = 180}
        };


        static Dictionary<int, string> listMenus = new Dictionary<int, string>()
        {
            { 1, "Cadastrar Cliente / Paciente" },
            { 2, "Cadastrar Consulta" },
            { 3, "Excluir Registro de cliente" },
            { 4, "Exibir registro de clientes" },
            { 5, "Exibir registro de consultas" },
            { 6, "Calcular Dieta" },
            { 9, "Sair" }

        };

        static void Main(string[] args)
        {
            #region _ifDebug
#if DEBUG
            listCustomer.Add(new Customer
            {
                FullName = "Rayson Matheus Santos da Fonseca",
                BirthDay = "02/12/1994",
                Email = "rayson.m.fonseca@gmail.com",
                Adress = "Rua Zoroastro de Souza, 42, Dom Bosco",
                PhoneNumber = "031 973435919",
                Id = 1,

                ListConsultation = new List<Consultation>()
                {
                    new Consultation()
                    {
                        Wheight = 78.9f,
                        FatPercentage = 10f,
                        CalRestriction = 420,
                        PhysicalSensation = "Muita FOME!",
                        Data = DateTime.Now
                    }
                }

            }) ;
#endif
            #endregion

            int countOperations = 0;
            char option = '0';
            while (option != '9')
            {
                bool hasCustomer = (listCustomer.Count >= 1);                
                if(countOperations == 0) ShowTitle();
                if (hasCustomer)
                {
                    ShowMenu();
                }
                else
                {
                    ShowMenu(2 , 3, 4, 5);
                }

                try
                {
                    if (char.IsDigit(option))
                        option = char.Parse(ReadLine());
                    else
                        continue;
                }
                catch (FormatException)
                {
                    continue;
                }

                switch (option)
                {
                    case '1':
                        RegisterCustomer();
                        break;

                    case '2':
                        if (hasCustomer)
                            RegisterConsultation();
                        break;

                    case '3':
                        if (hasCustomer)
                            DeleteCustomer();
                        break;

                    case '4':
                        if (hasCustomer)
                            ShowCustomerList();
                        break;

                    case '5':
                        if(hasCustomer)
                            ShowConsultationList();                       
                        break;

                    case '6':                        
                            CalculateDiet();
                        break;

                    default:
                        break;
                }

                countOperations++;

            }
            ShowEndMessage();
            ReadKey();

        }
        static void RegisterCustomer()
        {
            Clear();
            string name, email, adress, phone, birthDay;
            int id = 0;

            WriteLine(" Digite o nome completo: ");
            name = ReadLine();
            WriteLine(" Digite o email: ");
            email = ReadLine();
            WriteLine(" Digite o endereço: ");
            adress = ReadLine();
            WriteLine(" Digite o telefone: ");
            phone = ReadLine();
            WriteLine(" Digite a data de nascimento: ");
            birthDay = ReadLine();

            if (listCustomer.Count > 0)
            {               
               id = listCustomer.Last().Id;               
            }

            Customer customer = new Customer(id + 1, name, adress, email, phone, birthDay);            
            listCustomer.Add(customer);
            

            ShowSucess();
        }
        static void RegisterConsultation()
        {
            Clear();
            bool found = true;
            int id, restricition = 0;
            string sensation;
            float wheight = 0, fat = 0;
            Customer customer = new Customer();

            WriteLine("Digite o ID do cliente: ");         
            if (int.TryParse(ReadLine(), out id))
            {

                customer = FindCustomer(id);
                if (customer != null)
                {
                    ShowCustomer(customer);
                }
                else
                {
                    found = false;
                    WriteLine("\n\n\tID não encontrado. Favor verificar novamente ...\n\n");
                }
            }
            else
            {
                found = false;
                WriteLine("\n\n\tID não encontrado. Favor verificar novamente ...\n\n");
            }

            bool pass = false;

            if (found)
            {
                while (!pass)
                {
                    WriteLine(" Digite o peso: ");
                    pass = float.TryParse( ReadLine(), out wheight );
                }

                pass = false;

                while (!pass) 
                {
                    WriteLine(" Digite o percentual de gordura: ");
                    pass = float.TryParse(ReadLine(), out fat);
                }

                pass = false;


                WriteLine(" Digite a sensação: ");
                sensation = ReadLine();

                while (!pass)
                {
                    WriteLine(" Digite a restrição de calorias: ");
                    pass = int.TryParse(ReadLine(), out restricition);
                }

                pass = false;
               

                Consultation consult = new Consultation()
                {
                    Wheight = wheight,
                    FatPercentage = fat,
                    PhysicalSensation = sensation,
                    CalRestriction = restricition,
                    Data = DateTime.Now
                    
                };

                customer.ListConsultation.Add(consult);

                ShowSucess();

            }
        }
        static void ShowConsultationList()
        {
            Clear();
            WriteLine("\t\t\tLista de consultas:\n");

            bool found = true;
            Customer customer = new Customer();

            WriteLine("Digite o ID do cliente: ");
            int id;
            if (int.TryParse(ReadLine(), out id))
            {

                customer = FindCustomer(id);
                if (customer != null)
                {
                    ShowCustomer(customer);
                }
                else
                {
                    found = false;
                    WriteLine("\n\n\tID não encontrado. Favor verificar novamente ...\n\n");
                }
            }
            else
            {
                found = false;
                WriteLine("\n\n\tID não encontrado. Favor verificar novamente ...\n\n");
            }

            if (found)
                {
                    foreach (Consultation c in customer.ListConsultation)
                    {
                        WriteLine("\t-------------------------------------------------------------");
                        WriteLine($"\tData: {c.Data.ToString("dd/MM/yyyy : hh:mm")}");
                        WriteLine($"\tPeso: {c.Wheight}Kg");
                        WriteLine($"\tGordura: {c.FatPercentage}%");
                        WriteLine($"\tSensação: {c.PhysicalSensation}");
                        WriteLine($"\tRestrições: {c.CalRestriction}cal");
                        WriteLine("\t-------------------------------------------------------------\n");
                    }
                }
            
           
        }
        static void ShowCustomerList()
        {
            Clear();
            WriteLine("\t\t\tLista de clientes:\n");            
            foreach(Customer c in listCustomer)
            {
                WriteLine("\t-------------------------------------------------------------");
                WriteLine($"\tID: {c.Id}");
                WriteLine($"\tNome: {c.FullName} | Nascimento: {c.BirthDay}" );
                WriteLine($"\tEmail: {c.Email} | Telefone: {c.PhoneNumber}");
                WriteLine($"\tEndereço: {c.Adress}");
                WriteLine("\t-------------------------------------------------------------\n");
            }
        }
        static void ShowCustomer(Customer c)
        {
            WriteLine("\t-------------------------------------------------------------");
            WriteLine($"\tID: {c.Id}");
            WriteLine($"\tNome: {c.FullName} | Nascimento: {c.BirthDay}");
            WriteLine($"\tEmail: {c.Email} | Telefone: {c.PhoneNumber}");
            WriteLine($"\tEndereço: {c.Adress}");
            WriteLine("\t-------------------------------------------------------------\n");
        }
        static Customer FindCustomer(int id)
        {
            try
            {
                return listCustomer.Find(p => p.Id == id);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        static void DeleteCustomer()
        {
            Clear();
            WriteLine("Digite o ID do cliente para deletar: ");
            int id;
            bool found = true;
            Customer customer = new Customer();

            if (int.TryParse(ReadLine(), out id))
            {

                customer = FindCustomer(id);
                if (customer != null)
                {
                    ShowCustomer(customer);
                }
                else
                {
                    found = false;
                    WriteLine("\n\n\tID não encontrado. Favor verificar novamente ...\n\n");
                }
            }
            else
            {
                found = false;
                WriteLine("\n\n\tID não encontrado. Favor verificar novamente ...\n\n");
            }

            if (found)
            {
                listCustomer.Remove(customer);
                WriteLine("\t\tRegistro removido\n");
            }
        }
        static void CalculateDiet()
        {
            Clear();
            WriteLine("Digite a quantidade de calorias permitidas: ");
            int calories = int.Parse(ReadLine());         
            
            
            string foods = string.Empty;
            List<string> listFoods = new List<string>();

            for(int x = 0; x < listAl1.Count; x++)
            {                
                if (listAl1[x].Calories <= calories)
                {                 
                    for (int y = 0; y < listAl2.Count; y++)
                    {
                        if (listAl2[y].Calories' + listAl1[x].Calories <= calories)
                        {                   
                            for (int z = 0; z < listAl3.Count; z++)
                            {
                                if (listAl3[z].Calories + listAl2[y].Calories + listAl1[x].Calories <= calories)
                                {                                 
                                    listFoods.Add(listAl1[x].Name + "|" + listAl1[x].Calories  + ";" + listAl2[y].Name + "|" + listAl2[y].Calories + ";" + listAl3[z].Name +  "|" + listAl3[z].Calories);                                    
                                }
                            }

                        }                      
                    }
                }                             
            }

            WriteLine("\t\tCombinações encontradas:\n");
            listFoods.ForEach(s => 
            {
                WriteLine("-----------------------------------------------------------");
                WriteLine("\tCarboídrato: {0} - {1} cal" , s.Split(';')[0].Split('|')[0] , s.Split(';')[0].Split('|')[1] );
                WriteLine("\tProteína: {0} - {1} cal", s.Split(';')[1].Split('|')[0] , s.Split(';')[1].Split('|')[1] );
                WriteLine("\tGordura: {0} - {1} cal", s.Split(';')[2].Split('|')[0] , s.Split(';')[2].Split('|')[1] );
                if(listFoods.Last() == s) WriteLine("-----------------------------------------------------------");
            });

            WriteLine("\n");
            
        }

        #region Menu
        static void ShowTitle()
        {
            WriteLine("\t\t\tBem-Vindo ao Consultorio da Marina!\n");
        }
        static void ShowMenu()
        {
            foreach(KeyValuePair<int, string> kp in listMenus)
            {
                WriteLine($"{kp.Key} - {kp.Value}");
            }
        }
        static void ShowMenu(params int [] indexNotToShow)
        {
            foreach (KeyValuePair<int, string> kp in listMenus)
            {
                if( !indexNotToShow.Contains(kp.Key) )
                    WriteLine($"{kp.Key} - {kp.Value}");
            }
        }
        static void ShowEndMessage()
        {
            Clear();
            WriteLine("\n \t Programa finalizado... Adeus :)");
            WriteLine("\t Digite qualquer tecla para sair.");
        }
        static void ShowSucess()
        {
            WriteLine("\n \t Cadastro realizado!");
            Thread.Sleep(1000);
            Clear();
        }
        #endregion

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DTIDigital.BackEndTest.Testes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RegisterCustomer_Teste()
        {
           
            string name, email, adress, phone, birthDay;

            name = "Rayson Matheus Santos da Fonseca";
            email = "rayson@gmail.com";
            adress = "Rua Zoroastro de Souza, 42 , Bairro Dom Bosco";
            phone = "55 031 97343-5919";
            birthDay = "02/12/1994";         

            Customer customer = new Customer(1, name, adress, email, phone, birthDay);      
        }

        [TestMethod]
        public void RegisterConsultation_Teste()
        {          

            string sensation;
            float wheight, fat;
            int restriction;
            
            wheight = 80.2f;
            fat = 10.2f;
            sensation = "Fome!";
            restriction = 300;


            Consultation consult = new Consultation()
            {
                Wheight = wheight,
                FatPercentage = fat,
                PhysicalSensation = sensation,
                CalRestriction = restriction,
                Data = DateTime.Now
            };
        }
    }
}

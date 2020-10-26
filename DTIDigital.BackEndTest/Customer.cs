using System.Collections.Generic;


namespace DTIDigital.BackEndTest
{
    public class Customer
    {

        private List<Consultation> listConsultation = new List<Consultation>();        
        

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthDay { get; set; }     

        public List<Consultation> ListConsultation
        {
            get
            {
                return listConsultation;
            }

            set
            {
                listConsultation = value;
            }
        }

        public Customer(int id, string fullName, string adress, string email, string phoneNumber, string birthDay)
        {
            Id = id;
            FullName = fullName;
            Adress = adress;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDay = birthDay;                        
        }

        public Customer()
        {

        }

    }
}

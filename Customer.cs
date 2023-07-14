using System;

namespace ui_asg4
{
    abstract class Customer : ICustomer
    {
        private int houseAge;
        private decimal houseSize;
        private decimal paddockSize;
        private string creditCardNumber;
        private CustomerTaskDelegate customerTasks = null;

        public int HouseAge { get => houseAge; set => houseAge = value; }
        public decimal HouseSize { get => houseSize; set => houseSize = value; }
        public decimal PaddockSize { get => paddockSize; set => paddockSize = value; }
        public string CreditCardNumber
        {
            get => CreditCardHelper.ObscureCreditCardNumber(creditCardNumber);
            set => creditCardNumber = value;
        }
        public CustomerTaskDelegate CustomerTasks { get => customerTasks; set => customerTasks = value; }

        public Customer()
        {
            QueueTasks();
        }

        public abstract void CreateDesign();

        public void EstimateWork()
        {
            Console.WriteLine("Done estimating work.");
        }

        public void ArrangeWorkers()
        {
            Console.WriteLine("Arranged the workers");
        }

        public override string ToString()
        {
            return string.Format("House age is {0} year(s) and house size of {1:n} sqft with paddock size of {2:n} sqft having credit card number: {3}", houseAge, houseSize, paddockSize, CreditCardNumber);
        }


        private void QueueTasks()
        {
            customerTasks += EstimateWork;
            customerTasks += ArrangeWorkers;
            customerTasks += CreateDesign;
        }

        public int CompareTo(ICustomer otherCustomer)
        {
            return houseAge.CompareTo(otherCustomer.HouseAge);
        }
    }
}


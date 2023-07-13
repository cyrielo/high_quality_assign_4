using System;
using System.Collections;

namespace ui_asg4
{
    public delegate void CustomerTaskDelegate();
    internal interface ICustomer : IComparable<ICustomer>
    {
        int HouseAge { get; set; }
        decimal HouseSize { get; set; }
        decimal PaddockSize { get; set; }
        string CustomerName { get; set; }
        string CreditCardNumber { get; set; }
        CustomerTaskDelegate CustomerTasks { get; set; }
        void CreateDesign();
        void EstimateWork();
        void ArrangeWorkers();
        string ToString();
    }
}

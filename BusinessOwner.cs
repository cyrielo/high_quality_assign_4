using System;

namespace ui_asg4
{
    internal class BusinessOwner : Customer
    {

        public BusinessOwner() { }
        public override void CreateDesign()
        {
            Console.WriteLine("Creating the design of the customer lobbies... Done");
        }
    }
}

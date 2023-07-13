using System;
using ui_asg4;

namespace ui_asg4
{
    class Farmer : Customer
    {
        public Farmer() { }
        public override void CreateDesign()
        {
            Console.WriteLine("Creating the design of the grain storage areas... Done");
        }

        public override string ToString()
        {
            return string.Format("Farmer's name is {0}, {1}", base.CustomerName, base.ToString()); ;
        }
    }

}

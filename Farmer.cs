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

    }

}

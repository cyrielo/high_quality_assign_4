using System;

namespace ui_asg4
{
    //a design of sun rooms
    internal class HomeOwner : Customer
    {
        public HomeOwner() { }
        public override void CreateDesign()
        {
            Console.WriteLine("Creating a design of sun rooms ... Done");
        }
    }
}

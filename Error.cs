using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ui_asg4
{
    public partial class TextBoxError
    {
        private string message = "";
        private TextBox textBox = new TextBox();
        public string Message { get => message; set => message = value; }
        public TextBox TextBox { get => textBox; set => textBox = value; }
    }
}

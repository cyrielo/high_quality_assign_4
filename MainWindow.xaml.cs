using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ui_asg4;

namespace ui_asg4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<string> CustomerType { get; set; }
        public string FilePath { get; set; }
        public string SelectedCustomerType { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Run();
        }

        public void Run()
        {
            CustomerType = new List<string> 
            { "Home owner", "Business owner", "Farmer" };
            DataContext = this;
            CustomerTypeDrodown.ItemsSource = CustomerType;
            FilePath = SetupFile();
        }

        public void HandleCustomerTypeDrodown(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            SelectedCustomerType = selectedItem.ToString();
        }

        public List<TextBoxError> HandleValidation(out ArrayList parsedData)
        {
            List<TextBoxError> errors = new List<TextBoxError> { };
            parsedData = new ArrayList();

            if (!int.TryParse(HouseAge.Text, out int houseAge))
            {
                TextBoxError error = new TextBoxError() {
                    Message = "Invalid house age",
                    TextBox = HouseAge
                };
                errors.Add(error);
            }

            if (!decimal.TryParse(Housesize.Text, out decimal houseSize))
            {
                TextBoxError error = new TextBoxError()
                {
                    TextBox = Housesize,
                    Message = "House size is not valid"
                };
                errors.Add(error);
            }

            if (!decimal.TryParse(PaddockSize.Text, out decimal paddockSize))
            {
                TextBoxError error = new TextBoxError()
                {
                    TextBox = PaddockSize,
                    Message = "Paddock size is not valid"
                };
                errors.Add(error);
            }

            if (!CreditCardHelper.IsCreditCardNumberValid(CardNo.Text))
            {
                TextBoxError error = new TextBoxError()
                {
                    TextBox = CardNo,
                    Message = "Invalid credit card"
                };
                errors.Add(error);
            }

            if (errors.Count < 1)
            {
                parsedData.Add(houseAge);
                parsedData.Add(houseSize);
                parsedData.Add(paddockSize);
                parsedData.Add(CardNo.Text);
            }
            return errors;
        }

        public void DisplayErrors(List<TextBoxError> errors)
        {
            foreach (TextBoxError error in errors)
            {
                error.TextBox.Background = Brushes.Red;
                error.TextBox.Foreground = Brushes.White;
            }
        }

        public void ClearError()
        {
            HouseAge.Background = Brushes.White;
            CardNo.Background = Brushes.White;
            Housesize.Background = Brushes.White;
            PaddockSize.Background = Brushes.White;

            HouseAge.Foreground = Brushes.Black;
            CardNo.Foreground = Brushes.Black;
            Housesize.Foreground = Brushes.Black;
            PaddockSize.Foreground = Brushes.Black;
        }

        public void ClearForm()
        {
            HouseAge.Text = "";
            CardNo.Text = "";
            Housesize.Text = "";
            PaddockSize.Text = "";
        }

        public void HandleSubmit(object sender, RoutedEventArgs e)
        {
            ArrayList parsedData = new ArrayList();
            List<TextBoxError> errors = HandleValidation(out parsedData);
            ClearError();
            if (errors.Count > 0)
            {
                DisplayErrors(errors);
                return;
            }
            parsedData.Insert(0, SelectedCustomerType);
            SaveData(parsedData);
            ClearForm();
        }

        public void DisplayContent(object sender, RoutedEventArgs e)
        {
            FileStream fs = null;

        }

        public List<ICustomer> ReadCustomerFile()
        {
            List<ICustomer> customers = new List<ICustomer>();
            FileStream fs = null;
            try
            {
                fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                while (fs.Position != fs.Length)
                {
                    string customerType = br.ReadString();
                    int houseAge = br.ReadInt32();
                    decimal houseSize = br.ReadDecimal();
                    decimal paddockSize = br.ReadDecimal();
                    string CardNo = br.ReadString();
                    Customer customer = null;

                    switch (customerType)
                    {
                        case "Home owner":
                            customer = new HomeOwner();
                            break;
                        case "Business owner":
                            customer = new BusinessOwner();
                            break;
                        case "Farmer":
                            customer = new Farmer();
                            break;
                    }
                    customer.HouseAge = houseAge;
                    customer.CreditCardNumber = CardNo;
                    customer.HouseSize = houseSize;
                    customer.PaddockSize = paddockSize;
                    customers.Add(customer);
                }

                br.Close();
            } catch(IOException ioe)
            {
                Console.WriteLine(ioe);
            }
            finally
            {
                fs?.Close();
            }
            return customers;
        }

        public void SaveData(ArrayList data)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                foreach(object datum in data)
                {
                    if (datum is string)
                    {
                        bw.Write((string) datum);
                    }

                    if (datum is int)
                    {
                        bw.Write((int) datum);
                    }

                    if (datum is decimal)
                    {
                        bw.Write((decimal) datum);
                    }
                }
                bw.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.ToString());
            }
            finally
            {
                fs.Close();
            }
        }

        public string SetupFile() 
        {
            string dir = @"customer_data\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string filePath = System.IO.Path.Combine(dir, "customer_file");
            if (File.Exists(filePath))
            {
                File.Delete(filePath); 
            }
            FileStream fs = File.Create(filePath);
            fs.Close();
            return filePath;
        }
    }
}

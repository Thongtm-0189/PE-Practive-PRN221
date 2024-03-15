using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Wpf_pe_part2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Product> Products;
        public MainWindow()
        {
            InitializeComponent();
            Products = new List<Product>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id = int.Parse(txId.Text);
                double price = double.Parse(txPrice.Text);
                string name = txName.Text;
                Products.ForEach(p =>
                {
                    if (p.Id == Id)
                    {
                        throw new Exception("Id was exited");
                    }
                });
                Products.Add(new Product
                {
                    Id = Id,
                    Price = price,
                    Name = name
                });
                lvData.ItemsSource = null;
                lvData.ItemsSource = Products;
                txId.Clear();
                txName.Clear();
                txPrice.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // read file json
            //string jsonData = File.ReadAllText(filePath);
            //dynamic jsonObject = JsonConvert.DeserializeObject(jsonData);
            string jsonData = JsonConvert.SerializeObject(Products, Newtonsoft.Json.Formatting.Indented);
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = saveFileDialog.FileName;
                MessageBox.Show($"File will be saved to: {selectedFilePath}");

                try
                {
                    using (StreamWriter writer = new StreamWriter(selectedFilePath))
                    {
                        writer.Write(jsonData);
                    }

                    MessageBox.Show($"File saved successfully to: {selectedFilePath}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error");
                }
            }
        }
    }
}
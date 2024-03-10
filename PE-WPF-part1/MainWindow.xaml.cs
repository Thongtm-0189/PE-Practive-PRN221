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

namespace PE_WPF_part1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> users = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string deri = DirectorNameTB.Text;
                DateOnly dob = DateOnly.Parse(DobDP.Text);
                string des = DesTB.Text;
                bool IsMale = isMaleCB.IsChecked.Value;
                string nation = NationTB.Text;
                User user = new User()
                {
                    Name = deri,
                    Des = des,
                    dob = dob,
                    Nation = nation,
                    isMale = IsMale
                };
                users.Add(user);
                lvData.ItemsSource = null;
                lvData.ItemsSource = users;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error input");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
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
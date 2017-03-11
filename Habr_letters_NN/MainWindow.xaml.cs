using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Image = System.Drawing.Image;

namespace Habr_letters_NN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string basePath = @"C:\Users\ALEX\Documents\visual studio 2017\Projects\Habr_letters_NN\Habr_letters_NN\\Resources";

        List<Neuron> _lettersNeurons = new List<Neuron>();
        public MainWindow()
        {
            InitializeComponent();
            Task.Run(() => InitNn());
        }

        private void InitNn()
        {
            for (var c = 'A'; c <= 'Z'; c++)
                StudyValue.Dispatcher.Invoke(()=> StudyValue.Items.Add(c));

            NeuroLogic.InitNn(basePath, _lettersNeurons);
        }

        private void Study()
        {
            NeuroLogic.Study(basePath, StudyValue.Text.ToUpper(), StudyPath.Text, _lettersNeurons);
        }

        private void GetResult()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Study();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                StudyPath.Text = openFileDialog.FileName;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                ReconitionFile.Text = openFileDialog.FileName;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GetResult();
        }
    }
}

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using Image = System.Drawing.Image;

namespace Habr_letters_NN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string basePath = @"C:\Users\ALEX\Documents\visual studio 2017\Projects\Habr_letters_NN\Habr_letters_NN";

        List<Neuron> _lettersNeurons = new List<Neuron>();
        public MainWindow()
        {
            InitializeComponent();
            InitNn();
        }

        private void InitNn()
        {
            for (var c = 'A'; c <= 'Z'; c++)
            {
                var neuron = new Neuron
                {
                    Name = c.ToString(),
                    Memory = new int[30, 30]
                };

                var image = new Bitmap(Image.FromFile( $"{basePath}\\Resources\\{c}.bmp"));

                for (var i = 0; i < 29; i++)
                {
                    for (var j = 0; j < 29; j++)
                    {
                        int middle = (image.GetPixel(i, j).R + image.GetPixel(i, j).G + image.GetPixel(i, j).B) / 3;
                        neuron.Memory[i, j] = middle;
                    }
                }

                _lettersNeurons.Add(neuron);
            }
        }

        private void Study()
        {
            var letter = StudyValue.Text.ToUpper();  
            var image = new Bitmap(Image.FromFile($"{basePath}\\Resources\\Study\\{StudyPath.Text}.bmp"));

            var letterNeuron = _lettersNeurons.Single(x => x.Name.ToUpper().Equals(letter));

            letterNeuron.Input = new int[30,30];

            for (int i = 0; i < 29; i++)
            {
                for (int j = 0; j < 29; j++)
                {
                    int middle = (image.GetPixel(i, j).R + image.GetPixel(i, j).G + image.GetPixel(i, j).B) / 3;
                    letterNeuron.Input[i, j] = middle;

                    var n = letterNeuron.Memory[i, j];
                    var m = letterNeuron.Input[i, j];

                    letterNeuron.Memory[i, j] = n + (n + m) / 2;
                }
            }

            image.Save($"{basePath}\\Resources\\{letter}.bmp");


        }

        private void GetResult()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Study();
        }
    }
}

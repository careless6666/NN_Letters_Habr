using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int[,] input = new int[3, 5];
        Web _nw1;

        /// <summary>
        /// Open click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            button2.Enabled = true;
            openFileDialog1.Title = "Укажите тестируемый файл";
            openFileDialog1.ShowDialog();
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            var im = pictureBox1.Image as Bitmap;
            for (var i = 0; i <= 5; i++) listBox1.Items.Add(" ");

            for (var x = 0; x <= 2; x++)
            {
                for (var y = 0; y <= 4; y++)
                {
                    int n = (im.GetPixel(x, y).R);
                    n = n >= 250 ? 0 : 1;
                    listBox1.Items[y] += n.ToString();
                    input[x, y] = n;
                }
            }

            Recognize();
        }

        public void Recognize()
        {
            _nw1.MulW();
            _nw1.Sum();
            listBox1.Items.Add(_nw1.Rez() 
                ? $" - True, Sum = {_nw1.sum}" 
                : $" - False, Sum = {_nw1.sum}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _nw1 = new Web(3, 5, input);

            openFileDialog1.Title = "Укажите файл весов";
            openFileDialog1.ShowDialog();
            var s = openFileDialog1.FileName;
            var sr = File.ReadAllText(s);

            var model = JsonConvert.DeserializeObject<DigitModel>(sr);

            _nw1.Weight = model.Weights;
            var transponMatrix = new int[5, 3];

            for (var i = 0; i < 5; i++)
                for (var j = 0; j < 3; j++)
                    transponMatrix[i, j] = _nw1.Weight[j, i];
            

            for (var i = 0; i < 3; i++)
            {
                listBox1.Items.Add("");
                for (var j = 0; j < 5; j++)
                    listBox1.Items[i] += " " + transponMatrix[i, j];
            }
            
        }

        /// <summary>
        /// Not right click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            if (_nw1.Rez() == false)
                _nw1.IncW(input);
            else
                _nw1.DecW(input);

            //Запись
            //var s1 = new string[5];
            // File.Delete("w.txt");

            File.Delete("w.json");
            var model = new DigitModel
            {
                Digit = 5,
                Weights = _nw1.Weight
            };

            File.Create("w.json");
            File.WriteAllText("w.json", JsonConvert.SerializeObject(model));

            //var fs = new FileStream("w.txt", FileMode.OpenOrCreate);
            //var sw = new StreamWriter(fs);

            //for (var y = 0; y <= 4; y++)
            //{
            //    var s = $"{_nw1.Weight[0, y]} {_nw1.Weight[1, y]} {_nw1.Weight[2, y]}";
            //    s1[y] = s;
            //    sw.WriteLine(s);

            //}
            //sw.Close();
        }
    }

}

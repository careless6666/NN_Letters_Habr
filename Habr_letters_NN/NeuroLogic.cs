using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Habr_letters_NN
{
    public class NeuroLogic
    {
        public static void InitNn(string basePath, List<Neuron> lettersNeurons)
        {
            for (var c = 'A'; c <= 'Z'; c++)
            {
                var neuron = new Neuron
                {
                    Name = c.ToString(),
                    Memory = new int[30, 30]
                };

                var image = new Bitmap(Image.FromFile($"{basePath}\\{c}.bmp"));

                for (var i = 0; i < 29; i++)
                {
                    for (var j = 0; j < 29; j++)
                    {
                        int middle = (image.GetPixel(i, j).R + image.GetPixel(i, j).G + image.GetPixel(i, j).B) / 3;
                        neuron.Memory[i, j] = middle;
                    }
                }

                lettersNeurons.Add(neuron);
            }
        }

        public static void Study(string basePath, string studyValue, string studyPath, List<Neuron> lettersNeurons)
        {
            var letter = studyValue;
            var image = new Bitmap(Image.FromFile(studyPath));

            var letterNeuron = lettersNeurons.Single(x => x.Name.ToUpper().Equals(letter));

            letterNeuron.Input = new int[30, 30];

            for (var i = 0; i < 29; i++)
            {
                for (var j = 0; j < 29; j++)
                {
                    int middle = (image.GetPixel(i, j).R + image.GetPixel(i, j).G + image.GetPixel(i, j).B) / 3;
                    letterNeuron.Input[i, j] = middle;

                    var n = letterNeuron.Memory[i, j];
                    var m = letterNeuron.Input[i, j];

                    letterNeuron.Memory[i, j] = n + (n + m) / 2;
                }
            }

            image.Save($"{basePath}\\{letter}.bmp");
        }

        public static string GetResult(string inputFilePath, List<Neuron> lettersNeurons)
        {
            var image = new Bitmap(Image.FromFile(inputFilePath));

            var input = new int[30, 30];

            lettersNeurons.ForEach(x=> x.Weight = 0);

            foreach (var lettersNeuron in lettersNeurons)
            {
                for (var i = 0; i < 29; i++)
                {
                    for (var j = 0; j < 29; j++)
                    {
                        int middle = (image.GetPixel(i, j).R + image.GetPixel(i, j).G + image.GetPixel(i, j).B) / 3;
                        input[i, j] = middle;

                        var m = input[i, j];
                        var n = lettersNeuron.Memory[i, j];

                        if (Math.Abs(m - n) < 120)
                        {
                            if (m < 250)
                                lettersNeuron.Weight++;
                            
                            if(m != 0)
                                if (m < 250)
                                {
                                   // n = (n + (n + m) / 2);
                                    //lettersNeuron.Memory[i, j] = n;
                                }
                                else if(n != 0)
                                {
                                    if (m < 250)
                                    {
                                        //n = (n + (n + m) / 2);
                                        //lettersNeuron.Memory[i, j] = n;
                                    }
                                }
                        }
                    }
                }
            }

            var letter = lettersNeurons.FirstOrDefault(z=> z.Weight == lettersNeurons.Max(x => x.Weight)) ;
            return letter?.Name;
        }
    }
}

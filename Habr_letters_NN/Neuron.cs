namespace Habr_letters_NN
{
    public class Neuron
    {
        /// <summary>
        /// Тут название нейрона – буква, с которой он ассоциируется
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тут входной массив 30х30
        /// </summary>
        public int[,] Input { get; set; }
        /// <summary>
        /// Сюда он будет говорить, что решил
        /// </summary>
        public int Output { get; set; }
        /// <summary>
        /// Тут он будет хранить опыт о предыдущем опыте
        /// </summary>
        public int[,] Memory { get; set; }
        /// <summary>
        /// Neuron weight
        /// </summary>
        public int Weight { get; set; }
    }
}

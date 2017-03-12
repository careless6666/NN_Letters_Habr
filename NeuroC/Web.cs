namespace WindowsFormsApplication1
{
    public class Web
    {
        /// <summary>
        /// Тут будем хранить отмасштабированные сигналы
        /// </summary>
        private int[,] Mul;
        /// <summary>
        /// Массив для хранения весов
        /// </summary>
        public int[,] Weight;
        /// <summary>
        /// Входная информация
        /// </summary>
        private int[,] Input;
        /// <summary>
        /// Порог - выбран экспериментально, для быстрого обучения
        /// </summary>
        private int Limit = 9;
        /// <summary>
        /// Тут сохраним сумму масштабированных сигналов
        /// </summary>
        public int sum;

        public Web(int sizex, int sizey, int[,] inP)
        {
            Weight = new int[sizex, sizey];  // Определяемся с размером массива (число входов)
            Mul = new int[sizex, sizey];

            Input = new int[sizex, sizey];
            Input = inP;  // Получаем входные данные
        }

        public void MulW()
        {
            for (var x = 0; x <= 2; x++)
                for (var y = 0; y <= 4; y++)  // Пробегаем по каждому аксону
                    Mul[x, y] = Input[x, y] * Weight[x, y];  // Умножаем его сигнал (0 или 1) на его собственный вес и сохраняем в массив.
        }

        public void Sum()
        {
            sum = 0;
            for (var x = 0; x <= 2; x++)
                for (var y = 0; y <= 4; y++)
                    sum += Mul[x, y];
        }

        public bool Rez()
        {
            return sum >= Limit;
        }

        /// <summary>
        /// Если система не считает равными значения которые должны быть равны 5!=5, 
        /// увеличиваем веса на 1
        /// </summary>
        /// <param name="inP"></param>
        public void IncW(int[,] inP)
        {
            for (var x = 0; x <= 2; x++)
                for (var y = 0; y <= 4; y++)
                    Weight[x, y] += inP[x, y];
        }

        /// <summary>
        /// Если система считает равными значения которые не должны быть равны 6 == 5
        /// уменьшаем веса на 1
        /// </summary>
        /// <param name="inP"></param>
        public void DecW(int[,] inP)
        {
            for (var x = 0; x <= 2; x++)
                for (var y = 0; y <= 4; y++)
                    Weight[x, y] -= inP[x, y];
        }
    }
}

namespace WindowsFormsApplication1
{
    public class DigitModel
    {
        /// <summary>
        /// Digit value
        /// </summary>
        public int Digit { get; set; }
        public int Treshhold { get; set; }
        /// <summary>
        /// Digit weights array
        /// </summary>
        public int [,] Weights { get; set; }
    }
}

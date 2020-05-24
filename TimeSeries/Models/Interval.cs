namespace TimeSeries.Models
{
    public class Interval
    {
        public decimal Left { get; set; }
        public decimal Right { get; set; }
        public bool LeftIncluded { get; set; }
        public bool RightIncluded { get; set; }
        public int Index { get; set; }

        public bool IsValueInside(decimal value)
        {
            bool greaterThanLeft = LeftIncluded ?
                Left <= value :
                Left < value;

            bool lessThanRight = RightIncluded ?
                value <= Right :
                value < Right;

            return greaterThanLeft && lessThanRight;
        }
    }
}

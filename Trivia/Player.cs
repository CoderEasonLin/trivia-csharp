namespace Trivia
{
    public class Player
    {
        private bool _isPenaltized;
        private int _purse;
        public string Name { get; set; }

        public bool IsPenaltized
        {
            get { return _isPenaltized; }
        }

        public void SetPenaltized()
        {
            _isPenaltized = true;
        }

        public int Place { get; set; }

        public int Purse
        {
            get { return _purse; }
        }

        public void WonCoin()
        {
            _purse++;
        }

        public void Move(int roll)
        {
            Place += roll;

            if (Place > 11)
                Place -= 12;
        }
    }
}
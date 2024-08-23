namespace FlipView
{
    public class FlippedChangedEventArgs : EventArgs
    {
        public bool Flipped { get; }
        public FlippedChangedEventArgs(bool flipped)
        {
            Flipped = flipped;
        }
    }
}





using System;


namespace BubbleApi
{
    public class BubbleApiException : Exception 
    {
        public BubbleApiException(string text) : base(text) {}
    }
}

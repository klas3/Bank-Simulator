using System;
using Bank.Cards;

namespace Bank.EventArguments
{
    class RefillCardEventArgs : SumChangedEventArgs
    {
        public Card Card { get; set; }

        public RefillCardEventArgs(Card card, float sum) : base(sum)
        {
            Card = card;
        }
    }
}

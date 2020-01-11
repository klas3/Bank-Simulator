using System;
using Bank.Cards;

namespace Bank.EventArguments
{
    class TransferFromCardToCardEventArgs : SumChangedEventArgs
    {
        public Card CardFrom { get; set; }
        public Card CardTo { get; set; }

        public TransferFromCardToCardEventArgs(Card cardFrom, Card cardTo, float sum) : base(sum)
        {
            CardFrom = cardFrom;
            CardTo = cardTo;
        }
    }
}

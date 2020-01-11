using System.Collections.Generic;
using Bank.Cards;

namespace Bank.Collections
{
    sealed class CardsCollection
    {
        private Dictionary<int, Card> cards;

        public CardsCollection()
        {
            this.cards = new Dictionary<int, Card>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card.CardId, card);
        }

        public Card GetCardByNumber(string number)
        {
            foreach(KeyValuePair<int, Card> card in cards)
            {
                if (card.Value.Number == number) return card.Value;
            }

            return null;
        }
    }
}

using System.Collections.Generic;
using Bank.Cards;
using Bank.Exceptions;

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
            if(!cards.ContainsKey(card.CardId))
            {
                cards.Add(card.CardId, card);
            }
            else
            {
                throw new ExistingItemException();
            }
        }

        public void RemoveCard(int cardId)
        {
            if(cards.ContainsKey(cardId))
            {
                cards.Remove(cardId);
            }
            else
            {
                throw new UnexistingItemException();
            }
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

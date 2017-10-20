using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class SoftHitStrategy : IHitStrategy
    {
        private const int g_hitLimit = 17;

        public bool DoHit(model.Player a_dealer)
        {
            if (a_dealer.CalcScore() > g_hitLimit)
            {
                int score = a_dealer.CalcScore();

                foreach (var card in a_dealer.GetHand())
                {
                    if (card.GetValue() == Card.Value.Ace)
                    {
                        score = score - 13;

                        if (score < g_hitLimit)
                        {
                            break;
                        }
                    }
                }
            }

            return a_dealer.CalcScore() < g_hitLimit;
        }
    }
}
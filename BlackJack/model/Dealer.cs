﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Dealer : Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;

        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_hitRule;
        private rules.IWinnerStrategy m_winnerRule;
        private List<IExcitingObserver> m_listeners;

        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetHitRule();
            m_winnerRule = a_rulesFactory.GetWinnerRule();
            m_listeners = new List<IExcitingObserver>();
        }

        public void DrawAndDealCard(Player a_dealerOrPlayer, bool ShowOrHideCard, Player a_player)
        {
            Card c = m_deck.GetCard();
            c.Show(ShowOrHideCard);
            a_dealerOrPlayer.DealCard(c);

            foreach(IExcitingObserver listener in m_listeners)
            {
                listener.RedrawAndShowHand(this, a_player);
            }
        }

        public void AddListener(IExcitingObserver listener)
        {
            m_listeners.Add(listener);
        }

        public bool Stand()
        {
            if (m_deck != null) 
            {
                ShowHand();
                while (m_hitRule.DoHit(this))
                {
                    Card c = m_deck.GetCard();
                    c.Show(true);
                    DealCard(c);
                }

                return true;
            }

            return false;
        }

        public bool NewGame(Player a_player)
        {
            if (m_deck == null || IsGameOver())
            {
                m_deck = new Deck();
                ClearHand();
                a_player.ClearHand();
                return m_newGameRule.NewGame(this, a_player);   
            }
            return false;
        }

        public bool Hit(Player a_player)
        {
            if (m_deck != null && a_player.CalcScore() < g_maxScore && !IsGameOver())
            {
                DrawAndDealCard(a_player, true, a_player);
                
                // Card c;
                // c = m_deck.GetCard();
                // c.Show(true);
                // a_player.DealCard(c);

                return true;
            }
            return false;
        }

        public bool IsDealerWinner(Player a_player)
        {
            return m_winnerRule.WinnerStrategy(a_player.CalcScore(), CalcScore(), g_maxScore);
        }

        public bool IsGameOver()
        {
            if (m_deck != null && /*CalcScore() >= g_hitLimit*/ m_hitRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame
    {
        public bool Play(model.Game a_game, view.IView a_view)
        {
            a_view.DisplayWelcomeMessage();
            
            a_view.DisplayDealerHand(a_game.GetDealerHand(), a_game.GetDealerScore());
            a_view.DisplayPlayerHand(a_game.GetPlayerHand(), a_game.GetPlayerScore());

            if (a_game.IsGameOver())
            {
                a_view.DisplayGameOver(a_game.IsDealerWinner());
            }

            int input = a_view.GetInput();

            if (a_view.PlayerWantsToPlay(input))
            {
                a_game.NewGame();
            }
            else if (a_view.PlayerWantsToHit(input))
            {
                a_game.Hit();
            }
            else if (a_view.PlayerWantsToStand(input))
            {
                a_game.Stand();
            }

            return a_view.PlayerWantsToQuit(input);
        }
    }
}

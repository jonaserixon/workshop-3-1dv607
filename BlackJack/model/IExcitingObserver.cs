using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    interface IExcitingObserver
    {
        void RedrawAndShowHand(model.Dealer a_dealer, model.Player a_player);
    }
}

using LBoL.Presentation.UI.Panels;
using LBoL.Presentation.UI;
using System;
using System.Collections.Generic;
using System.Text;
using LBoL.Base;

namespace test.Cards
{
    static class Helpers
    {

        public static void FakeQueueConsumingMana()
        {
            // should not be changed
            var cost = new ManaGroup() { Any =  0 };
            var manaPanel = UiManager.GetPanel<BattleManaPanel>();
            manaPanel._consumingDeque.Insert(0, new BattleManaPanel.ConsumingManaWidgets(new ConsumingMana(cost, cost), manaPanel._unpooledCollection.Prepay(), manaPanel._pooledCollection.Prepay()));
        }
    }
}

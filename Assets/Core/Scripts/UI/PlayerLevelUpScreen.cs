using SpecialHedgehog.Framework;
using SpecialHedgehog.Pause;

namespace SpecialHedgehog.UI
{
    public class PlayerLevelUpScreen : UIScreen
    {
        public void SendClickEvent()
        {
            var eventWorld = EcsStartup.Instance.GetWorld(Constants.Worlds.Events);
            eventWorld.GetPool<DisablePauseRequest>().Add(eventWorld.NewEntity());
        }
    }
}
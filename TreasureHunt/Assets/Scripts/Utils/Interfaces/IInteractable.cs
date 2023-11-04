using TreasureHunt.Player;

namespace TreasureHunt.Interactions

{
    public interface IInteractable
    {
        public void Interact(PlayerController player);

        public void UIFeedback(PlayerController player);
    }
}

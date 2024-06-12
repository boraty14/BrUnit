namespace BrUnit
{
    public class Consumable<T>
    {
        private bool _isConsumableSet;
        private T _consumable;
        
        public void SetConsumable(T consumable)
        {
            _isConsumableSet = true;
            _consumable = consumable;
        }
        
        public bool TryConsume(out T consumable)
        {
            if (!_isConsumableSet)
            {
                consumable = default;
                return false;
            }

            _isConsumableSet = false;
            consumable = _consumable;
            return true;
        }
    }
}
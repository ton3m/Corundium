using System;

namespace CodeBase.ItemsSystem
{
    public class ValueItem : Item
    {
        private float _value;
        private readonly float _maxValue;

        protected ValueItem(string id, float value) : base(id)
        {
            _maxValue = value;
            _value = _maxValue;
        }

        protected float Value
        {
            get => _value;
            set => _value = Math.Clamp(value, 0, _maxValue);
        }
    }
}
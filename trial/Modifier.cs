using System.Collections.Generic;

namespace trial
{
    public interface Modifier
    {
        string Name { get; }
        string Description { get; } // Description of the modifier
        byte EffectedAttribute { get; } // Value of the modifier, e.g., damage increase, health regeneration rate, etc.
        string Value { get; } // The value of the modifier, e.g., +10% damage, +5 health per second, etc.
        double Duration { get; set; } // Duration of the modifier in seconds, 0 for permanent modifiers
        bool IsActive { get; set; } // Indicates if the modifier is currently active


        // VVV Untested methods VVV

        Entity Apply(Entity entity) // Method to apply the modifier to an entity
        {
            Dictionary<byte, object> Properties = new Dictionary<byte, object>()
            {
                { 0, entity.Health },
                { 1, entity.Damage },
                { 2, entity.Speed }
            };
            Properties[EffectedAttribute] = ConvertValue(entity, (double)Properties[EffectedAttribute], false);
            return entity;
        }
        private double ConvertValue(Entity entity, double effectedStat, bool invert)
        {
            double change = 0;

            if (Value[0] == '+')
            {
                change = double.Parse(Value.Substring(1));
            }
            else if (Value[0] == '-')
            {
                change = 0 - double.Parse(Value.Substring(1));
            }
            else if (Value[0] == '*')
            {
                change = (effectedStat * double.Parse(Value.Substring(1))) - effectedStat;
            }
            else if (Value[0] == '/')
            {
                change = (effectedStat * double.Parse(Value.Substring(1))) - effectedStat;
            }

            if (invert) { change = 0 - change; }
            return effectedStat + change;
        }

        Entity Remove(Entity entity) // Method to remove the modifier from an entity
        {
            Dictionary<byte, object> Properties = new Dictionary<byte, object>()
            {
                { 0, entity.Health },
                { 1, entity.Damage },
                { 2, entity.Speed }
            };
            Properties[EffectedAttribute] = ConvertValue(entity, (double)Properties[EffectedAttribute], true);
            return entity;
        }
        void Update(double elapsedTime) // Method to update the modifier's state, e.g., decrease duration
        {
            if (IsActive)
            {
                if (Duration > 0)
                {
                    Duration -= elapsedTime;
                    if (Duration <= 0)
                    {
                        IsActive = false; // Deactivate the modifier when duration ends
                    }
                }
            }
        }
    }
}

using DefaultNamespace.Enums;

namespace Interfaces
{
    public interface IPowerUp
    {
        PowerUpType type {get; set;}
        void UsePowerUp();
    }
}
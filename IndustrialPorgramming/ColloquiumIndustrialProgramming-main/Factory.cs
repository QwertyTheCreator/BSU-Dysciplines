using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    abstract public class Weapon
    {
        public abstract string Hit();
    }

    abstract public class Movement
    {
        public abstract string Move();
    }

    public class Arbalet : Weapon
    {
        public override string Hit()
        {
            return "Стреляем из арбалета";
        }
    }

    public class Sword : Weapon
    {
        public override string Hit()
        {
            return "Бьем мечом";
        }
    }

    public class FlyMovement : Movement
    {
        public override string Move()
        {
            return "Летим";
        }
    }

    public class RunMovement : Movement
    {
        public override string Move()
        {
            return "Бежим";
        }
    }

    abstract public class HeroFactory
    {
        public abstract Movement CreateMovement();
        public abstract Weapon CreateWeapon();
    }

    public class ElfFactory : HeroFactory
    {
        public override Movement CreateMovement()
        {
            return new FlyMovement();
        }

        public override Weapon CreateWeapon()
        {
            return new Arbalet();
        }
    }

    public class VoinFactory : HeroFactory
    {
        public override Movement CreateMovement()
        {
            return new RunMovement();
        }

        public override Weapon CreateWeapon()
        {
            return new Sword();
        }
    }

    public class Hero
    {
        private Weapon weapon;
        private Movement movement;
        public Hero(HeroFactory factory)
        {
            weapon = factory.CreateWeapon();
            movement = factory.CreateMovement();
        }
        public string Run()
        {
            return movement.Move();
        }
        public string Hit()
        {
            return weapon.Hit();
        }
    }
}

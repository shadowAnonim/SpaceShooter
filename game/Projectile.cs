using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace game
{
    public class Projectile
    {
        public Grid grid;
        private int gridRadius = 10;
        private int speed = 10;
        public void Move()
        {
            grid.Margin = new Thickness(grid.Margin.Left, grid.Margin.Top - speed, 0, 0);
            if(!GameManager.CheckObjectOnScreen(grid, gridRadius) && grid.Margin.Top < 0)
            {
                GameManager.destroyProjectile(this);
            }
        }
    }
}

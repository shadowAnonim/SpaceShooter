using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace game
{
    public class Enemy
    {
        public Grid grid;
        public int gridRadius = 100;
        public int speed = 10;
        public static int spawnDelay = 2;

        public void Move()
        {
            grid.Margin = new Thickness(grid.Margin.Left, grid.Margin.Top + speed, 0, 0);
            if (!GameManager.CheckObjectOnScreen(grid, gridRadius) 
                && grid.Margin.Top  + gridRadius> GameManager.windowHeight)
            {
                //grid.Visibility = Visibility.Collapsed;
                GameManager.destroyEnemy(this);
            }
            CheckCollision();
        }

        public void CheckCollision()
        {
            Hero hero = GameManager.hero;
            if (grid.Margin.Left + gridRadius > hero.grid.Margin.Left &&
                    grid.Margin.Left < hero.grid.Margin.Left + gridRadius &&
                    grid.Margin.Top + gridRadius> hero.grid.Margin.Top &&
                    grid.Margin.Top < hero.grid.Margin.Top + gridRadius)
            {
                GameManager.gameOver = true;
                GameManager.destroyEnemy(this);
            }
            foreach(Projectile proj in GameManager.projectiles)
            {
                if (proj.grid.Margin.Left > grid.Margin.Left && 
                    proj.grid.Margin.Left < grid.Margin.Left + gridRadius && 
                    proj.grid.Margin.Top > grid.Margin.Top && 
                    proj.grid.Margin.Top < grid.Margin.Top + gridRadius)
                {
                    GameManager.points++;
                    GameManager.destroyProjectile(proj);
                    GameManager.destroyEnemy(this);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace game
{
    /// <summary>
    /// Класс, для работы с врагами.
    /// </summary>
    public class Enemy
    {
        public Grid grid;
        public int gridRadius = 100;
        public int speed = 10;
        public static int spawnDelay = 2;

        /// <summary>
        /// Движение врага
        /// </summary>
        public void Move()
        {
            //Изменение кооринат врага.
            grid.Margin = new Thickness(grid.Margin.Left, grid.Margin.Top + speed, 0, 0);
            // Если враг вышнл за пределы экрана - уничтожается
            if (!GameManager.CheckObjectOnScreen(grid, gridRadius) 
                && grid.Margin.Top  + gridRadius> GameManager.windowHeight)
            {
                //grid.Visibility = Visibility.Collapsed;
                GameManager.destroyEnemy(this);
            }
            //Проверка на столкновения
            CheckCollision();
        }

        /// <summary>
        /// Cтолкновения врага с героем и снарядами.
        /// </summary>
        public void CheckCollision()
        {
            //Проверка на столкновение с героем
            Hero hero = GameManager.hero;
            if (grid.Margin.Left + gridRadius > hero.grid.Margin.Left &&
                    grid.Margin.Left < hero.grid.Margin.Left + gridRadius &&
                    grid.Margin.Top + gridRadius> hero.grid.Margin.Top &&
                    grid.Margin.Top < hero.grid.Margin.Top + gridRadius)
            {
                GameManager.gameOver = true;
                GameManager.destroyEnemy(this);
            }
            //Проверка на столкновение со снарядами
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

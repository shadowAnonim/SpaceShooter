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
    /// Класс для работы со снарядами
    /// </summary>
    public class Projectile
    {
        public Grid grid;
        private int gridRadius = 10;
        private int speed = 10;
        /// <summary>
        /// Движение
        /// </summary>
        public void Move()
        {
            // Изменение координат
            grid.Margin = new Thickness(grid.Margin.Left, grid.Margin.Top - speed, 0, 0);
            // Проверка на выход за пределы экрана
            if(!GameManager.CheckObjectOnScreen(grid, gridRadius) && grid.Margin.Top < 0)
            {
                // Уничтожение снаряда
                GameManager.destroyProjectile(this);
            }
        }
    }
}

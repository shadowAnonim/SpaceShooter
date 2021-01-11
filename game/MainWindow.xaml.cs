using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer mainTimer;
        // Таймер появления врагов
        private DispatcherTimer enemyTimer;
        // Таймер стрельбы
        public DispatcherTimer shootTimer;
        private Hero hero;
        public MainWindow()
        {
            InitializeComponent();
            
            // Создание главного таймера
            mainTimer = new DispatcherTimer();
            mainTimer.Interval = new TimeSpan(0, 0, 0, 0, 25);
            mainTimer.Tick += new EventHandler(mainTimer_Tick);
            mainTimer.Start();

            // Создание таймера врагов
            enemyTimer = new DispatcherTimer();
            enemyTimer.Interval = new TimeSpan(0, 0, Enemy.spawnDelay);
            enemyTimer.Tick += new EventHandler(enemyTimer_Tick);
            enemyTimer.Start();

            // Создание таймера стрельбы
            shootTimer = new DispatcherTimer();
            shootTimer.Interval = new TimeSpan(0, 0, 0, 0, Hero.shootDelay);
            shootTimer.Tick += new EventHandler(shootTimer_Tick);

            // Настройка корабля героя
            hero = new Hero();
            hero.grid = heroGrid;
            hero.shootTimer = shootTimer;

            // Настройка GameManager-а
            GameManager.hero = hero;
            GameManager.enemies = new List<Enemy>();
            GameManager.destroyedEnemies = new List<Enemy>();
            GameManager.projectiles = new List<Projectile>();
            GameManager.destroyedProjectiles = new List<Projectile>();
            GameManager.mainWindow = mainWidow;
            GameManager.windowHeight = mainWidow.Height;
            GameManager.windowWidth = mainWidow.Width;
            GameManager.mainGrid = mainGrid;
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            GameManager.Update();
        }

        private void enemyTimer_Tick(object sender, EventArgs e)
        {
            GameManager.SpawnEnemy();
        }

        private void shootTimer_Tick(object sender, EventArgs e)
        {
            shootTimer.Stop();
        }

        private void mainWidow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Изменение параметров окна в GameManager-е
            GameManager.windowHeight = mainWidow.ActualHeight;
            GameManager.windowWidth = mainWidow.ActualWidth;
        }
        /// <summary>
        /// Завершение игры
        /// </summary>
        public void EndGame()
        {
            // Уведомлние о результате
            MessageBoxResult result = MessageBox.Show("Врагов сбито: " + GameManager.points + "\nСыграть ещё?", "Игра окончена" ,MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // Перезапуск всех таймеров
                mainTimer.Stop();
                enemyTimer.Stop();
                shootTimer.Stop();
                mainTimer = new DispatcherTimer();
                mainTimer.Interval = new TimeSpan(0, 0, 0, 0, 30);
                mainTimer.Tick += new EventHandler(mainTimer_Tick);
                mainTimer.Start();
                enemyTimer = new DispatcherTimer();
                enemyTimer.Interval = new TimeSpan(0, 0, Enemy.spawnDelay);
                enemyTimer.Tick += new EventHandler(enemyTimer_Tick);
                enemyTimer.Start();
                shootTimer = new DispatcherTimer();
                shootTimer.Interval = new TimeSpan(0, 0, 0, 0, Hero.shootDelay);
                shootTimer.Tick += new EventHandler(shootTimer_Tick);
                GameManager.ResetGame();
            }
            else this.Close();
        }
    }
}

using SlyTheRaccoon.Models;
using SlyTheRaccoon.Models.Services;
using SlyTheRaccoon.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SlyTheRaccoon.ViewModels
{
    public class GameViewModel : ObservableObject
    {
        private readonly GameModel _model = new GameModel();
        private readonly EnemyMovementService _enemyMovementService;
        private readonly PlayerMovementService _playerMovementService;
        private ObservableCollection<CellViewModel> _cells = new ObservableCollection<CellViewModel>();

        public ObservableCollection<CellViewModel> Cells => _cells;
        public int GridRows => _model.CurrentLevel?.Height ?? 0;
        public int GridColumns => _model.CurrentLevel?.Width ?? 0;
        public ICommand MoveCommand { get; }

        private int _currentLevelIndex = 0;
        private readonly List<Level> _levelPrototypes = new List<Level>();

        public GameViewModel()
        {
            _levelPrototypes.Add(GameLevels.Level1);
            _levelPrototypes.Add(GameLevels.Level2);
            _levelPrototypes.Add(GameLevels.Level3);
            _levelPrototypes.Add(GameLevels.Level4);
            _levelPrototypes.Add(GameLevels.Level5);
            _levelPrototypes.Add(GameLevels.Level6);
            _levelPrototypes.Add(GameLevels.Level7);
            _levelPrototypes.Add(GameLevels.Level8);

            _enemyMovementService = new EnemyMovementService(_model);
            _playerMovementService = new PlayerMovementService(_model);

            MoveCommand = new RelayCommand<Key>(HandleMove);
            LoadLevel(0);
        }

        private void LoadLevel(int levelIndex)
        {
            _model.HasFood = false;

            if (levelIndex < 0 || levelIndex >= _levelPrototypes.Count)
            {
                HandleGameComplete();
                return;
            }

            _model.LoadLevel(GetLevel(levelIndex));
            UpdateAllCells();
            OnPropertyChanged(nameof(GridRows));
            OnPropertyChanged(nameof(GridColumns));
        }

        private Level GetLevel(int levelIndex)
        {
            switch (levelIndex)
            {
                case 0:
                { 
                    return GameLevels.Level1;
                }
                case 1:
                {
                    MessageBox.Show("Маленькие собаки ходят по одной клетке и только по прямой. Они быстро потеряют интерес, если потеряют тебя из виду. \nДойди до люка, избегая их. И про курочку не забудь!");
                    return GameLevels.Level2;
                }                 
                case 2:
                {
                    MessageBox.Show("Большие собаки быстрее тебя - они ходят по две клетки за ход! Но они всё так же отступят, если ты уйдёшь с их прямой видимости.");
                    return GameLevels.Level3;
                }
                case 3:
                {
                    return GameLevels.Level4;
                }
                case 4:
                    {
                        MessageBox.Show("Ищейки не такие быстрые, но как только завидят тебя, они не перестанут преследование!");

                        return GameLevels.Level5;
                    } 
                case 5: return GameLevels.Level6;
                case 6: return GameLevels.Level7;
                case 7: return GameLevels.Level8;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateAllCells()
        {
            _cells.Clear();
            for (int y = 0; y < _model.CurrentLevel.Height; y++)
            {
                for (int x = 0; x < _model.CurrentLevel.Width; x++)
                {
                    _cells.Add(new CellViewModel(_model.CurrentLevel.Grid[x, y], x, y));
                }
            }
        }

        private async void HandleMove(Key key)
        {
            if (!_playerMovementService.TryMovePlayer(key, out int newX, out int newY))
                return;


            if (_playerMovementService.CanCollectFood(newX, newY))
            {
                _model.HasFood = true;
            }


            if (_playerMovementService.CanExit(newX, newY))
            {
                await UpdateAndDelayAsync();

                int nextLevelIndex = _currentLevelIndex + 1;
                _model.HasFood = false;

                if (nextLevelIndex >= _levelPrototypes.Count)
                {
                    HandleGameComplete();
                }
                else
                {
                    _currentLevelIndex = nextLevelIndex;
                    LoadLevel(_currentLevelIndex);
                }
                return;
            }

            _playerMovementService.MovePlayer(newX, newY);
            _enemyMovementService.MoveEnemies();
            if (CheckEnemyCollision(newX, newY))
            {
                await UpdateAndDelayAsync();
                MessageBox.Show("Вы попались! Начать уровень заново?");
                LoadLevel(_currentLevelIndex);
                return;
            }
            UpdateAllCells();
        }

        private async Task UpdateAndDelayAsync()
        {
            UpdateAllCells();
            await Task.Delay(1);
            await Task.Yield();
        }

        private bool CheckEnemyCollision(int x, int y)
        {
            foreach (var enemy in _model.Enemies)
            {
                var distance = MovementService.ManhattanDistance(x, y, enemy.X, enemy.Y);
                if ((enemy.IsFast && distance <= 1) || (!enemy.IsFast && distance <= 1))
                    return true;
            }
            return false;
        }

        private void HandleGameComplete()
        {
            MessageBox.Show("Поздравляем! Вы прошли все уровни!");
        }
    }
}
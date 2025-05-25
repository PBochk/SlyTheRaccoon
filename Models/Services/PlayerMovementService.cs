using SlyTheRaccoon.Models;
using System;
using System.Windows.Input;

namespace SlyTheRaccoon.Models.Services
{
    public class PlayerMovementService
    {
        private readonly GameModel _model;

        public PlayerMovementService(GameModel model)
        {
            _model = model;
        }

        public bool TryMovePlayer(Key key, out int newX, out int newY)
        {
            var (dx, dy) = MovementService.GetDirection(key);
            newX = _model.PlayerX + dx;
            newY = _model.PlayerY + dy;

            return (MovementService.CanMoveTo(_model.CurrentLevel, newX, newY))
                && !(_model.CurrentLevel.Grid[newX, newY] == CellType.Exit && !_model.HasFood);
        }

        public void MovePlayer(int newX, int newY)
        {
            _model.CurrentLevel.Grid[_model.PlayerX, _model.PlayerY] = CellType.Empty;
            _model.CurrentLevel.Grid[newX, newY] = CellType.Player;
            _model.PlayerX = newX;
            _model.PlayerY = newY;
        }

        public bool CanCollectFood(int x, int y)
        {
            return _model.CurrentLevel.Grid[x, y] == CellType.Food;
        }

        public bool CanExit(int x, int y)
        {
            return _model.CurrentLevel.Grid[x, y] == CellType.Exit && _model.HasFood;
        }
    }
}
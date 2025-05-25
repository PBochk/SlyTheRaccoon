using SlyTheRaccoon.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace SlyTheRaccoon.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GameViewModel();

            this.LocationChanged += MainWindow_LocationChanged;

            Loaded += (sender, e) =>
            {
                Focus();
                Keyboard.Focus(this);
            };
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            var screen = SystemParameters.WorkArea;

            if (Left < screen.Left)
                Left = screen.Left;

            else if (Left + ActualWidth > screen.Right)
                Left = screen.Right - ActualWidth;

            if (Top < screen.Top)
                Top = screen.Top;

            else if (Top + ActualHeight > screen.Bottom)
                Top = screen.Bottom - ActualHeight;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (DataContext is GameViewModel vm)
            {
                vm.MoveCommand.Execute(e.Key);
                e.Handled = true;
            }
        }
    }
}
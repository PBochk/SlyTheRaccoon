using SlyTheRaccoon.ViewModels;
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

            // Убедимся, что окно получает фокус
            Loaded += (sender, e) =>
            {
                Focus();
                Keyboard.Focus(this);
            };
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (DataContext is GameViewModel vm)
            {
                vm.MoveCommand.Execute(e.Key);
                e.Handled = true; // Важно!
            }
        }
    }
}
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace TollCalculator.Console.Commands
{
    internal class MenuCommand : ICommand
    {
        public MenuCommand()
        {
            MenuItems = new List<ICommand>();
        }

        public List<ICommand> MenuItems { get; set; }

        public string Name { get; set; }

        public MenuBackType BackType { get; set; }

        public void Execute(Context context)
        {
            int menuOption;
            do
            {
                ShowMenu();
                menuOption = CommandHelper.GetIntegerInput() ?? -1;
                if (menuOption > 0 && menuOption <= MenuItems.Count())
                {
                    MenuItems[menuOption - 1].Execute(context);
                }
            } while (menuOption != 0);
        }

        private void ShowMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(Name);
            sb.AppendLine();
            for (int i = 0; i < MenuItems.Count(); i++)
            {
                sb.AppendLine($"{i + 1}. {MenuItems[i].Name}");
            }
            sb.AppendLine($"0. {BackType}");

            System.Console.Clear();
            System.Console.WriteLine(sb.ToString());
        }
    }
}

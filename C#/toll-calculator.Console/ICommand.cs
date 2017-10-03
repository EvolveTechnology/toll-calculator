namespace TollCalculator.Console
{
    internal interface ICommand
    {
        string Name { get; }
        void Execute(Context context);
    }
}

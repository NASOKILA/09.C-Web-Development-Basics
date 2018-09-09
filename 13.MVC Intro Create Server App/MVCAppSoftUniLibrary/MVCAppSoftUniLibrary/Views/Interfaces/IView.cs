namespace MVCAppSoftUniLibrary.Views.Interfaces
{
    //Tova mojehme da go na pravim generik abstrakten klas.
    public interface IView
    {
        string Display(object model); //object model = null TAKA SE SLAGA OPTIONAL PARAMETUR V INTERFEIS
    }
}

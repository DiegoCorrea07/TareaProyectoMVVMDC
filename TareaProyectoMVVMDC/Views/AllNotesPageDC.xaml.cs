namespace TareaproyectoMVVMDC.Views;

public partial class AllNotesPageDC : ContentPage
{
	public AllNotesPageDC()
	{
		InitializeComponent();
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}
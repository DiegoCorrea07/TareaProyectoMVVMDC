
namespace TareaProyectoMVVMDC;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(TareaproyectoMVVMDC.Views.NotePageDC), typeof(TareaproyectoMVVMDC.Views.NotePageDC));
    }
}


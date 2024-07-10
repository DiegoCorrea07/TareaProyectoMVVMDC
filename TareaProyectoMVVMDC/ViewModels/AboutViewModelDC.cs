using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TareaproyectoMVVMDC.ViewModels
{
    internal class AboutViewModelDC
    {
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string MoreInfoUrl => "https://aka.ms/maui";
        public string Message => "¡Hola! Soy un apasionado estudiante en Ingeniería de " +
            "Software que disfruta creando aplicaciones innovadoras y útiles. Con experiencia en " +
            "diversas tecnologías de desarrollo, estoy constantemente aprendiendo y " +
            "mejorando mis habilidades para ofrecer soluciones de alta calidad. Fuera " +
            "del mundo del desarrollo, me encanta explorar nuevas tecnologías, leer " +
            "libros y disfrutar de actividades al aire libre como jugar fútbol.";
        public ICommand ShowMoreInfoCommand { get; }

        public AboutViewModelDC()
        {
            ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
        }

        async Task ShowMoreInfo() =>
            await Launcher.Default.OpenAsync(MoreInfoUrl);
    }
}

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace TareaproyectoMVVMDC.ViewModels
{
    internal class NoteViewModelDC : ObservableObject, IQueryAttributable
    {
        private Models.NoteDC _noteDC;

        public string Text
        {
            get => _noteDC.Text;
            set
            {
                if (_noteDC.Text != value)
                {
                    _noteDC.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date => _noteDC.Date;

        public string Identifier => _noteDC.Filename;

        public ICommand SaveCommandDC { get; private set; }
        public ICommand DeleteCommandDC { get; private set; }

        public NoteViewModelDC()
        {
            _noteDC = new Models.NoteDC();
            SaveCommandDC = new AsyncRelayCommand(Save);
            DeleteCommandDC = new AsyncRelayCommand(Delete);
        }

        public NoteViewModelDC(Models.NoteDC noteDC)
        {
            _noteDC = noteDC;
            SaveCommandDC = new AsyncRelayCommand(Save);
            DeleteCommandDC = new AsyncRelayCommand(Delete);
        }

        private async Task Save()
        {
            _noteDC.Date = DateTime.Now;
            _noteDC.Save();
            await Shell.Current.GoToAsync($"..?saved={_noteDC.Filename}");
        }

        private async Task Delete()
        {
            _noteDC.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_noteDC.Filename}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _noteDC = Models.NoteDC.Load(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _noteDC = Models.NoteDC.Load(_noteDC.Filename);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
        }

    }
}

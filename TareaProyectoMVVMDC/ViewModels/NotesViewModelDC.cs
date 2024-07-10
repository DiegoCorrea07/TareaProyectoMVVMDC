using CommunityToolkit.Mvvm.Input;
using TareaproyectoMVVMDC.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TareaproyectoMVVMDC.ViewModels
{
    internal class NotesViewModelDC : IQueryAttributable
    {
        public ObservableCollection<ViewModels.NoteViewModelDC> AllNotesDC { get; }
        public ICommand NewCommandDC { get; }
        public ICommand SelectNoteCommandDC { get; }

        public NotesViewModelDC()
        {
            AllNotesDC = new ObservableCollection<ViewModels.NoteViewModelDC>(Models.NoteDC.LoadAll().Select(n => new NoteViewModelDC(n)));
            NewCommandDC = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommandDC = new AsyncRelayCommand<ViewModels.NoteViewModelDC>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.NotePageDC));
        }

        private async Task SelectNoteAsync(ViewModels.NoteViewModelDC note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.NotePageDC)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                NoteViewModelDC matchedNote = AllNotesDC.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotesDC.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                NoteViewModelDC matchedNote = AllNotesDC.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotesDC.Move(AllNotesDC.IndexOf(matchedNote), 0);
                }
                // If note isn't found, it's new; add it.
                else
                    AllNotesDC.Insert(0, new NoteViewModelDC(Models.NoteDC.Load(noteId)));
            }
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ClassListWPF.Models;
using ClassListWPF.Persistence;
using ClassListWPF.Properties;

namespace ClassListWPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Public Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Method

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Public Property

        public List<Form> Forms => FormRepository.GetInstance().GetForms();
        
        public List<Pupil> Pupils { get; private set; }
        public Form SelectedForm { get; set; }

        public Pupil SelectedPupil { get; set; }

        #endregion

        #region Public Method

        public void UpdateForms()
        {
            OnPropertyChanged(nameof(Forms));
        }

        public void UpdatePupils()
        {
            Pupils = PupilRepository.GetInstance().GetPupils().Where(p => p.Class == SelectedForm.Name).ToList();
            OnPropertyChanged(nameof(Pupils));
        }

        #endregion
    }
}
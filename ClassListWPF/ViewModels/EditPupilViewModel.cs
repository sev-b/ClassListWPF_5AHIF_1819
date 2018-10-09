using System.Linq;
using ClassListWPF.Models;
using ClassListWPF.Persistence;

namespace ClassListWPF.ViewModels
{
    public class EditPupilViewModel
    {
        private readonly Pupil _initialPupil;
        public Pupil SelectedPupil { get; set; }

        public EditPupilViewModel(Pupil pupil)
        {
            _initialPupil = pupil;
            SelectedPupil = new Pupil
            {
                Birthday = _initialPupil.Birthday,
                Class = _initialPupil.Class,
                Firstname = _initialPupil.Firstname,
                Lastname = _initialPupil.Lastname,
                Sex = _initialPupil.Sex
            };
        }

        public void DeletePupil()
        {
            PupilRepository.GetInstance().RemovePupil(SelectedPupil);
        }

        public void SavePupil()
        {
            _initialPupil.Class = SelectedPupil.Class;
            _initialPupil.Birthday = SelectedPupil.Birthday;
            _initialPupil.Firstname = SelectedPupil.Firstname;
            _initialPupil.Lastname = SelectedPupil.Lastname;
            _initialPupil.Sex = SelectedPupil.Sex;

            if (FormRepository.GetInstance().GetForms().All(f => f.Name != _initialPupil.Class))
            {
                FormRepository.GetInstance().AddForm(new Form
                {
                    Name = _initialPupil.Class
                });
            }

            PupilRepository.GetInstance().UpdatePupil(_initialPupil);
        }
    }
}
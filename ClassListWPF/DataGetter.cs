using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClassListWPF.Models;
using ClassListWPF.Persistence;

namespace ClassListWPF
{
    public delegate void ProgressHandler(int percentage);
    public class DataGetter
    {
        public static ProgressHandler Progress { get; set; }

        #region Public Method

        public static void Import(string filename)
        {
            var forms = FormRepository.GetInstance();
            var pupils = PupilRepository.GetInstance();
            var reader = new StreamReader(filename);
            var fileInfo = new FileInfo(filename);
            int bytesRead = reader.ReadLine().Length + 2;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                bytesRead += line.Length + 2;
                var data = line.Split(';');
                var form = new Form { Name = data[0] };
                var pupil = new Pupil { Class = data[0], Lastname = data[1], Firstname = data[2], Sex = data[3], Birthday = DateTime.Parse(data[4]) };

                if (forms.GetForms().All(f => f.Name != form.Name))
                {
                    forms.AddForm(form);
                }

                pupils.AddPupil(pupil);
                Progress?.Invoke(100* bytesRead / (int)fileInfo.Length);
            }
        }

        public static Task ImportAsync(string filename)
        {
            return Task.Run(() => Import(filename));
        }

        #endregion
    }
}
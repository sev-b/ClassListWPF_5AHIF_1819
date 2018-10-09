using System.Collections.Generic;
using System.Linq;
using ClassListWPF.Models;

namespace ClassListWPF.Persistence
{
    public class PupilRepository
    {
        #region Private Field

        private static PupilRepository instance;

        #endregion

        public void RemovePupil(Pupil pupil)
        {
            using (var ctx = new ClassListDbContext())
            {
                if (!ctx.Pupils.Contains(pupil))
                {
                    return;
                }

                ctx.Pupils.Remove(pupil);
                ctx.SaveChanges();
            }
        }

        public void UpdatePupil(Pupil pupil)
        {
            using (var ctx = new ClassListDbContext())
            {
                ctx.Pupils.Update(pupil);
                ctx.SaveChanges();
            }
        }

        #region Public Method

        public void AddPupil(Pupil pupil)
        {
            using (var ctx = new ClassListDbContext())
            {
                if (ctx.Pupils.Any(p => p.Class == pupil.Class && p.Firstname == pupil.Firstname && p.Lastname == pupil.Lastname))
                {
                    return;
                }
                ctx.Pupils.Add(pupil);
                ctx.SaveChanges();
            }
        }

        public static PupilRepository GetInstance() => instance ?? (instance = new PupilRepository());

        public List<Pupil> GetPupils() => new ClassListDbContext().Pupils.ToList();

        #endregion
    }
}
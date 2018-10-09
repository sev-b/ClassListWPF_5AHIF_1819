using System.Collections.Generic;
using System.Linq;
using ClassListWPF.Models;

namespace ClassListWPF.Persistence
{
    internal class FormRepository
    {
        #region Private Field

        private static FormRepository _instance;

        #endregion

        #region Public Method

        public void AddForm(Form form)
        {
            using (var ctx = new ClassListDbContext())
            {
                if (ctx.Forms.Any(f => f.Name == form.Name))
                {
                    return;
                }

                ctx.Forms.Add(form);
                ctx.SaveChanges();
            }
        }

        public List<Form> GetForms() => new ClassListDbContext().Forms.ToList();

        public static FormRepository GetInstance() => _instance ?? (_instance = new FormRepository());

        public void RemoveForm(Form form)
        {
            using (var ctx = new ClassListDbContext())
            {
                if (!ctx.Forms.Contains(form))
                {
                    return;
                }

                ctx.Forms.Remove(form);
                ctx.SaveChanges();
            }
        }

        public void UpdateForm(Form form)
        {
            using (var ctx = new ClassListDbContext())
            {
                ctx.Forms.Update(form);
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}
using ClassListWPF.Models;

namespace ClassListWPF.Persistence
{
    public class UowClassList
    {
        private ClassListDbContext ctx = new ClassListDbContext();
        private RepositoryUow<Form, ClassListDbContext> repForm;
        private readonly RepositoryUow<Pupil, ClassListDbContext> repPupi;
    }
}
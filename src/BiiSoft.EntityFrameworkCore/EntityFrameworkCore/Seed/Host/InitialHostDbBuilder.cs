namespace BiiSoft.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly BiiSoftDbContext _context;

        public InitialHostDbBuilder(BiiSoftDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}

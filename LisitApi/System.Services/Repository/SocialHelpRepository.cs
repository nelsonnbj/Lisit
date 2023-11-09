using System.Data;
using System.Infrastructure.IRepository;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Repository
{
    public class SocialHelpRepository : GenericRepository<SocialHelp>, ISocialHelpRepository
    {
        private readonly AplicationDataContext context;
        public SocialHelpRepository(AplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}

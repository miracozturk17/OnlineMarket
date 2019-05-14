using OM.Database.Context;
using OM.Entities.EntityClass;

namespace OM.Services.Services
{
    public class ConfigrationService
    {
        private readonly OMContext _context;

        public ConfigrationService(OMContext context)
        {
            _context = context;
        }

        public Configration GetConfigration(string key)
        {
            return _context.Configrations.Find(key);
        }
    }
}

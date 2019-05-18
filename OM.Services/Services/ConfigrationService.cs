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

        public Configration GetConfig(string Key)
        {
            return _context.Configrations.Find(Key);
        }

        public int PageSize()
        {
            var pageSizeConfig = _context.Configrations.Find("PageSize");

            return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 5;
        }

        public int ShopPageSize()
        {
            var pageSizeConfig = _context.Configrations.Find("ShopPageSize");

            return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
        }
    }
}

using OM.Database.Context;
using OM.Entities.EntityClass;

namespace OM.Services.Services
{
    public class ConfigrationService
    {
        private readonly OMContext _context;

        /*
        public ConfigrationService(OMContext context)
        {
            _context = context;
        }
        */

        #region CTOR
        public ConfigrationService()
        {

        }
        #endregion

        #region SINGLETON PATTERN
        public static ConfigrationService Instance => InstanceValid ?? (InstanceValid = new ConfigrationService());
        private static ConfigrationService InstanceValid { get; set; }
        #endregion

        public Configration GetConfig(string key)
        {
            return _context.Configrations.Find(key);
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

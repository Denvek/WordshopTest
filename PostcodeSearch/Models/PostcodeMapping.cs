using PostcodeSearch.Db.Models;
using TinyCsvParser.Mapping;

namespace PostcodeSearch.Models
{
    public class PostcodeMapping:CsvMapping<PostcodeEntry>
    {
        public PostcodeMapping()
            :base()
        {
            MapProperty(0, p => p.Postcode);
            MapProperty(42, p => p.Latitude);
            MapProperty(43, p=>p.Longitude);
        }
    }
}

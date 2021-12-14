using PostcodeSearch.Db;
using PostcodeSearch.Db.Models;
using System.Device.Location;

namespace PostcodeSearch.Services.Impl
{
    public class DbPostcodeRepo : IPostcodeRepo
    {
        private readonly PostcodeDbContext db;

        public DbPostcodeRepo(PostcodeDbContext db)
        {
            this.db = db;
        }

        public List<PostcodeEntry> Search(string partialPostcode)
        {
            var sanitisedPostcode = partialPostcode.ToUpper().Replace(" ", string.Empty);
            return this.db.Postcodes.Where(p => (p.Postcode ?? string.Empty).ToUpper().Contains(sanitisedPostcode)).ToList();
        }

        public List<PostcodeEntry> Search(double latitude, double longitude, double distanceKM)
        {
            var geoCoord = new GeoCoordinate(latitude, longitude);
            return this.db.Postcodes.Where(p => geoCoord.GetDistanceTo(new GeoCoordinate(p.Latitude.Value, p.Longitude.Value)) * 1000 < distanceKM).ToList();
        }
    }
}

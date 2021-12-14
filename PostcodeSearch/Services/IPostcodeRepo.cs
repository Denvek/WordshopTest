using PostcodeSearch.Db.Models;

namespace PostcodeSearch.Services
{
    public interface IPostcodeRepo
    {
        List<PostcodeEntry> Search(string partialPostcode);

        List<PostcodeEntry> Search(double latitude, double longitude, double distanceKM);
    }
}

using PostcodeSearch.Db.Models;

namespace PostcodeSearch.Services
{
    public interface IPostcodeRepo
    {
        /// <summary>
        /// Searches for all postcodes partially matching the search string
        /// </summary>
        /// <param name="partialPostcode">The search string</param>
        /// <returns>A list of postcodes</returns>
        List<string> Search(string partialPostcode);

        /// <summary>
        /// Searches for all postcodes withing the given distanc in km of the given point
        /// </summary>
        /// <param name="latitude">The latitude</param>
        /// <param name="longitude">The longitude</param>
        /// <param name="distanceKM">The search radius</param>
        /// <returns>A list of postcodes</returns>
        List<string> Search(double latitude, double longitude, double distanceKM);

        /// <summary>
        /// Downloads and imports postcodes from the web
        /// </summary>
        void Import();
    }
}

using Microsoft.AspNetCore.Http;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulation Country entity
    /// </summary>
    public interface ICountriesUploaderService
    {
        /// <summary>
        /// Uploads countries from excel file into database
        /// </summary>
        /// <param name="fomrFile"></param>
        /// <returns>Returns the country object after adding it (including newly generated </returns>
        Task<int> UploadCountriesFromExcelFile(IFormFile formFile);
    }
}

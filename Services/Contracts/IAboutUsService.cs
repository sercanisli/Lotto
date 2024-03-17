using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface IAboutUsService 
    {
        Task<AboutUsDto> GetOneAboutUsAsync(int id, bool trackchanges);
        Task<AboutUsDto> CreateOneAboutUsAsync(AboutUsDto aboutUsDto);
        Task UpdateAboutUsAsync(int id, AboutUsDto aboutUsDto, bool trackchanges);
        Task DeleteAboutUsAsync(int id, bool trackChanges);
    }
}

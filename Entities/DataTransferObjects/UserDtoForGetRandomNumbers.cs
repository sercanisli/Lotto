namespace Entities.DataTransferObjects
{
    public record UserDtoForGetRandomNumbers
    {
        public string UserId { get; init; }
        public string UserName { get; init; }
    }
}

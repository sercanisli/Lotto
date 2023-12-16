namespace Services.Contracts
{
    public interface ICacheService
    {
        //CacheData<T> GetDatas<T>(string key);
        T GetData<T> (string key);
        bool SetData<T> (string key, T value, DateTimeOffset expirationTime);
        object RemoveData(string key);

    }
}

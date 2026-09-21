namespace Ucu.Poo.Repositories
{
    /// <summary>
    /// Esta clase representa un auto.
    /// </summary>
    public interface IItem
    {
        bool HasValue(string field, string value);
    }
}
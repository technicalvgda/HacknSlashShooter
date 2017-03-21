/// <summary>
/// Defines door behavior.
/// </summary>
public interface Door
{
    //opens the door
    void Open();
    //closes the door
    void Close();
    //unlocks the door, may not be used on all doors
    void Unlock();
    //locks the door, may not be used on all doors
    void Lock();
}
namespace Assets.Scripts.Door
{
    [System.Serializable]
    public class DataDoor
    {
        public DoorInteract DoorInteract;
        public bool IsOpenKey;

        public void Init()
        {
            DoorInteract.Init();
        }
        public DataDoor(DoorInteract doorInteract, bool Open)
        {
            DoorInteract = doorInteract;
            IsOpenKey = Open;
        }
    }
}

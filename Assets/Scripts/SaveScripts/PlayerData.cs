using System.Collections.Generic;

namespace Assets.Scripts.SaveScripts
{
    [System.Serializable]
    public class PlayerData
    {
        public int CountSave;
        public MyVector3 Position;
        public List<bool> Doors;
        public List<bool> Triggers;
        public bool IsGrapKey;

        public void AddDoor(bool dooropen)
        {
            Doors.Add(dooropen);
        }
    }
}

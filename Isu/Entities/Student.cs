namespace Isu.Entities
{
    public class Student
    {
        private int _id;
        private string _name;
        private GroupName _groupName;

        public Student(int id, string name, GroupName groupName)
        {
            _id = id;
            _name = name;
            _groupName = groupName;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public GroupName GetGroupName()
        {
            return _groupName;
        }

        internal GroupName SetGroupName(GroupName newGroupName)
        {
            return _groupName = newGroupName;
        }
    }
}
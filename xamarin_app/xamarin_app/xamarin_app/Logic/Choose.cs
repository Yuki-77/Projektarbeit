using System;

namespace xamarin_app.Logic
{
    /// <summary>
    /// This class holds all informations about an Category
    /// </summary>
    public class Choose : IComparable<Choose>
    {
        private readonly string name;
        private readonly string icon;
        private readonly int id;
        private readonly bool exec;

        public Choose(string name, string icon, int id, bool exec)
        {
            this.name = name;
            this.icon = icon;
            this.id = id;
            this.exec = exec;
        }

        public string GetName() { return name; }

        public string GetIcon() { return icon; }

        public int GetId() { return id; }

        public bool GetExec() { return exec; }

        /// <summary>
        /// This Method compares two Languages alphabetical
        /// </summary>
        /// <param name="other">The compared Language</param>
        public int CompareTo(Choose other)
        {
            if (other == null) return 1;

            string otherName = other.GetName();
            if (otherName != null)
                return this.name.CompareTo(otherName);
            else
                throw new ArgumentException("Object is not from type Choose");
        }
    }
}

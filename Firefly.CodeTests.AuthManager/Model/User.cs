namespace Firefly.CodeTests.AuthManager.Model
{
    public abstract class User 
    {
        public abstract string Username { get; }
        public abstract  string Password { get;  }

        public virtual bool Equals(User other)
        {
            return Username.Equals(other.Username);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }
    }
}
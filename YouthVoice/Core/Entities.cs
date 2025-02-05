using System.Data;
using System.Text.RegularExpressions;

// Database structure representment 
namespace Core.Entities
{
    public class User
    {
		private int id;
		private string username;
		private string name;
		private string surname;
		private string mobile;
		private string email;
		private string role;


		public string Role
		{
			get { return role; }
			set {
				if (value != "Administrator" || value != "Representative" || value != "Moderator")
					throw new ArgumentException("Невалидна роля.");

				role = value; 
			}
		}

		public string Email
		{
			get { return email; }
			set {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(value, pattern))
					throw new ArgumentException("Невалиден имейл.");
				
				email = value; 
			}
		}

		public string Mobile
		{
			get { return mobile; }
			set {
				if (value.Length != 9)
					throw new ArgumentException("Невалиден телефонен номер.");

				mobile = value; 
			}
		}

		public string Surname
		{
			get { return surname; }
			set {
				if (value.Length < 2 || value.Length > 20)
					throw new ArgumentOutOfRangeException("Невалидна фамилия.");

				surname = value; 
			}
		}

		public string Name
		{
			get { return name; }
			set {
                if (value.Length < 2 || value.Length > 20)
                    throw new ArgumentOutOfRangeException("Невалидно име.");

                name = value; 
			}
		}

		public string Username
		{
			get { return username; }
			set {
                if (value.Length < 2 || value.Length > 15)
                    throw new ArgumentOutOfRangeException("Потребителското име трябва да съдържа между 2 и 15 символа.");

                username = value; 
			}
		}

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

        public User(int id, string username, string name, string surname, string mobile, string email, string role)
        {
            Id = id;
			Username = username;
			Name = name;
			Surname = surname;
			Mobile = mobile;
			Email = email;
			Role = role;
        }
    }


	public class Organisation
	{
		private int id;
		private string name;
		private string city;
		private List<Event> events;

		public List<Event> Events
		{
			get { return events; }
			set { events = value; }
		}


		public string City
		{
			get { return city; }
			set {
				if (value.Length < 2 || value.Length > 20)
					throw new ArgumentOutOfRangeException("Невалиден град.");

				city = value; 
			}
		}

		public string Name
		{
			get { return name; }
			set {
				if (value.Length < 2 || value.Length > 30)
					throw new ArgumentOutOfRangeException("Невалидно име.");

				name = value; 
			}
		}

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

        public Organisation(int id, string name, string location)
        {
            Id = id;
			Name = name;
			City = location;

			events = new List<Event>();
        }
    }


	public class Event
	{
		private int id;
		private string type;
		private DateTime date;
		private TimeOnly hour;
		private double duration;
		private string location;
		private Organisation organisation;
		private string description;

		public string Description
		{
			get { return description; }
			set {
				if (value.Length < 10 || value.Length > 2000)
					throw new ArgumentOutOfRangeException("Невалидно описание.");

				description = value; 
			}
		}

		public Organisation Organisation
		{
			get { return organisation; }
			set { organisation = value; }
		}

		public string Location
		{
			get { return location; }
			set {
				if (value.Length < 5 || value.Length > 60)
					throw new ArgumentOutOfRangeException("Невалидно местоположение.");

				location = value; 
			}
		}

		public double Duration
		{
			get { return duration; }
			set {
				if (value < 0 || value > 672)
					throw new ArgumentOutOfRangeException("Невалиден период.");

				duration = value; 
			}
		}

		public TimeOnly Hour
		{
			get { return hour; }
			set { hour = value; }
		}

		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

        public Event(int id, string type, DateTime date, TimeOnly hour, double duration, string location, Organisation organisation, string description)
        {
            Id = id;
			Type = type;
			Date = date;
			Hour = hour;
			Duration = duration;
			Location = location;
			Organisation = organisation;
			Description = description;
        }
    }
}
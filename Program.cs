using System;
using System.Linq;

namespace MongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a database and insert some dummy data

            MongoCRUD db = new MongoCRUD("AddressBook");

            db.InsertRecord("Users", new PersonModel { FirstName = "Russ", LastName = "VeganRankin", IsAdmin = true });
            db.InsertRecord("Users", new PersonModel { FirstName = "Billy", LastName = "MeatballMilano", IsAdmin = true });
            db.InsertRecord("Users", new PersonModel { FirstName = "Colin", LastName = "AnimalJerwood", IsAdmin = true });
            db.InsertRecord("Users", new PersonModel { FirstName = "Wattie", LastName = "HeartAttackBuchan" });
            db.InsertRecord("Users", new PersonModel { FirstName = "Elvis", LastName = "ToiletPresley" });
            
            //Load the database we've just created
            var records = db.LoadRecords<PersonModel>("Users");

            //Loop through the records and show them
            foreach (var record in records)
            {
                Console.WriteLine($"{record.FirstName}\t\t{record.LastName}\t\tAdministrator: {record.IsAdmin}");
            };

            //Ask a first and lastname to search for and delete record from the database
            Console.Write("Enter the firstname: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter the lastname: ");
            string lastName = Console.ReadLine();

            db.DeleteRecord<PersonModel>("Users", firstName, lastName);

            //reload database and show it again, this time the record should be deleted
            var records2 = db.LoadRecords<PersonModel>("Users");

            foreach (var IsAdmin in records2.Where(t => t.IsAdmin == true))
            {
                Console.WriteLine(IsAdmin.FirstName);
            };


            //This time, only get the lastname and check if the person is an admin
            Console.Write("Enter the lastname: ");
            string findLastName = Console.ReadLine();

            var searchResult = db.FindRecord<PersonModel>("Users", findLastName);

            //Display every record that complies with the search parameter
            foreach (var item in searchResult)
            {
                Console.WriteLine($"{item.FirstName}  {item.LastName}  Administrator: {item.IsAdmin}");
            }

            Console.ReadLine();
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectMapper.TestApp {
    class Program {
        static void Main (string[] args) {
            var person = new Person ();
            person = person.GetDemo ();

            Console.WriteLine (person.FirstName);
        }
    }

    public static class ObjectMapper {
        public static U MapObject<T, U> (T sourceObject, U destObject)
        where U : class, new ()
        where T : class, new () {
            List<PropertyInfo> sourceProperties = sourceObject.GetType ().GetProperties ().ToList ();
            List<PropertyInfo> destinationProperties = sourceObject.GetType ().GetProperties ().ToList ();

            foreach (PropertyInfo sourceProperty in sourceProperties) {
                PropertyInfo destinationProperty = destinationProperties.Find (item => item.Name == sourceProperty.Name);

                if (!sourceProperty.GetType ().Name.Contains ("String")) {

                }

                if (destinationProperty != null) {
                    try {
                        destinationProperty.SetValue (destObject, sourceProperty.GetValue (sourceObject, null), null);
                    } catch (ArgumentException) {

                    }
                }
            }

            return (U) destObject;
        }
    }
    public class Person {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public List<Address> Addresses { get; set; }
        public Person GetDemo () {
            Person person = new Person {
                Id = 1,
                FirstName = "Jeremey",
                LastName = "Schrack",
                MiddleName = "D",
                Addresses = new List<Address> ()
            };

            Address address1 = new Address {
                Id = 1,
                PersonId = 1,
                Street1 = "5018 E Kittentails Drive",
                Street2 = "",
                City = "Tucson",
                State = "AZ",
                Zip = "85756"
            };

            person.Addresses.Add (address1);

            Address address2 = new Address {
                Id = 2,
                PersonId = 1,
                Street1 = "210 Whitworth Drive",
                Street2 = "",
                City = "Martinez",
                State = "GA",
                Zip = "30907"
            };

            person.Addresses.Add (address2);

            return person;
        }
    }

    public class PersonModel {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public List<AddressModel> Addresses { get; set; }
    }

    public class Address {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class AddressModel {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.Models
{
    public class PersonModel
    {
        public long ID { get; set; }

        [Required, StringLength(15), Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required, StringLength(15), Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required, MinLength(9), StringLength(15), Display(Name = "Telefon")]
        public string Phone { get; set; }

        [EmailAddress, Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Required, Display(Name = "Data Utworzenia")]
        public DateTime Created { get; set; }

        [Display(Name = "Data Modyfikacji")]
        public DateTime? Updated { get; set; }

        public PersonModel(string firstName, string lastName, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Created = DateTime.Now;
        }

        public PersonModel()
        {
        }
    }
}
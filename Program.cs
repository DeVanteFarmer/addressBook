// See https://aka.ms/new-console-template for more information
/*

Instructions
1. Create a new console application.
2. Replace the default Program class with the code below.
3. Follow the instructions in the comments.

Hint: For the address book's list of contacts, the easiest and most efficient way to structure your data
      is a dictionary with the person's email address as the key and their contact as a value. 
      We'll talk about dictionaries in slightly more depth later, but for today you can skip ahead to this chapter 
      and just read the part about dictionaries. Here's an example of what your data structure might 
      look like in your AddressBook class.


"private Dictionary<string, Contact> _contactList {get; set;} = new Dictionary<string, Contact>();"

*/




using System;
using System.Collections.Generic;
public class Program
{
    /*
        1. Add the required classes to make the following code compile.
        

        2. Run the program and observe the exception.

        3. Add try/catch blocks in the appropriate locations to prevent the program from crashing
            Print meaningful error messages in the catch blocks.
    */

    static void Main(string[] args)
    {
        try 
        {
        // Create a few contacts
        Contact bob = new Contact() {
            FirstName = "Bob", 
            LastName = "Smith",
            Email = "bob.smith@email.com",
            Address = "100 Some Ln, Testville, TN 11111"
        };
        Contact sue = new Contact() {
            FirstName = "Sue", 
            LastName = "Jones",
            Email = "sue.jones@email.com",
            Address = "322 Hard Way, Testville, TN 11111"
        };
        Contact juan = new Contact() {
            FirstName = "Juan", 
            LastName = "Lopez",
            Email = "juan.lopez@email.com",
            Address = "888 Easy St, Testville, TN 11111"
        };


        // Create an AddressBook and add some contacts to it
        AddressBook addressBook = new AddressBook();
        addressBook.AddContact(bob);
        addressBook.AddContact(sue);
        addressBook.AddContact(juan);

        // Try to add a contact a second time
            try 
            {
                addressBook.AddContact(sue);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }


        // Create a list of emails that match our Contacts
        List<string> emails = new List<string>() {
            "sue.jones@email.com",
            "juan.lopez@email.com",
            "bob.smith@email.com",
        };

        // Insert an email that does NOT match a Contact
        emails.Insert(1, "not.in.addressbook@email.com");


        //  Search the AddressBook by email and print the information about each Contact
        foreach (string email in emails)
        {
            try
            {
                Contact contact = addressBook.GetByEmail(email);
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Name: {contact.FullName}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Address: {contact.Address}");
            }
            catch (KeyNotFoundException ex)
            {
                
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

public class Contact
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public string FullName 
    {
        get { return $"{FirstName ?? "Unknown"} {LastName ?? "Unknown"}"; }
    }
}

public class AddressBook
{
    private Dictionary<string, Contact> _contactList { get; set; } = new Dictionary<string, Contact>();

    public void AddContact(Contact contact)
    {
         if (contact.Email == null)
        {
            throw new ArgumentException("Contact must have an email address.");
        }

        if (_contactList.ContainsKey(contact.Email))
        {
            throw new ArgumentException($"A contact with the email {contact.Email} already exists.");
        }
        _contactList[contact.Email] = contact;
    }

    public Contact GetByEmail(string email)
    {
        if (email == null)
        {
            throw new ArgumentNullException(nameof(email), "Email cannot be null.");
        }

        if (!_contactList.ContainsKey(email)) 
        {
            throw new KeyNotFoundException($"No contact found with the email {email}.");
        }
        return _contactList[email];
    }
}
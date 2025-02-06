using System;
using System.Collections.Generic;

public enum Role
{
    Admin,
    User
}

public class User
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role UserRole { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string FullName { get; set; }
}

public class Program
{
    static List<User> users = new List<User>();
    static List<string> notes = new List<string>();

    public static void Main()
    {
        // Başlangıçta kullanıcıları ve yöneticiyi ekleyelim
        users.Add(new User
        {
            Username = "admin",
            Email = "mid@gmail.com", // Yönetici e-posta
            Password = "123456", // Yönetici şifresi
            UserRole = Role.Admin,
            RegistrationDate = DateTime.Now,
            FullName = "Muhammet İslam Demir"
        });

        users.Add(new User
        {
            Username = "user1",
            Email = "user1@gmail.com",
            Password = "password1",
            UserRole = Role.User,
            RegistrationDate = DateTime.Now,
            FullName = "User One"
        });
            users.Add(new User
        {
            Username = "tahir",
            Email = "tahir@gmail.com",
            Password = "123456",
            UserRole = Role.User,
            RegistrationDate = DateTime.Now,
            FullName = "Tahir bey"
        });

        users.Add(new User
        {
            Username = "user2",
            Email = "user2@gmail.com",
            Password = "password2",
            UserRole = Role.User,
            RegistrationDate = DateTime.Now,
            FullName = "User Two"
        });

        // Kullanıcıdan giriş bilgilerini alalım
        Console.WriteLine("E-posta: ");
        string email = Console.ReadLine();
        Console.WriteLine("Şifre: ");
        string password = Console.ReadLine();

        User loggedInUser = AuthenticateUser(email, password);
        if (loggedInUser != null)
        {
            Console.WriteLine($"Hoşgeldiniz, {loggedInUser.FullName}!");
            DisplayMainMenu(loggedInUser); // Ana menüyü başlat
        }
        else
        {
            Console.WriteLine("Geçersiz e-posta veya şifre!");
        }
    }

    static User AuthenticateUser(string email, string password)
    {
        foreach (var user in users)
        {
            if (user.Email == email && user.Password == password)
            {
                return user;
            }
        }
        return null;
    }

    static void DisplayMainMenu(User user)
    {
        bool exit = false;
        while (!exit)
        {
            if (user.UserRole == Role.Admin)
            {
                AdminMenu(user); // Yönetici menüsüne yönlendirme
            }
            else
            {
                UserMenu(user); // Kullanıcı menüsüne yönlendirme
            }
        }
    }

    static void AdminMenu(User user)
    {
        Console.Clear();
        Console.WriteLine("\n--- Yöneticilik Menüsü ---");
        Console.WriteLine("1. Notları Görüntüle");
        Console.WriteLine("2. Yeni Kullanıcı Ekle");
        Console.WriteLine("3. Not Ekle");
        Console.WriteLine("4. Not Sil");
        Console.WriteLine("5. Not Güncelle");
        Console.WriteLine("6. Kullanıcıları Görüntüle");
        Console.WriteLine("7. Çıkış");
        Console.Write("Seçiminiz: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                DisplayNotes();
                break;
            case "2":
                AddNewUser();
                break;
            case "3":
                AddNote();
                break;
            case "4":
                DeleteNote();
                break;
            case "5":
                UpdateNote();
                break;
            case "6":
                DisplayUsers();
                break;
            case "7":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Geçersiz seçenek!");
                break;
        }

        // İşlem yaptıktan sonra tekrar menüye dönülmesi sağlanıyor
        Console.WriteLine("\nİşlem tamamlandı. Ana menüye dönmek için herhangi bir tuşa basın...");
        Console.ReadKey();
        Console.Clear();
    }

    static void UserMenu(User user)
    {
        Console.Clear();
        Console.WriteLine("\n--- Kullanıcı Menüsü ---");
        Console.WriteLine("1. Not Ekle");
        Console.WriteLine("2. Notları Görüntüle");
        Console.WriteLine("3. Çıkış");
        Console.Write("Seçiminiz: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddNote();
                break;
            case "2":
                DisplayNotes();
                break;
            case "3":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Geçersiz seçenek!");
                break;
        }

        // İşlem yaptıktan sonra tekrar menüye dönülmesi sağlanıyor
        Console.WriteLine("\nİşlem tamamlandı. Ana menüye dönmek için herhangi bir tuşa basın...");
        Console.ReadKey();
        Console.Clear();
    }

    static void AddNote()
    {
        Console.WriteLine("Yeni notunuzu girin:");
        string note = Console.ReadLine();
        notes.Add($"{note} - {DateTime.Now}");
        Console.WriteLine("Not başarıyla eklendi!");
    }

    static void DisplayNotes()
    {
        if (notes.Count == 0)
        {
            Console.WriteLine("Hiç not bulunmamaktadır.");
        }
        else
        {
            for (int i = 0; i < notes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {notes[i]}");
            }
        }
    }

    static void DeleteNote()
    {
        DisplayNotes();
        Console.WriteLine("\nSilmek istediğiniz not numarasını girin:");
        if (int.TryParse(Console.ReadLine(), out int noteIndex) && noteIndex > 0 && noteIndex <= notes.Count)
        {
            notes.RemoveAt(noteIndex - 1);
            Console.WriteLine("Not başarıyla silindi.");
        }
        else
        {
            Console.WriteLine("Geçersiz not numarası!");
        }
    }

    static void UpdateNote()
    {
        DisplayNotes();
        Console.WriteLine("\nGüncellemek istediğiniz not numarasını girin:");
        if (int.TryParse(Console.ReadLine(), out int noteIndex) && noteIndex > 0 && noteIndex <= notes.Count)
        {
            Console.WriteLine("Yeni notunuzu girin:");
            string newNote = Console.ReadLine();
            notes[noteIndex - 1] = $"{newNote} - {DateTime.Now}";
            Console.WriteLine("Not başarıyla güncellendi.");
        }
        else
        {
            Console.WriteLine("Geçersiz not numarası!");
        }
    }

    static void AddNewUser()
    {
        Console.WriteLine("Yeni kullanıcının kullanıcı adını girin:");
        string username = Console.ReadLine();
        Console.WriteLine("Yeni kullanıcının e-posta adresini girin (Sonu '@gmail.com' olmalı):");
        string email = Console.ReadLine();
        
        // E-posta doğrulaması
        if (!email.EndsWith("@gmail.com"))
        {
            Console.WriteLine("E-posta adresi '@gmail.com' ile bitmelidir.");
            return;
        }

        Console.WriteLine("Yeni kullanıcının şifresini girin (en az 6 karakter):");
        string password = Console.ReadLine();
        if (password.Length < 6)
        {
            Console.WriteLine("Şifre en az 6 karakter olmalıdır.");
            return;
        }
        Console.WriteLine("Yeni kullanıcının adını girin:");
        string fullName = Console.ReadLine();

        users.Add(new User
        {
            Username = username,
            Email = email,
            Password = password,
            UserRole = Role.User,
            RegistrationDate = DateTime.Now,
            FullName = fullName
        });

        Console.WriteLine("Yeni kullanıcı başarıyla eklendi.");
    }

    static void DisplayUsers()
    {
        Console.WriteLine("\n--- Kullanıcı Listesi ---");
        if (users.Count == 0)
        {
            Console.WriteLine("Hiç kullanıcı bulunmamaktadır.");
        }
        else
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Kullanıcı Adı: {user.Username}, E-posta: {user.Email}, Ad: {user.FullName}, Kayıt Tarihi: {user.RegistrationDate}");
            }
        }
    }
}

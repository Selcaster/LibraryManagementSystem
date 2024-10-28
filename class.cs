using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    
        public abstract class Person
        {
            public abstract string Name { get; set; }
            public static int count;
            public int Id { get; private set; }
            public Person(string name)
            {
                this.Name = name;
                this.Id = ++count;
            }
        }

        public abstract class Librarian : Person
        {
            public abstract DateTime HireDate { get; set; }
            public Librarian(string name, DateTime hireDate) : base(name)
            {
                this.HireDate = hireDate;
            }
        }

        public abstract class LibraryMember : Person
        {
            public abstract DateTime MembershipDate { get; set; }
            public LibraryMember(string name, DateTime member) : base(name)
            {
                this.MembershipDate = member;
            }
        }

        public abstract class LibraryItem
        {
            public int Id { get; private set; }
            public static int itemCount = 0;
            public abstract string Title { get; set; }
            public abstract int? PublicationYear { get; set; }
            public LibraryLocation Location { get; set; }  // Location of the item
            public LibraryItem(string title, int? publicationYear, LibraryLocation location)
            {
                this.Title = title;
                this.PublicationYear = publicationYear;
                this.Location = location;
                this.Id = ++itemCount;  // Assign unique ID to each library item
            }
            public abstract void DisplayInfo();
        }

        public class Book : LibraryItem
        {
            private string title;
            private int? publicationYear;
            public override string Title
            {
                get { return title; }
                set { title = value; }
            }
            public override int? PublicationYear
            {
                get { return publicationYear; }
                set { publicationYear = value; }
            }
            public string Genre { get; set; }
            public Book(BookGenre genre, string title, int? publicationYear, LibraryLocation location)
                : base(title, publicationYear, location)
            {
                this.Genre = genre.ToString();//error-line cannot implicitly convert type 'LibraryManagementSystem.BookGenre' to 'string'
                this.Title = title;
                this.PublicationYear = publicationYear;
            }
            public override void DisplayInfo()
            {
                if (PublicationYear == null)
                    Console.WriteLine($"Kitabimiz {Title} adlanir, buraxilis ili namelumdur.");
                else
                    Console.WriteLine($"Kitabimiz {Title} adlanir, ilk defe {PublicationYear} ilde nesr olunub.");
            }
        }

        public class Magazine : LibraryItem
        {
            //public string Genre { get; set; }
            //public Magazine(string genre, string title, int? publicationYear, LibraryLocation location)
            //    : base(title, publicationYear, location)
            //{
            //    this.Genre = genre;
            //}
            //public override void DisplayInfo()
            //{
            //    if (PublicationYear == null)
            //        Console.WriteLine($"Jurnalimiz {Title} adlanir, buraxilis ili namelumdur.");
            //    else
            //        Console.WriteLine($"Jurnalimiz {Title} adlanir, ilk defe {PublicationYear} ilde nesr olunub.");
            //}
            private string title;
            private int? publicationYear;
            public override string Title
            {
                get { return title; }
                set { title = value; }
            }
            public override int? PublicationYear
            {
                get { return publicationYear; }
                set { publicationYear = value; }
            }
            public string Genre { get; set; }
            public Magazine(string genre, string title, int? publicationYear, LibraryLocation location)
                : base(title, publicationYear, location)
            {
                this.Genre = genre;
                this.Title = title;
                this.PublicationYear = publicationYear;
            }
            public override void DisplayInfo()
            {
                if (PublicationYear == null)
                    Console.WriteLine($"Jurnalimiz {Title} adlanir, buraxilis ili namelumdur.");
                else
                    Console.WriteLine($"Jurnalimiz {Title} adlanir, ilk defe {PublicationYear} ilde nesr olunub.");
            }
        }

        public class DVD : LibraryItem
        {
            private string title;
            private int? publicationYear;
            public override string Title
            {
                get { return title; }
                set { title = value; }
            }
            public override int? PublicationYear
            {
                get { return publicationYear; }
                set { publicationYear = value; }
            }
            public string Genre { get; set; }
            public DVD(string genre, string title, int? publicationYear, LibraryLocation location)
                : base(title, publicationYear, location)
            {
                this.Genre = genre;
                this.Title = title;
                this.PublicationYear = publicationYear;
            }
            public override void DisplayInfo()
            {
                if (PublicationYear == null)
                    Console.WriteLine($"DVDmiz {Title} adlanir, buraxilis ili namelumdur.");
                else
                    Console.WriteLine($"DVDmiz {Title} adlanir, ilk defe {PublicationYear} ilde yayimlanib.");
            }
        }

        public enum BookGenre
        {
            Fiction = 1,
            NonFiction = 2,
            Science = 3,
            Art = 4,
        }

        public struct LibraryLocation
        {
            public int Aisle { get; set; }
            public int Shelf { get; set; }
            public LibraryLocation(int aisle, int shelf)
            {
                this.Aisle = aisle;
                this.Shelf = shelf;
            }
            public override string ToString()
            {
                return $"Aisle: {Aisle}, Shelf: {Shelf}";
            }
        }

        public static class LibraryHelper
        {
            public static int CalculateAge(this LibraryItem item)
            {
                if (!item.PublicationYear.HasValue) return 0;
                return DateTime.Now.Year - item.PublicationYear.Value;
            }
            public static string ToTitleCase(this LibraryItem item)
            {
                if (string.IsNullOrEmpty(item.Title)) return item.Title;
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Title.ToLower());
            }
        }

        public class CustomBookError : Exception
        {
            public CustomBookError(int id)
                : base($"Book with ID {id} does not exist in the catalog.") { }
        }

        public class LibraryCatalog
        {
            private LibraryItem[] catalog = new LibraryItem[0];
            private int count = 0;
            public LibraryItem this[int id]
            {
                get
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (catalog[i].Id == id)
                        {
                            return catalog[i];
                        }
                    }
                    throw new CustomBookError(id);
                }
            }

            public void AddBook(LibraryItem item)
            {
                Array.Resize(ref catalog, count + 1);
                catalog[count] = item;
                count++;
            }

            public void RemoveBook(int id)
            {
                int indexToRemove = -1;
                for (int i = 0; i < count; i++)
                {
                    if (catalog[i].Id == id)
                    {
                        indexToRemove = i;
                        break;
                    }
                }

                if (indexToRemove != -1)
                {
                    for (int i = indexToRemove; i < count - 1; i++)
                    {
                        catalog[i] = catalog[i + 1];
                    }
                    Array.Resize(ref catalog, count - 1);
                    count--;
                }
                else
                {
                    throw new CustomBookError(id);
                }
            }
        }
}

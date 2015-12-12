using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginService" in code, svc and config file together.
public class LoginService : ILoginService
{
    BookReviewDbEntities db = new BookReviewDbEntities();

    public void AddAuthor(string authorName)
    {

        Author a = new Author();
        a.AuthorName = authorName;
        db.Authors.Add(a);
        db.SaveChanges();


    }

    public void AddBook(NewBook b)
    {
        Book book = new Book();
        book.BookTitle = b.Title;
        book.BookISBN = b.ISBN;
        book.BookEntryDate = DateTime.Now;

        if (b.Authors != null)
        {
            foreach (string a in b.Authors)
            {
                Author author = db.Authors.FirstOrDefault(x => x.AuthorName == a);
                book.Authors.Add(author);
            }
        }


        if (b.Categories != null)
        {
            foreach (string c in b.Categories)
            {
                Category cat = db.Categories.FirstOrDefault(x => x.CategoryName == c);
                book.Categories.Add(cat);
            }
        }

        db.Books.Add(book);
        db.SaveChanges();
    }

        

   

    public void AddReview(NewReview r)
    {
        Review rev = new Review();
        rev.Book = db.Books.FirstOrDefault(x => x.BookTitle == r.BookTitle);
         
        rev.ReviewDate = DateTime.Now;
        rev.ReviewerKey = r.ReviewerKey;
        rev.ReviewRating = r.Rating;
        rev.ReviewTitle = r.ReviewTitle;
        rev.ReviewText = r.ReviewText;
        db.Reviews.Add(rev);
        db.SaveChanges();
    }

    public int GetBookKey(string title)
    {
        int bkKey = 0;
        var key = from k in db.Books
                  where k.BookTitle.Equals(title)
                  select new { k.BookKey };
        foreach (var k in key)
        {
            bkKey = (int)k.BookKey;
        }
        return bkKey;
    }

    public int ReviewerLogin(string username, string password)
    {

        int key = 0;
        int pass=db.usp_ReviewerLogin(username, password);
        //returns 1 if good -1 if bad
        //I had trouble getting the stored procedure to
        //return the userkey so we have to look it up
        if (pass != -1)
        {
            var rev = from r in db.Reviewers
                      where r.ReviewerUserName.Equals(username)
                      select new { r.ReviewerKey };
            foreach (var r in rev)
            {
                key = (int)r.ReviewerKey;
            }
        }


        return key;
    }

    public int ReviewerRegistration(ReviewerLite r)
    {
        int worked = db.usp_NewReviewer
            (r.ReviewerUserName, r.ReviewerFirstName, 
            r.ReviewerLastName, r.ReviewerEmail, r.ReviewerPassword);
        return worked;
    }
}

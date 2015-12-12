using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoginService" in both code and config file together.
[ServiceContract]
public interface ILoginService
{
    [OperationContract]
    int ReviewerLogin(string username, string password);

    [OperationContract]
    int ReviewerRegistration(ReviewerLite r);

    [OperationContract]
    void AddBook(NewBook b);

    [OperationContract]
    void AddAuthor(string authorName);

    [OperationContract]
    void AddReview(NewReview r);

    [OperationContract]
    int GetBookKey(string title);
}

[DataContract]
public class ReviewerLite
{
    [DataMember]
    public string ReviewerUserName { set; get; }

    [DataMember]
    public string ReviewerLastName { set; get; }

    [DataMember]
    public string ReviewerFirstName { set; get; }

    [DataMember]
    public string ReviewerEmail { set; get; }

    [DataMember]
    public string ReviewerPassword { set; get; }
}

[DataContract]
public class NewBook
{
    [DataMember]
    public string Title { set; get; }

    [DataMember]
    public string ISBN { set; get; }

    [DataMember]
    public List<string> Authors
    { set; get; }

                
    [DataMember]
    public List<string> Categories { set; get; }

}
[DataContract]
public class NewReview
{
    [DataMember]
    public string  BookTitle { set; get; }
    [DataMember]
    public int ReviewerKey { set; get; }

    [DataMember]
    public string ReviewTitle { set; get; }

    [DataMember]
    public int Rating { set; get; }

    [DataMember]
    public string ReviewText { set; get; }

}




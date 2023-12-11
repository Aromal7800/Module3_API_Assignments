
using RestSharp;

string baseUrl = " https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseUrl);

GetUsers(client);
createUsers(client);
UpdateUsers(client);
DeleteUsers(client);




static void GetUsers(RestClient client)
{

    var getUserRequest = new RestRequest("users/2", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine(getUserResponse.Content);

}







static void createUsers(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
    //createUserRequest.AddParameter("name", "John Doe");
    //createUserRequest.AddParameter("job", "Software Developer");
    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST User Response :\n");
    Console.WriteLine(createUserResponse.Content);
}

static void UpdateUsers(RestClient client)
{

    var updateUserRequest = new RestRequest("posts/1", Method.Put);

    updateUserRequest.AddHeader("Content-Type", "application/json");

    updateUserRequest.AddJsonBody(new { name = "John Doe updated ", job = "Senior Software Developer" });

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Update Response :");
    Console.WriteLine(updateUserResponse.Content);

}

static void DeleteUsers(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts/1", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("Delete User Response");
    Console.WriteLine(deleteUserResponse.Content);
}
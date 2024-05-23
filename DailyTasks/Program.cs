using Application.Features.Books.Queries.GetById;
using Application.Features.Favorites.Queries.GetList;
using Application.Features.Members.Commands.Update;
using Application.Features.Members.Queries.GetById;
using Application.Features.Reservations.Queries.GetList;
using Application.Features.Users.Queries.GetById;
using MailKit.Net.Smtp;
using MimeKit;
using NArchitecture.Core.Application.Responses;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;

var baseUrl = "http://localhost:60805/";
var client = new HttpClient();

CheckDeliveryPenalties();
BookReserveNotification();
CloseDeliveryDateNotification();
async Task<T> AsyncGet<T>(string Url)
{
    string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE2MTI1MjAxLTRjZTUtNDJkYy1lZTE1LTA4ZGM3MTRiNDc5MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InN0cmluZzNAc3RyaW5nLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNzE1NDExNzc0LCJleHAiOjIzMTU0MTE3MTQsImlzcyI6Im5BcmNoaXRlY3R1cmVAa29kbGFtYS5pbyIsImF1ZCI6InN0YXJ0ZXJQcm9qZWN0QGtvZGxhbWEuaW8ifQ.KoBKorhRkK45aVK_b28gf0UIKqDAwE_VBxEOJ4VCcMjS_b7g3XVSQASNo7A47e8fN8HqIxLQUIDqaCY0pDnxVg";
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    var response = await client.GetAsync(Url);
    var responseString = await response.Content.ReadAsStringAsync();
    var result = JsonConvert.DeserializeObject<T>(responseString);
    return result;
}

async void CheckDeliveryPenalties()
{
    var reservationList = await AsyncGet<GetListResponse<GetListReservationListItemDto>>($"{baseUrl}api/Reservations?PageIndex=0&PageSize=1000");

    var result = reservationList.Items;
    foreach (var item in result)
    {
        var today = DateTime.Now;
        var reserveEndDate = item.ReservationEndDate;
        var Day = today - reserveEndDate;
        double daysDifference = Day.TotalDays;

        if (daysDifference > 0 && item.IsReserv ==true)
        {
            double Penalty = daysDifference * 10;
            var MemberId = item.MemberID;
            var MemberResult = await AsyncGet<GetByUserIdMemberResponse>($"{baseUrl}api/Members/{MemberId}");

            MemberResult.PenaltyAmount = Penalty;

            UpdateMemberCommand updateMemberCommand = new UpdateMemberCommand()
            {
                Id = MemberResult.Id,
                FirstName = MemberResult.FirstName,
                LastName = MemberResult.LastName,
                NationalIdentity = MemberResult.NationalIdentity,
                Adress = MemberResult.Adress,
                PenaltyAmount = Penalty,
                UserId = MemberResult.UserId,

            };

            var json = JsonConvert.SerializeObject(updateMemberCommand);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestResponse = await client.PutAsync($"{baseUrl}api/Members", content);
            var resposeString = await requestResponse.Content.ReadAsStringAsync();

            var UserResult = await AsyncGet<GetByIdUserResponse>($"{baseUrl}api/Users/{updateMemberCommand.UserId}");
            string body = $"Reserve ettiğiniz kitabı {daysDifference * (-1)} gündür teslim etmediğiniz için hesabınıza {Penalty} Tl ceza eklenmiştir. En kısa sürede teslimle birlikte ödemenizi bekliyoruz.";
            string mail = UserResult.Email;
            SendMail(body, mail);
        }
    }

}

async void BookReserveNotification()
{

    var favoriteResult = await AsyncGet<GetListResponse<GetListFavoriteListItemDto>>($"{baseUrl}api/Favorites?PageIndex=0&PageSize=10000");
    var favorites = favoriteResult.Items;
    foreach (var item in favorites)
    {
        var result = await AsyncGet<GetByIdBookResponse>($"{baseUrl}api/Books/{item.BookId}");
        if (result.NumberOfCopies > 0 && item.BookId == result.Id)
        {
            var userResult = await AsyncGet<GetByIdUserResponse>($"{baseUrl}api/Users/{item.UserId}");
            string body = $"İstek Listenizdeki {result.BookName} adlı kitap stoklarımız güncellenmiştir.";
            SendMail(body, userResult.Email);
        }
    }
}

async void CloseDeliveryDateNotification()
{
    var result = await AsyncGet<GetListResponse<GetListReservationListItemDto>>($"{baseUrl}api/Reservations?PageIndex=0&PageSize=1000");
    var reservationList = result.Items;
    foreach (var item in reservationList)
    {
        var today = DateTime.Now;
        var reserveEndDate = item.ReservationEndDate;
        var Day = reserveEndDate - today;
        double daysDifference = Day.TotalDays;

        if (daysDifference < 5 && item.IsReserv == true)
        {
            var memberId = item.MemberID;

            var memberResult = await AsyncGet<GetByUserIdMemberResponse>($"{baseUrl}api/Members/{memberId}");
            var userId = memberResult.UserId;

            var userResult = await AsyncGet<GetByIdUserResponse>($"{baseUrl}api/Users/{userId}");
            var userMail = userResult.Email;

            string message = $"Reserve ettiğiniz kitabın teslim süresine {daysDifference} gün kalmıştır kitabı zamanında teslim etmenizi rica ederiz";
            SendMail(message, userMail);
        }
    }
}
void SendMail(string body, string mail)
{

    string from = "emirkarameke4444@gmail.com";
    string subject = "Test Mail";
    // string body = "Bu bir test mailidir.";

    MimeMessage message = new MimeMessage();
    message.From.Add(MailboxAddress.Parse(from));
    message.To.Add(MailboxAddress.Parse(mail));
    message.Subject = subject;
    message.Body = new TextPart("plain") { Text = body };

    using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
    {
        client.Connect("sandbox.smtp.mailtrap.io", 587, false);
        client.Authenticate("d92fb68cb4c2c0", "fc7235e74b66b8");
        client.Send(message);
        client.Disconnect(true);
    }

    Console.WriteLine("Mail gönderildi!");
}

Console.ReadLine();
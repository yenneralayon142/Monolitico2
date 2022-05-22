using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Options;




namespace EmployeeCRUD.Services
{
    public class SmsSender : ISmsSender

    {
        public SmsOptions Options { get; } // set only via secret manager 

        public SmsSender(IOptions<SmsOptions>optionsAccesor)
        {
            Options = optionsAccesor.Value;
        }

        public Task SendSmsAsync(string number, string message)
        {
            //Plug in your SMS service here to send a text message.
            //Your Account SID from twilio.com/console

            var accountSid = Options.SmSAccountIdentification;

            // Your Auth Token from twilio.com/console

            var authToken = Options.SmSAccountPassword;

            TwilioClient.Init(accountSid, authToken);

            return MessageResource.CreateAsync(
                to: new PhoneNumber(number),
                from: new PhoneNumber(Options.SmSAccountFrom),
                body: message);
        }


    }
}

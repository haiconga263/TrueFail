using FcmSharp.Requests;
using SourceType = Notifications.UI.Messages.Models.Message;
using TargetType = FcmSharp.Requests.FcmMessage;

namespace Notifications.Messages.Converters
{
    public static class MessageConverter
    {
        public static TargetType Convert(SourceType source)
        {
            if (source == null)
            {
                return null;
            }

            return new TargetType
            {
                ValidateOnly = false,
                Message = new Message
                {
                    Topic = source.Topic,
                    Notification = new Notification
                    {
                        Title = source.Title,
                        Body = source.Body
                    }
                }
            };
        }
    }
}

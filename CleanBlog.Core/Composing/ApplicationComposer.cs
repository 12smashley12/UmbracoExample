using SlackBotMessages;
using SlackBotMessages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Umbraco.Core.Composing;

namespace CleanBlog.Core.Composing
{
    public class ApplicationComposer : ComponentComposer<ApplicationComponent>, IUserComposer
    {
    }

    public class ApplicationComponent : IComponent
    {
        public void Initialize()
        {
            try
            {
                var client = new SbmClient(WebConfigurationManager.AppSettings["SlackBotMessagesWebHookUrl"]);

                var message = new Message
                {
                    Username = "Clean Blog Robot",
                    IconEmoji = ":robot_face:", //Emoji.NoEntrySign
                    Attachments = new List<Attachment>()
                    {
                        new Attachment()
                        {
                            Fallback = "Clean Blog Website Started",
                            Color = "good",
                            Fields = new List<Field>()
                            {
                                new Field()
                                {
                                    Value = "Clean Blog Website Started"
                                }
                            }
                        }
                    }
                };

                client.Send(message);
            }
            catch(Exception ex)
            {
                Current.Logger.Error(typeof(ApplicationComponent), ex, "Unable to send slack message to notify site starting up");
            }
        }

        public void Terminate()
        { 
            
        }
    }
}

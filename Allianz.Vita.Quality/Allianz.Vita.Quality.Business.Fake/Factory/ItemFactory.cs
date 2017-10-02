using Allianz.Vita.Quality.Business.Fake.Models;
using Allianz.Vita.Quality.Business.Interfaces;
using System;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Fake.Factory
{
    public class ItemFactory : IItemFactory
    {
        public IDefect GetNewDefect(IMailItem itemRead)
        {
            throw new NotImplementedException();
        }

        public IFolderItem GetNewFolderItem()
        {
            throw new NotImplementedException();
        }

        public IMailItem GetNewMailItem(string uniqueId = "")
        {
            return new MailItem()
            {
                UniqueId = string.IsNullOrEmpty(uniqueId) ? Guid.NewGuid().ToString() : uniqueId
                ,
                From = "srm@allianz.it"
                ,
                Subject = "R: Request 2017/827273 - 27/09/2017 [4a8abb0b-9111-48ff-9c0b-d9f3ab956a13] ALLIANZ RAS 010336000000 BRESSANONE"
                ,
                Content = @"Buongiorno,
L'utente questa mattina ha censito un nuovo cliente cognome e nome Di Meo Umberto ma aveva inserito la data di nascita errata. successivamente alla modifica, riscontra blocco in emissione nuova proposta vita.
Cliente DI MEO UMBERTO
data di nascita 06/02/1978 
CF DMIMRT78B06F839J
l'anagrafica è stata correttamente modificata ed in SCU i dati sono corretti ma in emissione polizza vita compare sempre errore CONTATTARE DIREZIONE VITA: Persona NVI chiave 171368433 e persona AGORA chiave 913411372 hanno data di nascita differente: verificare anagrafiche centralizzate
non è possibile cancellare l'anagrafica perché risulta avere almeno un legame con altra polizza
all img 
Dalle verifiche svolte dal III LIV sviluppo applicativo Anagrafe, Derni risponde: Vi confermo che il nostro modulo di cancellazione si blocca perché trova legami su ptf vita.
Potete cortesemente verificare l'errore che si presenta in emissione nuova proposta vita?
"
                //, Flagged = propFull ? mail.Flag.FlagStatus != ItemFlagStatus.NotFlagged : false
                ,
                Attachments = new object[] { }
                ,
                Categories = new string[] { }
                ,
                Importance = "Normal"
                //, ConversationId = propFull ? mail.ConversationId.UniqueId : string.Empty
            };
        }


        public IList<IMailItem> GetNewMailItemList()
        {
            throw new NotImplementedException();
        }

        public Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment ToAttachment(IAttachment att, string comment = "", string fileName = "")
        {
            throw new NotImplementedException();
        }

        public IAttachment ToAttachment(string subject, byte[] content)
        {
            throw new NotImplementedException();
        }

        public IDefect ToDefectItem(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem w)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDefect> ToDefectItemCollection(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCollection workItems)
        {
            throw new NotImplementedException();
        }

        public IFolderItem ToFolderItem(Microsoft.Exchange.WebServices.Data.Folder folder, Microsoft.Exchange.WebServices.Data.FindItemsResults<Microsoft.Exchange.WebServices.Data.Item> resultItems = null)
        {
            throw new NotImplementedException();
        }

        public IMailItem ToMailItem(Microsoft.Exchange.WebServices.Data.EmailMessage mail, bool propFull = false)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Enums;

namespace Allianz.Vita.Quality.Business.Models
{
    public class DefectItem : IDefect
    {

        public DefectItem()
        {
            Attachment = new IAttachment[] { };
            Comments = new string[] { };
        }

        #region IDefect Members

        public int? Id { get; set; }

        public bool AutoAssign { get; set; }

        public string Title { get; set; }

        public string AreaPath { get; set; }

        public string Iteration { get; set; }

        public string SurveySystem { get; set; }

        public string DefectID { get; set; }

        public string FoundIn { get; set; }

        public string Agency { get; set; }

        public string Environment { get; set; }

        public string DefectType { get; set; }

        public SeverityLevel Severity { get; set; }

        public string State { get; set; }

        public string Description { get; set; }

        public string[] Comments { get; set; }

        public IAttachment[] Attachment { get; set; }

        public string IMailItemUniqueId { get; set; }

        public string AssignedTo { get; set; }

        public string ConfirmedNotificationAddress { get; set; }

        #endregion
    }
}

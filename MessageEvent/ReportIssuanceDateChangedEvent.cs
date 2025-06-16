using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEvent
{
    public class ReportIssuanceDateChangedEvent
    {
        public ReportIssuanceDateChangedEvent(Guid engagementId,
                                              DateTime? earliestIssuanceDate,
                                              string eventType,
                                              string oldRecordNumber = null,
                                              string newRecordNumber = null
                                              )
        {
            EngagementId = engagementId;
            EarliestIssuanceDate = earliestIssuanceDate;
            OldRecordNumber = oldRecordNumber;
            NewRecordNumber = newRecordNumber;
            EventType = eventType;
        }

        public Guid EngagementId { get; }
        public DateTime? EarliestIssuanceDate { get; }

        public string OldRecordNumber { get; }
        public string NewRecordNumber { get; }

        public string EventType { get; }


    }
}
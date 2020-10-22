using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IReportHandler
    {
        List<ReportItem> AccomodationsReport(int spotId, DateTime startingDate, DateTime finishingDate);
    }
}
